using UnityEngine;
using System.Collections;

public class MatchBarToHealth : MonoBehaviour {

	private float initialScaleX;
	private GameplayController gameController;

	// Use this for initialization
	void Start () {
		initialScaleX=this.transform.localScale.x;
		gameController=GetComponentInParent<GameplayController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController!=null){
			float health=gameController.health;
			transform.localScale=new Vector3(initialScaleX*(health/100),transform.localScale.y,1);
		}
	}
}
