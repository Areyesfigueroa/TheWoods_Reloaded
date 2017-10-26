using UnityEngine;
using System.Collections;
using TheWoods.Manager;

namespace TheWoods.UI
{
    public class ScoreCount : MonoBehaviour
    {

        static int score = 0;
        public float timer = 180f; //3min, 180sec
        float finalTimer = 0;
        string strTimer;

        public static ScoreCount Instance { get { return _instance; } } //getter for instance
        static protected ScoreCount _instance; //declaring instance variable

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("There is already a ScoreCount in play. Deleting old, instantiating new");
                Destroy(ScoreCount.Instance.gameObject);
                _instance = null;
            }
            else
            {
                _instance = this;
            }
        }

        void OnGUI()
        {
            finalTimer = timer - Time.time;
            score = PlayerManager.Instance.PeopleKilled;
            GUI.Label(new Rect(10, 10, 100, 20), ("Victims: " + score));

            //GUI.Label(new Rect(10, 10, 250, 100), strTimer);
            GUI.Label(new Rect(100, 10, 200, 20), ("Time Until Daylight: " + (finalTimer)));

            checkTimer();
        }

        void checkTimer()
        {
            if (finalTimer <= 0.0f)
            {
                //DeathScene
                //ScenesManager.Instance.LoadScene (2);
            }
        }
    }
}
