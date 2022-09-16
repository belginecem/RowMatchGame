using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public boardGrid grid;
    public HUD hud;

    public int currentScore;

    void Start()
    {
        hud.SetScore(currentScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //virtual to override => look for LevelMoves.cs
    public virtual void GameWin()
    {
        Debug.Log("You Win");
        grid.GameOver();
        hud.OnGameWin(currentScore);
    }
    public virtual void GameLose()
    {
        Debug.Log("You Lose");
        grid.GameOver();
        hud.OnGameLose();
    }

    //to count player moves for the move count
    public virtual void OnMove()
    {
        Debug.Log("You Moved");
    }

    public void OnRowMatched(int score)
    {
        currentScore = score;
        hud.SetScore(currentScore);
        Debug.Log("CurrentScore= " + currentScore);
    }
}
