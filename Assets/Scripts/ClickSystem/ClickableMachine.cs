using UnityEngine;
using TMPro;

/// <summary>
/// Oyuncunun tıklayarak para kazanmasını sağlayan makine sistemi.
/// Belirli bir süre aralığında tıklanabilir.
/// </summary>
public class ClickableMachine : MonoBehaviour
{
    [Header("Kazanç Ayarları")]
    public int clickIncome = 10;

    [Header("Zamanlayıcı")]
    public float clickCooldown = 10f;
    private float timer = 0f;
    private bool canClick = true;

    [Header("Efekt ve UI")]
    public GameObject coinEffectPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI countdownText;
    public AudioSource clickSound;

    private void Update()
    {
        // Cooldown süresi aktifse zamanlayıcıyı çalıştır
        if (!canClick)
        {
            timer -= Time.deltaTime;

            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(timer).ToString();

            if (timer <= 0f)
            {
                canClick = true;
                if (countdownText != null)
                    countdownText.text = "";
            }
        }
    }

    /// <summary>
    /// Makineye tıklanınca çağrılır. Para ekler, efekt gösterir.
    /// </summary>
    public void OnMachineClick()
    {
        if (!canClick) return;

        // Para ekle
        GameManager.Instance.AddMoney(clickIncome);

        // Coin efekti oluştur
        if (coinEffectPrefab != null && spawnPoint != null)
            Instantiate(coinEffectPrefab, spawnPoint.position, Quaternion.identity);

        // Ses çal
        if (clickSound != null)
            clickSound.Play();

        // Cooldown başlat
        timer = clickCooldown;
        canClick = false;
    }
}
