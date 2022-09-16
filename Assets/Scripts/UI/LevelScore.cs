using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour
{
    public int level;
    public FinalScore getScore;
    private int scoreValue;
    private Text scoreText;
 
    
    void Start()
    {
        scoreText = gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("ScoreHolder") != null)
        {
            getScore = GameObject.Find("ScoreHolder").GetComponent<FinalScore>();
            scoreValue = getScore.scoreArray[level - 1];
            if (scoreValue == 0)
            {
                scoreText.text = "No Score";

            }
            else
            {
                scoreText.text = "Highest Score: " + scoreValue.ToString();
                if (level != 15)
                {
                    GameObject.Find("Level" + (level + 1)).transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Level" + (level + 1)).transform.GetChild(1).gameObject.SetActive(false);
                }

            }
        }
    }
}
