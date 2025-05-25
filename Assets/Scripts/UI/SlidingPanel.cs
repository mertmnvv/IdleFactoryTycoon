using UnityEngine;

/// <summary>
/// Zone panelini sahnede g�steren/kayd�ran sistemi y�netir.
/// Prefab instantiate eder, i�indeki FactoryZone'a zone atar.
/// </summary>
public class SlidingPanel : MonoBehaviour
{
    [Header("Panel Kayd�rma Ayarlar�")]
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
        // Panel ba�lang��ta ekran d���nda
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
    /// Se�ilen zone'a ait prefab'� sahneye getirir ve FactoryZone'a set eder.
    /// </summary>
    public void ShowZone(ZoneConfig zoneConfig)
    {
        // �nceki paneli yok et
        if (currentPanel != null)
            Destroy(currentPanel);

        // Yeni prefab instantiate et
        currentPanel = Instantiate(zoneConfig.uiParentPrefab, panelTransform);

        // ��indeki FactoryZone'u bul ve zone'u ata
        currentFactoryZone = currentPanel.GetComponentInChildren<FactoryZone>();
        if (currentFactoryZone != null)
            currentFactoryZone.SetZone(zoneConfig.zoneName);

        isShown = true;
        isSliding = true;
    }

    /// <summary>
    /// Panelin g�sterilip gizlenmesini sa�lar.
    /// </summary>
    public void TogglePanel()
    {
        isShown = !isShown;
        isSliding = true;
    }
}
