using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Level level;

    public UnityEngine.UI.Text remainingText;
    public UnityEngine.UI.Text remainingSubText;
    public UnityEngine.UI.Text scoreText;

    private bool isGameOver = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();

    }

    public void SetRemaining(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void OnGameWin(int score)
    {
        isGameOver = true;
    }

    public void OnGameLose()
    {
        isGameOver = true;
    }
}
