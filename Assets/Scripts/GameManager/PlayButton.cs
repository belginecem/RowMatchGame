using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public int setLevel;
    void OnMouseDown()
    {
        GameObject.Find("LoadLevel").GetComponent<LoadGame>().levelNumber = setLevel;
        GameObject.Find("LevelsPopup").SetActive(false);
        SceneManager.LoadScene("LevelScene");
    }
}
