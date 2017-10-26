using UnityEngine;
using System.Collections;
using TheWoods.Manager;

namespace TheWoods.UI
{
    public class Win_Trigger : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                ScenesManager.Instance.LoadScene(3);

            }

        }
    }

}
