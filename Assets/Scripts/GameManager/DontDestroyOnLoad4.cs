using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad4 : MonoBehaviour
{
    private static DontDestroyOnLoad4 mainScene;
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
