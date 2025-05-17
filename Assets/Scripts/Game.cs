using System;
using UnityEngine;
using UralHedgehog.UI;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public GameState State { get; private set; }
    
    [SerializeField] private Level _levelPrefab;
    
    private UIManager _uIManager;
    private Level _level;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _uIManager = new UIManager(FindObjectOfType<UIRoot>());
        ChangeState(GameState.MAIN);
    }

    public void ChangeState(GameState state)
    {
        State = state;

        switch (State)
        {
            case GameState.MAIN:
                _uIManager.OpenViewMain();
                break;
            case GameState.PLAY:
                _level = Instantiate(_levelPrefab);
                _level.Init(() => { _uIManager.OpenViewGameplay(_level); });
                break;
            case GameState.VICTORY:
            case GameState.DEFEAT:
                Destroy(_level.gameObject);
                _uIManager.CloseViewGameplay();
                _uIManager.OpenViewLoseWin();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        print($"<color=yellow>{State.ToString()}</color>");
    }
}