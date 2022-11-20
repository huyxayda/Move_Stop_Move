using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        UIManager.Instance.OpenUI<GamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);

        Close();

    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close();
    }
}
