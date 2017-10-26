using UnityEngine;
using System.Collections;

namespace TheWoods.Enviroment
{
    public class playerStick : MonoBehaviour
    {

        GameObject player; //testing

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Detected");
                other.transform.parent = transform;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Exiting");
                other.transform.parent = null;
            }
        }
    }
}
