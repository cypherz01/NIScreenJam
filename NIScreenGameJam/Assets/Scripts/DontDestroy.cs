using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DontDestroy : MonoBehaviour

{
    public GameObject rootCanvas;

    void Awake()
    {
        DontDestroyOnLoad(rootCanvas);
    }

}



   