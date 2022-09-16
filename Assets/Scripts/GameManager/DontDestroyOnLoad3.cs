using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad3 : MonoBehaviour
{
    private static DontDestroyOnLoad3 mainScene;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (mainScene == null)
        {
            mainScene = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
