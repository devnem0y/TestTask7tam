using System.Collections.Generic;
using System.Linq;

public class Bar : IBar
{
    private readonly List<Cell> _cells = new()
    {
        new Cell(0, ItemType.SQUARE),
        new Cell(1, ItemType.SQUARE),
        new Cell(2, ItemType.SQUARE),
        new Cell(3, ItemType.CIRCLE),
        new Cell(4, ItemType.CIRCLE),
        new Cell(5, ItemType.CIRCLE),
        new Cell(6, ItemType.TRIANGLE),
        new Cell(7, ItemType.TRIANGLE),
        new Cell(8, ItemType.TRIANGLE)
    };
    
    public int Length => _cells.Count;

    public Bar()
    {
        MyTools.Shuffle(_cells);
    }

    public ICell GetCellByIndex(int id)
    {
        return _cells[id];
    }
    
    public Cell GetCellById(int id)
    {
        return _cells.Find(cell => cell.Id == id);
    }

    public Cell GetCellByType(ItemType type)
    {
        return _cells.Find(cell => cell.Type == type);
    }
    
    public Cell GetEmptyCellByType(ItemType type)
    {
        return _cells.FirstOrDefault(cell => cell.Type == type && cell.IsEmpty);
    }
    
    public List<Cell> GetCellsByKey(string key)
    {
        return _cells.Where(cell => cell.ItemData?.Key == key).ToList();
    }
}