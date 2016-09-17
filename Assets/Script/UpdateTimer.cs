using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateTimer : MonoBehaviour {

	private GameplayController gameController;
	public Text timerText;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Controller")
					   .GetComponent<GameplayController>();
		timerText = GameObject.Find("TimerText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
                timerText.text = string.Format("{0:0}", gameController.time);
	}
}
