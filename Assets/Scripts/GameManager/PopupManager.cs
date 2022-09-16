using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject mainScene;
    public GameObject popupScene;
    public bool isFromLevel = false;
 
    void Start()
    {

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFromLevel)
        {
            mainScene.SetActive(false);
            popupScene.SetActive(true);
            isFromLevel = false;
        }

    }
}
