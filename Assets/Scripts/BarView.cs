using System.Collections.Generic;
using UnityEngine;

public class BarView : MonoBehaviour
{
    [SerializeField] private CellView _cellViewPrefab;
    [SerializeField] private Transform _wrapper;
    
    private List<CellView> _cells;

    public void Init(IBar bar)
    {
        _cells = new List<CellView>();
        
        for (var i = 0; i < bar.Length; i++)
        {
            var cellView = Instantiate(_cellViewPrefab, _wrapper);
            cellView.Init(bar.GetCellByIndex(i).Id, bar.GetCellByIndex(i).Type);
            _cells.Add(cellView);
        }
    }

    public CellView GetCellById(int id)
    {
        return _cells.Find(cell => cell.Id == id);
    }
}
