using UnityEngine;

/// <summary>
/// Zone panelini sahnede gösteren/kaydýran sistemi yönetir.
/// Prefab instantiate eder, içindeki FactoryZone'a zone atar.
/// </summary>
public class SlidingPanel : MonoBehaviour
{
    [Header("Panel Kaydýrma Ayarlarý")]
    public RectTransform panelTransform;
    public float slideSpeed = 2000f;

    private GameObject currentPanel;
    private FactoryZone currentFactoryZone;

    private Vector2 hiddenPos;
    private Vector2 shownPos;
    private bool isSliding = false;
    private bool isShown = false;

    private void Start()
    {
        // Panel baþlangýçta ekran dýþýnda
        hiddenPos = new Vector2(0, -Screen.height);
        shownPos = Vector2.zero;
        panelTransform.anchoredPosition = hiddenPos;
    }

    private void Update()
    {
        if (!isSliding) return;

        Vector2 target = isShown ? shownPos : hiddenPos;
        panelTransform.anchoredPosition = Vector2.MoveTowards(
            panelTransform.anchoredPosition, target, slideSpeed * Time.deltaTime
        );

        if (Vector2.Distance(panelTransform.anchoredPosition, target) < 0.1f)
            isSliding = false;
    }

    /// <summary>
    /// Seçilen zone'a ait prefab'ý sahneye getirir ve FactoryZone'a set eder.
    /// </summary>
    public void ShowZone(ZoneConfig zoneConfig)
    {
        // Önceki paneli yok et
        if (currentPanel != null)
            Destroy(currentPanel);

        // Yeni prefab instantiate et
        currentPanel = Instantiate(zoneConfig.uiParentPrefab, panelTransform);

        // Ýçindeki FactoryZone'u bul ve zone'u ata
        currentFactoryZone = currentPanel.GetComponentInChildren<FactoryZone>();
        if (currentFactoryZone != null)
            currentFactoryZone.SetZone(zoneConfig.zoneName);

        isShown = true;
        isSliding = true;
    }

    /// <summary>
    /// Panelin gösterilip gizlenmesini saðlar.
    /// </summary>
    public void TogglePanel()
    {
        isShown = !isShown;
        isSliding = true;
    }
}
