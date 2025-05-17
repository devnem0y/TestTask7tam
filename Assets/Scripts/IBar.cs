public interface IBar
{
    public int Length { get; }
    
    ICell GetCellByIndex(int id);
}