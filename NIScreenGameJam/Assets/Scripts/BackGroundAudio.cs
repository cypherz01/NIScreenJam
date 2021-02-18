using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAudio : MonoBehaviour
{
    public static BackGroundAudio bgAudio;

    private void Awake()
    {
        if(bgAudio != null && bgAudio != this)
        {
            Destroy(this.gameObject);
                return;
        }

        bgAudio = this;
        DontDestroyOnLoad(this);
    }
}
    
