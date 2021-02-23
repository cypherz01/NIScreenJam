using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace _Complete
{
    public class Pickup : MonoBehaviour

    {
        public Text countText;
        public Text countText2;
        public Text TotalScoreText;
        private int count;
        private int parcelcount;
        private int totalscore;
       
      
        public bool destroyed;
        AudioSource audiosrc;

        void Start()
        {
            destroyed = false;
            count = 0;
            parcelcount = 0;
            totalscore = 0;
            SetCountText();
          
            audiosrc = GameObject.Find("AudioManager_pickup").GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            destroyed = false;
        }
        void OnTriggerEnter2D(Collider2D other)
        {

            if (destroyed == false && other.tag == "Letter")
            {
                destroyed = true;
                Destroy(other.gameObject);
                count = count + 1;
                totalscore = totalscore + 1;
                SetCountText();
                SetTotalScoreText();
                audiosrc.Play();
            }

            if (destroyed == false && other.tag == "Parcel")
            {
                destroyed = true;
                Destroy(other.gameObject);
                parcelcount = parcelcount + 1;
                totalscore = totalscore + 1;
                SetCountText2();
                SetTotalScoreText();
                audiosrc.Play();
            }

            if(other.tag == "end")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        void SetCountText()
        {
            countText.text = "" + count.ToString();
        }

        void SetCountText2()
        {
            countText2.text = "" + parcelcount.ToString();
        }

        void SetTotalScoreText()
        {
            TotalScoreText.text = "Total: " + totalscore.ToString();
        }
      
    }
  
}
