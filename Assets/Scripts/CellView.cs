using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private ItemView _itemViewPrefab;
    
    public int Id { get; private set; }
    
    public void Init(int id, ItemType type)
    {
        Id = id;
        
        _image.sprite = type switch
        {
            ItemType.SQUARE => _sprites[0],
            ItemType.CIRCLE => _sprites[1],
            ItemType.TRIANGLE => _sprites[2],
            _ => _image.sprite
        };
    }

    public void AddItem(ItemData itemData, Vector3 startPosition)
    {
        var item = Instantiate(_itemViewPrefab, startPosition, Quaternion.identity, transform);
        item.Init(itemData, transform.position);
    }

    public void Clear()
    {
        if (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
    }
}
