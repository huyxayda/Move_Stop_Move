using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        GameManager.Instance.ChangeState(GameState.GamePlay);
        UIManager.Instance.OpenUI<GamePlay>();
        UIManager.Instance.OpenUI<IndicatorCanVas>();
        UIManager.Instance.CloseUI<Loose>();
    }

    public void HomeButton()
    {
        LevelManager.Instance.OnRetry();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<Loose>();
    }
}
