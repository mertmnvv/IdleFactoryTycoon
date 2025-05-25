using UnityEngine;

/// <summary>
/// Tek bir üreticinin davranışlarını temsil eder.
/// Türetilen verilerle üretim yapar, seviye alır ve kayıt işlemlerini destekler.
/// </summary>
public class Producer
{
    // Üretim gerçekleştiğinde çağrılacak event (UI tetikleri için)
    public System.Action OnProduced;

    public ProducerData data;     // ScriptableObject bilgisi
    public float timer;           // Üretim zamanlayıcısı
    public int level = 1;         // Başlangıç seviyesi
    public bool isPurchased = false; // Satın alınma durumu

    /// <summary>
    /// Producer, tanımlı veriyle oluşturulur.
    /// </summary>
    public Producer(ProducerData data)
    {
        this.data = data;
        timer = 0f;
    }

    /// <summary>
    /// Her frame çağrılır; üretim zamanına ulaşıldığında para üretir.
    /// </summary>
    public void Tick(float deltaTime)
    {
        if (!isPurchased) return;

        timer += deltaTime;

        if (timer >= data.productionInterval)
        {
            timer = 0f;
            Produce();
        }
    }

    /// <summary>
    /// Üretimi tetikler, parayı GameManager’a aktarır.
    /// </summary>
    private void Produce()
    {
        int income = data.baseIncome * level;
        GameManager.Instance.AddMoney(income);
        OnProduced?.Invoke(); // UI efektlerini tetikler
    }

    /// <summary>
    /// Seviye artırımı yapar.
    /// </summary>
    public void Upgrade() => level++;

    /// <summary>
    /// Bu üreticinin kaydedilebilir versiyonunu üretir.
    /// </summary>
    public ProducerSaveData ToSaveData() => new ProducerSaveData
    {
        producerName = data.producerName,
        level = level,
        isPurchased = isPurchased
    };

    /// <summary>
    /// Kayıttan gelen veriyi üreticiye yükler.
    /// </summary>
    public void LoadFromSaveData(ProducerSaveData saved)
    {
        level = saved.level;
        isPurchased = saved.isPurchased;
    }
}
