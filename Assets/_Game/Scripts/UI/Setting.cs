using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
        UIManager.Instance.CloseUI<GamePlay>();

    }

    public override void Close()
    {
        Time.timeScale = 1;

        base.Close();
    }
    public void ContinueButton()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        Close();
    }

    public void HomeButton()
    {
        UIManager.Instance.CloseUI<Setting>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
    }
}
