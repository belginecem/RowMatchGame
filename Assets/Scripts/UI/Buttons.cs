using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public GameObject currentScene;
    public GameObject nextScene;
    void OnMouseDown()
    {
        GameObject.Find("PopupManager").GetComponent<PopupManager>().isFromLevel = false;
        print("clicked");
        nextScene.SetActive(true);
        currentScene.SetActive(false);
        //Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
