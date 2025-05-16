using UnityEngine;
using UnityEngine.UI;
using UralHedgehog;
using UralHedgehog.UI;

public class PMain : Widget<IEmptyWidget>
{
    [SerializeField] private Button _btnPlay;
    
    public override void Init(IEmptyWidget model)
    {
        base.Init(model);
        
        _btnPlay.onClick.AddListener(() =>
        {
            Game.Instance.ChangeState(GameState.PLAY);
            Hide();
        });
    }
}