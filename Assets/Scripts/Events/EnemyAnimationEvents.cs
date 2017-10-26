using UnityEngine;
using System.Collections;

public class EnemyAnimationEvents : MonoBehaviour {

    //Enemy Animation events, works
    //Plays when the animation is fired

	void Start (){
		print ("Hello Is It me Your looking for?");
	}

	//Done
    public void onEnemyStep()
    {
        Debug.Log("Enemy Move event Fired");
        AudioEventSystem.EnemyStep();
    }

    //No Animations
    public void onEnemyIdle()
    {
        Debug.Log("Enemy Idle event Fired");
        AudioEventSystem.EnemyIdle();
    }

    //No Animations
    public void onEnemyAlert()
    {
        Debug.Log("Enemy Alert event Fired");
        AudioEventSystem.EnemyAlert();
    }

    //No Animations
    public void onEnemyEscape()
    {
        Debug.Log("Enemy Escape Fired");
        AudioEventSystem.EnemyEscape();
    }

}
