using UnityEngine;
using System.Collections;
using TheWoods.Audio;
using TheWoods.Manager;

namespace TheWoods.Player
{
    public class Attack : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("Working");
                PlayerManager.Instance.PeopleKilled++;
                AudioEventSystem.EnemyCapture();
                Destroy(other.gameObject, .2f);
            }
        }
    }
}
