namespace UralHedgehog
{
    namespace UI
    {
        public class UIManager
        {
            private readonly UIRoot _uiRoot;
            
            public UIManager(UIRoot uiRoot)
            {
                _uiRoot = uiRoot;
            }

            #region Open
            
            public void OpenViewGameplay(ILevel level)
            {
                _uiRoot.Create(nameof(PGameplay), level);
            }
            
            public void OpenViewMain()
            {
                _uiRoot.Create<IEmptyWidget>(nameof(PMain), null);
            }
            
            public void OpenViewLoseWin()
            {
                _uiRoot.Create<IEmptyWidget>(nameof(WLoseWin), null);
            }

            #endregion
            
            #region Close
            
            public void CloseViewGameplay()
            {
                _uiRoot.Kill(nameof(PGameplay));
            }

            #endregion
        }
    }
}