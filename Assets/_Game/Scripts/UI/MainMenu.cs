using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        //vao game va bat gameplayUI, dong UI Mainmenu
        LevelManager.Instance.OnStartGame();
        UIManager.Instance.OpenUI<GamePlay>();
        UIManager.Instance.CloseUI<MainMenu>();
    }

    public void WeaponButton()
    {

    }

    public void SkinButton()
    {

    }
}
