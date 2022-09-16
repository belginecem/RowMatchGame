using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//derives from the level class
public class LevelMoves : Level
{
    public int numMoves;
    public int targetScore;
    public int level;
    private int movesUsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InfoStart", 0.1f);
    }

    private void Awake()
    {
        level = GameObject.Find("LoadLevel").GetComponent<LoadGame>().levelNumber;
    }
    public void InfoStart()
    {
        hud.SetScore(currentScore);
        hud.SetRemaining(numMoves);
        Debug.Log("Number of Moves: " + numMoves);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnMove()
    {
        movesUsed++;

        Debug.Log("Moves remaining: " + (numMoves - movesUsed));
        hud.SetRemaining(numMoves - movesUsed);

        if (numMoves - movesUsed == 0)
        {
           GameLose();
        }
    }
}
