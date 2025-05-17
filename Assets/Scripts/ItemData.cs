using UnityEngine;

[System.Serializable]
public class ItemData
{
    [SerializeField] private ItemType _type;
    public ItemType Type => _type;
    
    [SerializeField] private Color _color;
    public Color Color => _color;
    
    [SerializeField] private int _imageId;
    public int ImageId => _imageId;
    
    [SerializeField] private Sprite _image;
    public Sprite Image => _image;
    
    [SerializeField] private string _key;
    public string Key => _key;
}