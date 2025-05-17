public interface ICell
{
    public int Id { get; }
    public ItemType Type { get; }
    public ItemData ItemData { get; }
    public bool IsEmpty { get; }
}