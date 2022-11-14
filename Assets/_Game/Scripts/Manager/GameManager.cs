using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {MainMenu, GamePlay, Pause, FinishGame, Shop}
public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    private void Update()
    {
        Debug.Log(gameState);
    }
    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
