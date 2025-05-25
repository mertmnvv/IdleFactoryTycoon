using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Oyun genel yönetim sınıfı.
/// Para, kayıt/yükleme, offline kazanç ve Singleton yönetimini sağlar.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI – Para Gösterimi")]
    public TextMeshProUGUI moneyText;

    private string savePath;
    private SaveData saveData;

    private void Awake()
    {
        // Singleton örneğini oluşturur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişse bile kalıcı
        }
        else
        {
            Destroy(gameObject); // Zaten varsa yok et
            return;
        }

        // Kayıt dosyasının yolu
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    private void Start()
    {
        LoadData(); // Oyun başlarken veriyi yükle
    }

    /// <summary>
    /// Diğer sınıfların kayıt verisine erişebilmesi için yardımcı metod.
    /// </summary>
    public SaveData Load()
    {
        if (saveData == null)
            LoadData();
        return saveData;
    }

    /// <summary>
    /// Oyuncuya para ekler ve UI'ı günceller.
    /// </summary>
    public void AddMoney(int amount)
    {
        saveData.money += amount;
        UpdateMoneyUI();
    }

    /// <summary>
    /// Oyuncudan para düşer. Yeterli bakiye varsa true döner.
    /// </summary>
    public bool SpendMoney(int amount)
    {
        if (saveData.money >= amount)
        {
            saveData.money -= amount;
            UpdateMoneyUI();
            return true;
        }

        Debug.LogWarning("Yetersiz bakiye!");
        return false;
    }

    /// <summary>
    /// UI üzerindeki para yazısını günceller.
    /// </summary>
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = $"Para: {saveData.money}";
    }

    /// <summary>
    /// Kaydedilmiş verileri dosyadan yükler.
    /// Gerekli listeleri başlatır ve offline kazancı hesaplar.
    /// </summary>
    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
        }

        // Null listeleri garanti altına al
        if (saveData.zones == null)
            saveData.zones = new List<ZoneSaveData>();
        foreach (var z in saveData.zones)
            if (z.producers == null)
                z.producers = new List<ProducerSaveData>();

        // Offline süreye göre kazanç hesapla
        if (saveData.lastQuitTime > 0)
        {
            long now = DateTime.UtcNow.Ticks;
            double elapsed = (now - saveData.lastQuitTime) / (double)TimeSpan.TicksPerSecond;

            foreach (var zoneData in saveData.zones)
            {
                foreach (var ps in zoneData.producers)
                {
                    if (!ps.isPurchased) continue;

                    var pd = Resources.Load<ProducerData>($"ProducerData/{ps.producerName}");
                    if (pd == null)
                    {
                        Debug.LogWarning($"ProducerData bulunamadı: {ps.producerName}");
                        continue;
                    }

                    int ticks = Mathf.FloorToInt((float)(elapsed / pd.productionInterval));
                    int income = ticks * pd.baseIncome * ps.level;
                    saveData.money += income;

                    Debug.Log($"[Offline] {ps.producerName}: {ticks} üretim → +{income} para");
                }
            }
        }

        UpdateMoneyUI();
    }

    /// <summary>
    /// Verileri JSON formatında diske kaydeder.
    /// </summary>
    public void SaveAllZones()
    {
        var newData = new SaveData
        {
            money = saveData.money,
            lastQuitTime = DateTime.UtcNow.Ticks,
            zones = new List<ZoneSaveData>()
        };

        ProductionManager.Instance.PopulateSaveData(newData);

        string json = JsonUtility.ToJson(newData, true);
        File.WriteAllText(savePath, json);
        saveData = newData;

        Debug.Log("Kayıt yapıldı:\n" + json);
    }

    /// <summary>
    /// Uygulama kapanırken kayıt yapar.
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveAllZones();
    }
}
