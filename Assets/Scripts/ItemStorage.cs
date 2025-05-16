using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStorage", menuName = "Storage/Item", order = 1)]
public class ItemStorage : ScriptableObject
{
    [SerializeField] private Item _itemSquarePrefab;
    public Item ItemSquarePrefab => _itemSquarePrefab;
    [SerializeField] private Item _itemCirclePrefab;
    public Item ItemCirclePrefab => _itemCirclePrefab;
    [SerializeField] private Item _itemTrianglePrefab;
    public Item ItemTrianglePrefab => _itemTrianglePrefab;
    
    [Space(5)]
    
    [SerializeField] private List<ItemData> _items;
    public List<ItemData> Items => _items;
}