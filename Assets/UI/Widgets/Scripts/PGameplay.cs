using UnityEngine;
using UnityEngine.UI;
using UralHedgehog.UI;

public class PGameplay : Widget<ILevel>
{
    [SerializeField] private BarView _barView;
    [SerializeField] private Button _btnRefresh;
    
    public override void Init(ILevel model)
    {
        base.Init(model);
        
        _barView.Init(Model.Bar);
        
        Model.AddItem += OnAddItem;
        Model.RemoveItem += OnRemoveItem;

        Model.Begin += value => { _btnRefresh.interactable = value; };
        _btnRefresh.onClick.AddListener(() => Model.Refresh());
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