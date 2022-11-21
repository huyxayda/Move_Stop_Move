using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        GameManager.Instance.ChangeState(GameState.GamePlay);
        UIManager.Instance.OpenUI<GamePlay>();
        UIManager.Instance.OpenUI<IndicatorCanVas>();

        Close();

    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close();
    }
}
