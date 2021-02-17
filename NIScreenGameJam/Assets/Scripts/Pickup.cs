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
        private int parcelcount;
        public bool destroyed;

        AudioSource audiosrc;

        void Start()
        {

            destroyed = false;
            count = 0;
            parcelcount = 0;
            SetCountText();
            audiosrc = GameObject.Find("AudioManager_pickup").GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            destroyed = false;
        }
        void OnTriggerExit2D(Collider2D other)
        {

            if (destroyed == false && other.tag == "Letter")
            {
                destroyed = true;
                Destroy(other.gameObject);
                count = count + 1;
                SetCountText();
                audiosrc.Play();
               

            }

            if (destroyed == false && other.tag == "Parcel")
            {
                destroyed = true;
                Destroy(other.gameObject);
                parcelcount = parcelcount + 3;
                SetCountText2();
                audiosrc.Play();


            }
        }
        void SetCountText()
        {
            countText.text = "" + count.ToString ();
        }

        void SetCountText2()
        {
            countText2.text = "" + parcelcount.ToString();
        }
    }
}
