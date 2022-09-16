using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad2 : MonoBehaviour
{
    private static DontDestroyOnLoad2 mainScene;
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
