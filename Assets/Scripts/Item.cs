using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private SpriteRenderer _image;

    public ItemData ItemData { get; private set; }

    public event Action<Item, Vector3> Selected;

    public void Init(ItemData itemData)
    {
        ItemData = itemData;
        _sprite.color = itemData.Color;
        _image.sprite = itemData.Image;
    }

    public void OnMouseDown()
    {
        Selected?.Invoke(this, Input.mousePosition);
    }
}