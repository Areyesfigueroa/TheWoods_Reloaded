using UnityEngine;
using System.Collections;
using TheWoods.Player;

namespace TheWoods.Manager
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager Instance { get { return instance; } } //getter for instance
        static protected PlayerManager instance; //declaring instance variable

        private bool isDead = false;
        public float playerMaxSpeed = 10;
        public Animator anim;

        //keep track of the people collected
        private int peopleKilled;
        public int PeopleKilled
        {
            get { return peopleKilled; }
            set { peopleKilled = value; }
        }

        void Start()
        {
            peopleKilled = 0;
        }

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("There is alreade a PlayerManager in play. Deleting old, instantiating new");
                Destroy(PlayerManager.Instance.gameObject);
                instance = null;
            }
            else
            {
                instance = this;
            }
        }

        void Update()
        {
            anim.SetBool("isDead", isDead);
            anim.SetFloat("Speed", Mathf.Abs(Player.PlayerController.Instance.Velocity.x) / playerMaxSpeed);

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                Attack();
            }
        }

        void Attack()
        {
            anim.SetTrigger("Attack");
            //		AkSoundEngine.PostEvent ("Collection", gameObject);
        }

    }
}
