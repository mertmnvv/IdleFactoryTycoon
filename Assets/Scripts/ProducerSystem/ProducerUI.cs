using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Tek bir üreticinin UI kontrolünü sağlar (seviye, upgrade, gelir vs.).
/// </summary>
public class ProducerUI : MonoBehaviour
{
    private Producer producer;

    [Header("UI Bileşenleri")]
    public Slider productionBar;
    public TextMeshProUGUI incomePopupText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI costText;
    public Button buyButton;
    public Button upgradeButton;

    /// <summary>
    /// UI'ı verilen üreticiye bağlar ve gerekli event'leri ayarlar.
    /// </summary>
    public void Setup(Producer newProducer)
    {
        // Eski bağlantıyı temizle
        if (producer != null)
            producer.OnProduced -= ShowIncomePopup;

        producer = newProducer;

        // Metinleri doldur
        nameText.text = producer.data.producerName;
        UpdateUI();

        // Buton listener'larını ayarla
        buyButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();

        buyButton.onClick.AddListener(OnBuy);
        upgradeButton.onClick.AddListener(OnUpgrade);

        // Üretim olduğunda popup göster
        producer.OnProduced += ShowIncomePopup;
    }

    private void Update()
    {
        // Üretim barını güncelle
        if (producer != null && productionBar != null)
        {
            productionBar.value = producer.timer / producer.data.productionInterval;
        }
    }

    /// <summary>
    /// Seviye, gelir ve butonları günceller.
    /// </summary>
    public void UpdateUI()
    {
        levelText.text = $"Level: {producer.level}";
        incomeText.text = $"Income: {producer.data.baseIncome * producer.level}";

        if (producer.isPurchased)
        {
            costText.text = $"Upgrade: {producer.data.upgradeCost * producer.level}";
            buyButton.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            costText.text = $"Buy: {producer.data.upgradeCost}";
            buyButton.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Satın alma işlemini gerçekleştirir.
    /// </summary>
    private void OnBuy()
    {
        if (!producer.isPurchased && GameManager.Instance.SpendMoney(producer.data.upgradeCost))
        {
            producer.isPurchased = true;
            UpdateUI();
            GameManager.Instance.SaveAllZones();
        }
    }

    /// <summary>
    /// Seviye yükseltme işlemini gerçekleştirir.
    /// </summary>
    private void OnUpgrade()
    {
        int cost = producer.data.upgradeCost * producer.level;

        if (GameManager.Instance.SpendMoney(cost))
        {
            producer.Upgrade();
            UpdateUI();
            GameManager.Instance.SaveAllZones();
        }
    }

    /// <summary>
    /// Üretim olduğunda ekranda geçici gelir gösterir.
    /// </summary>
    private void ShowIncomePopup()
    {
        if (incomePopupText == null) return;

        incomePopupText.text = $"+{producer.data.baseIncome * producer.level}";
        incomePopupText.gameObject.SetActive(true);
        StartCoroutine(HidePopup());
    }

    private IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(1f);
        incomePopupText.gameObject.SetActive(false);
    }

    /// <summary>
    /// UI objesi kaldırılırken event bağlantılarını temizler.
    /// </summary>
    public void Cleanup()
    {
        if (producer != null)
            producer.OnProduced -= ShowIncomePopup;
    }
}
