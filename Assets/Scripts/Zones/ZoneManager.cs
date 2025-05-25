using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Dropdown üzerinden zone seçimini yönetir.
/// Seçilen zone'a ait paneli gösterir.
/// </summary>
public class ZoneManager : MonoBehaviour
{
    [Header("Zone Konfigürasyonları (Dropdown'da listelenecek)")]
    public List<ZoneConfig> zones;

    [Header("Dropdown UI ve Sliding Panel")]
    public TMP_Dropdown zoneDropdown;
    public SlidingPanel slidingPanel;

    private void Start()
    {
        // Dropdown temizlenir ve zone isimleri eklenir
        zoneDropdown.ClearOptions();
        var options = zones.Select(z => new TMP_Dropdown.OptionData(z.zoneName)).ToList();
        zoneDropdown.AddOptions(options);

        // Seçim eventi atanır
        zoneDropdown.onValueChanged.AddListener(OnZoneSelected);

        // Başlangıçta ilk zone gösterilir
        if (zones.Count > 0)
        {
            zoneDropdown.value = 0;
            OnZoneSelected(0);
        }
    }

    /// <summary>
    /// Dropdown'dan yeni zone seçildiğinde çalışır.
    /// </summary>
    private void OnZoneSelected(int idx)
    {
        if (idx < 0 || idx >= zones.Count) return;

        var selectedConfig = zones[idx];
        Debug.Log($"[ZoneManager] Seçilen bölge: {selectedConfig.zoneName}");

        // Sliding panel'e seçilen zone'u gönder
        slidingPanel.ShowZone(selectedConfig);
    }
}
