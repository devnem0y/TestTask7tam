using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private SpriteRenderer _image;
    
    private string _id;
    public string Id => _id;
    
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    
    public event Action<Item, Vector3> Selected;

    public void Init(string id, ItemData itemData)
    {
        _id = id;
        _itemData = itemData;
        _sprite.color = itemData.Color;
        _image.sprite = itemData.Image;
    }

    public void OnMouseDown()
    {
        Selected?.Invoke(this, Input.mousePosition);
    }
}