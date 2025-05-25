using UnityEngine;

/// <summary>
/// Para efekti animasyonu: yukar� ��kar ve yok olur.
/// T�klama sonras� �retim efekti olarak kullan�l�r.
/// </summary>
public class CoinEffect : MonoBehaviour
{
    [Header("Hareket Ayarlar�")]
    public float moveSpeed = 2f;
    public float lifetime = 1.5f;

    private float timer = 0f;

    private void Update()
    {
        // Yukar� hareket
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Belirli s�rede yok ol
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
