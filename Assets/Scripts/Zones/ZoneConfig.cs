using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Her üretim bölgesi (zone) için ayarlarý tutan ScriptableObject.
/// Hangi üreticiler ve hangi UI prefab kullanýlacaðý burada belirlenir.
/// </summary>
[CreateAssetMenu(menuName = "Fabrika/ZoneConfig")]
public class ZoneConfig : ScriptableObject
{
    [Header("Benzersiz Bölge Adý (kayýt sisteminde anahtar olarak kullanýlýr)")]
    public string zoneName;

    [Header("Bu bölgeye ait UI panel prefab'ý (örneðin: WoodPanel, IronPanel)")]
    public GameObject uiParentPrefab;

    [Header("Bu bölgeye ait üreticiler (ProducerData asset'leri)")]
    public List<ProducerData> producers;
}
