using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public GameObject end;
    public AudioSource victorysound;

    private void OnDestroy()
    {
        end.SetActive(true);
        victorysound.Play();
    }
}
