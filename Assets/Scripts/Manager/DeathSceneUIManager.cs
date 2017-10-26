using UnityEngine;
using System.Collections;
using TheWoods.Audio;

namespace TheWoods.Manager
{
    public class DeathSceneUIManager : MonoBehaviour
    {

        //Death Scene
        public void onTryAgainButton()
        {
            AudioEventSystem.ButtonPress();
            ScenesManager.Instance.LoadScene(1);
        }

        public void onMainMenuButton()
        {
            AudioEventSystem.ButtonPress();
            ScenesManager.Instance.LoadScene(0);
        }
    }
}
