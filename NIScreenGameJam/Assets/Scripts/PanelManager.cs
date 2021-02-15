using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] Cams;

    public int i;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        y = 0;
        
    }

    // Update is called once per frame
    public GameObject goLeft(string name)
    {
        if (name == "pos") {
           
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
