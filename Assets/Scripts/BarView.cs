using UnityEngine;

public class BarView : MonoBehaviour
{
    [SerializeField] private CellView _cellViewPrefab;
    [SerializeField] private Transform _wrapper;

    public void Init(IBar bar)
    {
        for (var i = 0; i < bar.Length; i++)
        {
            var cellView = Instantiate(_cellViewPrefab, _wrapper);
            cellView.Init(bar.GetCellByIndex(i).Id, bar.GetCellByIndex(i).Type);
        }
    }
}
