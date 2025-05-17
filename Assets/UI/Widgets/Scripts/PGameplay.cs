using UnityEngine;
using UralHedgehog.UI;

public class PGameplay : Widget<ILevel>
{
    [SerializeField] private BarView _barView;
    
    public override void Init(ILevel model)
    {
        base.Init(model);
        
        _barView.Init(Model.Bar);
    }
}