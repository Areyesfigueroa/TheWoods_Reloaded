using UnityEngine;
using System.Collections;

public class DeathSceneUIManager : MonoBehaviour {

	//Death Scene
	public void onTryAgainButton()
	{
        AudioEventSystem.ButtonPress();
		ScenesManager.Instance.LoadScene (1);
	}

	public void onMainMenuButton()
	{
        AudioEventSystem.ButtonPress();
		ScenesManager.Instance.LoadScene (0);
	}
}
