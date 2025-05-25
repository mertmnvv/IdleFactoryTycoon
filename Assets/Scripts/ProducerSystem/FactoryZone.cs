using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Belirli bir zone için üreticilerin UI gösterimini yönetir.
/// </summary>
public class FactoryZone : MonoBehaviour
{
    [Header("Zone Bilgisi")]
    public string zoneName;

    [Header("UI Ayarları")]
    public GameObject producerUIPrefab; // Üretici UI prefabı
    public Transform uiParent;          // UI'ların konulacağı alan

    private List<Producer> currentProducers = new List<Producer>();

    /// <summary>
    /// Verilen zone adıyla üretici listesini UI'a yükler.
    /// </summary>
    public void SetZone(string newZoneName)
    {
        // Önceki UI objelerini temizle
        foreach (Transform child in uiParent)
        {
            var ui = child.GetComponent<ProducerUI>();
            if (ui != null)
                ui.Cleanup();

            Destroy(child.gameObject);
        }

        currentProducers.Clear();
        zoneName = newZoneName;

        // Üretici listesini al ve UI üret
        var producers = ProductionManager.Instance.GetProducers(zoneName);
        currentProducers = producers;

        foreach (var p in currentProducers)
        {
            var ui = Instantiate(producerUIPrefab, uiParent).GetComponent<ProducerUI>();
            ui.Setup(p);
        }

        Debug.Log($"[FactoryZone] UI yüklendi: {zoneName} → {currentProducers.Count} üretici");
    }

    /// <summary>
    /// Dışarıdan kayıt yapılması gerektiğinde çağrılır.
    /// (Şu an doğrudan kullanılmıyor çünkü kayıt GameManager üzerinden yapılıyor.)
    /// </summary>
    public void PopulateSaveData(SaveData data)
    {
        ProductionManager.Instance.PopulateSaveData(data);
    }
}
