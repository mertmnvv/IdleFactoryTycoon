using UnityEngine;

/// <summary>
/// ScriptableObject olarak tanýmlanan üretici verisi.
/// Tüm üretici türleri için kullanýlacak temel taným.
/// </summary>
[CreateAssetMenu(fileName = "YeniUretici", menuName = "Fabrika/Üretici")]
public class ProducerData : ScriptableObject
{
    [Header("Üretici Tanýmý")]
    [Tooltip("Üreticinin eþsiz ismi (kayýt ve yükleme için kullanýlýr)")]
    public string producerName;

    [Tooltip("Tek üretimde kazandýrdýðý para miktarý")]
    public int baseIncome;

    [Tooltip("Üretim aralýðý (saniye cinsinden)")]
    public float productionInterval;

    [Tooltip("Upgrade baþýna maliyet")]
    public int upgradeCost;

    [Tooltip("UI'da gösterilecek ikon")]
    public Sprite icon;
}
