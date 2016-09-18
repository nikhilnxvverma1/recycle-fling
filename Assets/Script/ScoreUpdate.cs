using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUpdate : MonoBehaviour {

	public static float timeFromLastLevel;

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
//		Text score = GetComponent<Text>();
//		scoreLabel.text = string.Format("You lasted {0}s", timeFromLastLevel);
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = string.Format("You lasted {0}s", Mathf.Floor(timeFromLastLevel));
	}
}
