using UnityEngine;

/// <summary>
/// ScriptableObject olarak tan�mlanan �retici verisi.
/// T�m �retici t�rleri i�in kullan�lacak temel tan�m.
/// </summary>
[CreateAssetMenu(fileName = "YeniUretici", menuName = "Fabrika/�retici")]
public class ProducerData : ScriptableObject
{
    [Header("�retici Tan�m�")]
    [Tooltip("�reticinin e�siz ismi (kay�t ve y�kleme i�in kullan�l�r)")]
    public string producerName;

    [Tooltip("Tek �retimde kazand�rd��� para miktar�")]
    public int baseIncome;

    [Tooltip("�retim aral��� (saniye cinsinden)")]
    public float productionInterval;

    [Tooltip("Upgrade ba��na maliyet")]
    public int upgradeCost;

    [Tooltip("UI'da g�sterilecek ikon")]
    public Sprite icon;
}
