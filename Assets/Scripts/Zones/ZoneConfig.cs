using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Her �retim b�lgesi (zone) i�in ayarlar� tutan ScriptableObject.
/// Hangi �reticiler ve hangi UI prefab kullan�laca�� burada belirlenir.
/// </summary>
[CreateAssetMenu(menuName = "Fabrika/ZoneConfig")]
public class ZoneConfig : ScriptableObject
{
    [Header("Benzersiz B�lge Ad� (kay�t sisteminde anahtar olarak kullan�l�r)")]
    public string zoneName;

    [Header("Bu b�lgeye ait UI panel prefab'� (�rne�in: WoodPanel, IronPanel)")]
    public GameObject uiParentPrefab;

    [Header("Bu b�lgeye ait �reticiler (ProducerData asset'leri)")]
    public List<ProducerData> producers;
}
