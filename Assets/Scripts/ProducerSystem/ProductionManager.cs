using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Tüm üreticileri yönetir, oluşturur ve pasif üretimi yürütür.
/// Singleton olarak sahnede tek örnekle çalışır.
/// </summary>
public class ProductionManager : MonoBehaviour
{
    public static ProductionManager Instance;

    [Header("Zone Ayarları")]
    public List<ZoneConfig> zoneConfigs;

    // Her bölgeye ait üretici listesi
    private Dictionary<string, List<Producer>> zoneProducers = new Dictionary<string, List<Producer>>();

    private void Awake()
    {
        // Singleton kurulumu
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Kayıtlı verileri yükle
        SaveData save = GameManager.Instance.Load();

        foreach (var cfg in zoneConfigs)
        {
            var list = new List<Producer>();
            var zoneData = save.zones.Find(z => z.zoneName == cfg.zoneName);

            // Kayıtta yoksa zone'u oluştur
            if (zoneData == null)
            {
                zoneData = new ZoneSaveData { zoneName = cfg.zoneName };
                save.zones.Add(zoneData);
            }

            foreach (var pd in cfg.producers)
            {
                var producer = new Producer(pd);

                // Kayıt varsa bilgileri yükle
                var saved = zoneData.producers.Find(p => p.producerName == pd.producerName);
                if (saved != null)
                    producer.LoadFromSaveData(saved);

                list.Add(producer);
            }

            zoneProducers[cfg.zoneName] = list;
        }
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        // Her üreticiye Tick süresi uygula
        foreach (var kv in zoneProducers)
        {
            foreach (var producer in kv.Value)
                producer.Tick(dt);
        }
    }

    /// <summary>
    /// İstenen bölgenin üreticilerini getirir.
    /// </summary>
    public List<Producer> GetProducers(string zoneName)
    {
        if (zoneProducers.TryGetValue(zoneName, out var list))
            return list;
        return new List<Producer>();
    }

    /// <summary>
    /// Tüm üreticileri kayıt verisine aktarır.
    /// </summary>
    public void PopulateSaveData(SaveData data)
    {
        foreach (var kv in zoneProducers)
        {
            // Zone kaydını bul ya da oluştur
            var zsd = data.zones.Find(z => z.zoneName == kv.Key)
                      ?? new ZoneSaveData { zoneName = kv.Key };
            if (!data.zones.Contains(zsd))
                data.zones.Add(zsd);

            // Üretici kayıtlarını aktar
            zsd.producers.Clear();
            foreach (var producer in kv.Value)
                zsd.producers.Add(producer.ToSaveData());
        }
    }
}
