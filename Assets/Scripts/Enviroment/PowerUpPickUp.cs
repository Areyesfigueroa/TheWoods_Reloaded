using UnityEngine;
using System.Collections;
using TheWoods.Audio;
using TheWoods.Player;
using TheWoods.Manager;

namespace TheWoods.Enviroment
{
    public class PowerUpPickUp : MonoBehaviour
    {

        [Header("Lenght of the final position")]
        public float destination;
        Vector2 endPos;
        [Header("Time it takes until destination")]
        public float timeTakenDuringLerp = 1;

        Vector2 startPos;
        float timeStartedLerping;

        // Use this for initialization
        void Start()
        {
            startPos = transform.position;
            endPos = new Vector2(transform.position.x, destination);
            timeStartedLerping = Time.time;

            AudioEventSystem.PowerUpIdle(); //loop the audio
        }

        // Update is called once per frame
        void Update()
        {
            Animation();
            Debug.DrawLine(startPos, endPos, Color.blue);
        }

        void Animation()
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = (timeSinceStarted / timeTakenDuringLerp);

            //goes up and down
            transform.position = Vector2.Lerp(startPos, endPos, Mathf.PingPong(percentageComplete, 1f));
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                //Call Pick up AudioEvent
                Debug.Log("Collided");
                AudioEventSystem.PowerUpPickUp();//play audio event
                Controller2D.Instance.activatePowerUp();
                GUIManager.Instance.displayPowerUpUI();
                Destroy(this.gameObject, 0.2f);
            }
        }
    }
}