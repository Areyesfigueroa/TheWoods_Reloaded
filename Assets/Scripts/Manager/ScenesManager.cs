using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesManager : MonoBehaviour {

	public static ScenesManager Instance { get { return _instance; } } //getter for instance
	static protected ScenesManager _instance; //declaring instance variable

	void Awake()
	{
		if (_instance != null)
		{
			Debug.LogWarning("There is already a ScenesManager in play. Deleting old, instantiating new");
			Destroy(ScenesManager.Instance.gameObject);
			_instance = null;
		}
		else
		{
			_instance = this;
		}
	}

	public void LoadScene(int scene)
	{
		SceneManager.LoadScene (scene);
	}

}
