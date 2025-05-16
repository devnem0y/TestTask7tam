using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UralHedgehog;
using UralHedgehog.UI;

public class WLoseWin : Widget<IEmptyWidget>
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _btnClose;
    
    public override void Init(IEmptyWidget model)
    {
        base.Init(model);
        
        _title.text = Game.Instance.State == GameState.VICTORY ? "You Win!" : "You Lose";
        
        _btnClose.onClick.AddListener(() =>
        {
            Game.Instance.ChangeState(GameState.PLAY);
            Hide();
        });
    }
}