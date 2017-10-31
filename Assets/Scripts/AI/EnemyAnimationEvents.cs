using UnityEngine;
using System.Collections;
using TheWoods.Audio;

namespace TheWoods.AI
{
    public class EnemyAnimationEvents : MonoBehaviour
    {

        //Enemy Animation events, works
        //Plays when the animation is fired

        //Done
        public void onEnemyStep()
        {
            AudioEventSystem.EnemyStep();
        }

        //No Animations
        public void onEnemyIdle()
        {
            AudioEventSystem.EnemyIdle();
        }

        //No Animations
        public void onEnemyAlert()
        {
            AudioEventSystem.EnemyAlert();
        }

        //No Animations
        public void onEnemyEscape()
        {
            AudioEventSystem.EnemyEscape();
        }

    }
}
