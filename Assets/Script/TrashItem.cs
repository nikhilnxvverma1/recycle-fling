using UnityEngine;
using System.Collections;

public class TrashItem : MonoBehaviour {

	private const float targetScale=0.3f;
	private bool shrink=false;
	private bool shrinkAndMove=false;
	private float shrinkAndMoveDuration;
	private Vector2 initialPosition;
	private Vector2 finalPosition;
	private float timeSurpassed=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(shrink){
			if(transform.localScale.x>targetScale){
				transform.localScale-=Vector3.one*Time.deltaTime/3;
			}
		}

		if(shrinkAndMove){
			timeSurpassed+=Time.deltaTime;
			float fraction=timeSurpassed/shrinkAndMoveDuration;
//			Debug.Log("Fraction "+fraction);

			Vector2 interpolated=Vector2.Lerp(initialPosition,finalPosition,fraction);
			transform.position=interpolated;

			Vector3 scale=Vector3.Lerp(Vector3.one,new Vector3(targetScale,targetScale,targetScale),fraction);
			transform.localScale=scale;
		}

	}

	public void StartShrinkinig(){
		shrink=true;
	}

	public void MoveAndShrinkTo(Vector2 position,float duration){
		shrinkAndMove=true;
		shrinkAndMoveDuration=duration;
		initialPosition=transform.position;
		finalPosition=position;
		timeSurpassed=0;
	}
}
