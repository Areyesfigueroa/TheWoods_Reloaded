using UnityEngine;
using System.Collections;

public class Win_Trigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			ScenesManager.Instance.LoadScene (3);

		}

	}
}
