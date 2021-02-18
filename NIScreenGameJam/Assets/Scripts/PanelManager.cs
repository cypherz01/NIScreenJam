using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] Cams;

    public int i;
    public int y;

    void Start()
    {
        i = 1;
        y = 0;
        
    }

    public GameObject goLeft(string name)
    {
        if (name == "pos") 
        {
            Debug.Log(i);
            return positions[i];
        }
        else if (name == "cam")
        {
            Debug.Log(y);
            return Cams[y];
        }
        return null;
    }

    public GameObject goRight(string name)
    {
        if (name == "pos")
        {
            Debug.Log(i);
            return positions[i];
        }
        else if (name == "cam")
        {
            Debug.Log(y);
            return Cams[y];
        }
        return null;
    }
}
