using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private ItemType _type;

    private int _id;
    public int Id => _id;
    
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    
    public bool IsEmpty => _itemData == null;
    
    public Vector3 Position => transform.position;
    public ItemType Type => _type;

    public void Init(int id)
    {
        _id = id;
    }
    
    public void SetItem(ItemData item)
    {
        _itemData = item;
    }

    public void Clear()
    {
        _itemData = null;
    }
}
