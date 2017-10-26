﻿using UnityEngine;
using System.Collections;

public class WinSceneUIManager : MonoBehaviour {

    private void Start()
    {
        //Win Music
        AudioEventSystem.PlayerVictory();
    }

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
