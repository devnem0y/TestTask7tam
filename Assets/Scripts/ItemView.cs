using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    private const float SPEED = 720f;
    
    [SerializeField] private Image _sprite;
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<Sprite> _images;
    
    private Vector3 _target;
    private bool _isMoving;

    public void Init(ItemData itemData, Vector3 target)
    {
        _sprite.sprite = itemData.Type switch
        {
            ItemType.SQUARE => _sprites[0],
            ItemType.CIRCLE => _sprites[1],
            ItemType.TRIANGLE => _sprites[2],
            _ => _image.sprite
        };

        if (itemData.Type == ItemType.TRIANGLE) _image.rectTransform.anchoredPosition = new Vector2(0, -20);

        _sprite.color = itemData.Color;
        _image.sprite = _images[itemData.ImageId];
        
        _target = target;
        
        _isMoving = true;
    }

    private void Update()
    {
        if (!_isMoving) return;
        
        var step =  SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);

        if (!(Vector3.Distance(transform.position, _target) < 0.001f)) return;
        transform.localPosition = new Vector3(0, 0, 0);
        _isMoving = false;
    }
}