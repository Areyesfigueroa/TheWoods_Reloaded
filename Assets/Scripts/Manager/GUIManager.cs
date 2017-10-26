using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//GUI
	public Text victimCounter;
	public Text timeCounter;
	public Image attackImage;
	public Image invisibleImage;

	public float deleteUITimer;
	//score
	int score;

	//How long should the timer be
	public float timerLength = 180.0f; //default
	float currentTimer;

	public static GUIManager Instance { get { return _instance; } } //getter for instance
	static protected GUIManager _instance; //declaring instance variable

	void Awake()
	{
		if (_instance != null)
		{
			Debug.LogWarning("There is already a GUIManager in play. Deleting old, instantiating new");
			Destroy(GUIManager.Instance.gameObject);
			_instance = null;
		}
		else
		{
			_instance = this;
		}
	}

	void Start()
	{
		invisibleImage.enabled = false;
		destroyUI (attackImage);
	}

	void Update()
	{
		currentTimer = timerLength - Time.time;
		timeCounter.text = currentTimer.ToString ();

		if (currentTimer <= 0) 
		{
			ScenesManager.Instance.LoadScene (2);
		}

		score = PlayerManager.Instance.PeopleKilled;
		victimCounter.text = score.ToString();
	}
		
	//Helper
	void destroyUI(Image img)
	{
		Destroy (img, deleteUITimer);
	}
	public void displayPowerUpUI()
	{
		invisibleImage.enabled = true;
		destroyUI (invisibleImage);
	}
}
