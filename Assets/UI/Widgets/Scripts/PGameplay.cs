using UnityEngine;
using UralHedgehog.UI;

public class PGameplay : Widget<ILevel>
{
    [SerializeField] private BarView _barView;
    
    public override void Init(ILevel model)
    {
        base.Init(model);
        
        _barView.Init(Model.Bar);
        
        Model.AddItem += OnAddItem;
        Model.RemoveItem += OnRemoveItem;
    }

    private void OnAddItem(int cellId, ItemData itemData, Vector3 pos)
    {
        _barView.GetCellById(cellId).AddItem(itemData, pos);
    }
    
    private void OnRemoveItem(int cellId)
    {
        _barView.GetCellById(cellId).Clear();
    }

    private void OnDestroy()
    {
        Model.AddItem -= OnAddItem;
        Model.RemoveItem -= OnRemoveItem;
    }
}