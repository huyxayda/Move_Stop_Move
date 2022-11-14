using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close()
    {
        Time.timeScale = 1;

        base.Close();
    }
    public void ContinueButton()
    {
        Close();
    }

    public void HomeButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<Setting>();
    }
}
