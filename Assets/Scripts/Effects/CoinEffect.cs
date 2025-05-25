using UnityEngine;

/// <summary>
/// Para efekti animasyonu: yukarý çýkar ve yok olur.
/// Týklama sonrasý üretim efekti olarak kullanýlýr.
/// </summary>
public class CoinEffect : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 2f;
    public float lifetime = 1.5f;

    private float timer = 0f;

    private void Update()
    {
        // Yukarý hareket
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Belirli sürede yok ol
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
