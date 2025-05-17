public class Cell : ICell
{
    private int _id;
    public int Id => _id;
    
    private ItemType _type;
    public ItemType Type => _type;
    
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    
    public bool IsEmpty => _itemData == null;

    public Cell(int id, ItemType type)
    {
        _id = id;
        _type = type;
    }

    public void SetItem(ItemData itemData)
    {
        _itemData = itemData;
    }
    
    public void Clear()
    {
        _itemData = null;
    }
}