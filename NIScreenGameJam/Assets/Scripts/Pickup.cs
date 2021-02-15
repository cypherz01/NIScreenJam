using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Complete
{
<<<<<<< Updated upstream
    public class Pickup : MonoBehaviour
    {
        public Text countText;
        public Text countText2;
=======
    public class Pickup : MonoBehaviour { 

        public Text countText;
    
>>>>>>> Stashed changes
        private int count;

        void Start()
        {
            count = 0;
            SetCountText ();
<<<<<<< Updated upstream
            SetCountText2 ();
=======

>>>>>>> Stashed changes
        }
         void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Letter")
            {
<<<<<<< Updated upstream
               
                //coinText.text = Count.ToString();
                Destroy(other.gameObject);
                count = count + 1;
                SetCountText ();

=======
                count = count + 1;
                //coinText.text = Count.ToString();
                Destroy(other.gameObject);
               
                SetCountText ();
>>>>>>> Stashed changes
            }


            if (other.tag == "Parcel")
            {
<<<<<<< Updated upstream
                Destroy(other.gameObject);
                count = count + 3;
                SetCountText2 ();

=======

                count = count + 3;
                Destroy(other.gambeObject);

                SetCountText2();
>>>>>>> Stashed changes

            }
        }

        void SetCountText()
        {
<<<<<<< Updated upstream
            countText.text = " " + count.ToString();
        }

        void SetCountText2()
        {
            countText2.text = " " + count.ToString();
        }
    }

=======
            countText.text = " " + count.ToString ();
        }

    }

   
>>>>>>> Stashed changes
}


