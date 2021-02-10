using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Complete
{
    public class Pickup : MonoBehaviour
    {
        int Count;

        void Start()
        {
            Count = 0;
        }
         void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag =="Letter")
            {
                Count++;
                //coinText.text = Count.ToString();
                Destroy(other.gameObject);

            }

            if (other.tag == "Parcel")
            {
                

            }
        }
    }
}
