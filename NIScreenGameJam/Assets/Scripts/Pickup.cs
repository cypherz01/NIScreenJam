using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Complete
{
    public class Pickup : MonoBehaviour
    {
        public Text countText;
        public Text countText2;
        private int count;

        void Start()
        {
            count = 0;
            SetCountText ();
            SetCountText2 ();
        }
         void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Letter")
            {
               
                //coinText.text = Count.ToString();
                Destroy(other.gameObject);
                count = count + 1;
                SetCountText ();

            }

            if (other.tag == "Parcel")
            {
                Destroy(other.gameObject);
                count = count + 3;
                SetCountText2 ();


            }
        }

        void SetCountText()
        {
            countText.text = " " + count.ToString();
        }

        void SetCountText2()
        {
            countText2.text = " " + count.ToString();
        }
    }

}
