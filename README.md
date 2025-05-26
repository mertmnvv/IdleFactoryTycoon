#  Idle Factory Tycoon – Unity Case Projesi

Bu proje, Unity kullanılarak geliştirilmiş bir Idle Factory oyun sistemidir. Proje, verilen case dokümanına göre tüm temel ve bazı bonus sistemleri başarıyla içermektedir.

## Özellikler

### ✅ Zorunlu Sistemler
- **Click to Earn:** Ana makineye tıklayarak gelir elde etme
- **Passive Producer System:** Belirli aralıklarla üretim yapan makineler
- **Upgrade System:** Seviye atladıkça gelir artışı
- **Save/Load:** JSON üzerinden veri kaydetme ve yükleme
- **Visual Feedback:** Coin efekti, üretim barı, popup kazanç animasyonu
- **Modülerlik:** Yeni zone ve üretici kolayca eklenebilir

###  Bonuslar
- **Offline Progress Sistemi:** Oyuncu oyunda değilken bile üretim devam eder
- **Editor Tool:** `ProducerData` ScriptableObject’lerini yöneten özel editör aracı

## Proje Yapısı

```
Assets/
│
├── Scripts/
│   ├── Core (GameManager, ProductionManager)
│   ├── ProducerSystem (Producer, ProducerData, ProducerUI)
│   ├── UI (FactoryZone, SlidingPanel, ClickableMachine)
│   ├── ScriptableObjects (ZoneConfig)
│   └── Editor (ProducerEditorWindow.cs)
│
├── Resources/
│   └── ProducerData/  → Tüm üretici verileri burada
```

## Kurulum

1. Unity 2021.3+ ile açın  
2. Scene: `MainScene`  
3. "Play" tuşuna basarak test edin  
4. `Tools > Producer Editor` üzerinden üretici yönetin  
5. `SaveData` JSON dosyası `Application.persistentDataPath` içinde oluşur

## 📝 Teslim Notları

- Tüm sistemler test edilmiştir.
- Kayıt ve offline üretim sistemleri detaylı olarak uygulanmıştır.
- Kodlar temizlenmiş, açıklamalar eklenmiştir.
- UI sistemleri mobil uyuma uygundur.
- Gereken her şey `Resources` klasörüne düzenli şekilde yerleştirilmiştir.

##  Geliştirici

> Bu proje case kapsamında tarafımdan geliştirilmiştir.  
> Gelişime açık olan kısımlar için yorumlarınız değerli olacaktır.