﻿using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public float time=0f;
	public float health=100f;
	public GameObject [] trashItems;
	private float healthDecaySpeed=1f;
	private float lastCheckpoint;
	private GameObject currentTrashItem;
	private Vector2 initialTouchPoint;
	private const float MIN_SWIPE_DISTANCE=0.5f;

	// Use this for initialization
	void Start () {
		lastCheckpoint=time;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTimeAndHealth();
		Vector2 swipeDirection=processMouseInput();
		if(swipeDirection!=Vector2.zero){
			Debug.Log("Swiped at "+swipeDirection.x+","+swipeDirection.y);
		}
		if(currentTrashItem==null){
			currentTrashItem=Instantiate(trashItems[0],new Vector3(0.02f,4.37f,0),Quaternion.identity) as GameObject;
		}

	}

	private void UpdateTimeAndHealth(){
		time+=Time.deltaTime;
		health-=healthDecaySpeed*Time.deltaTime*10;// TODO we can probably swap out the delta time for something else
		
		//every 10 seconds increase the speed
		if(time-lastCheckpoint>10){
			lastCheckpoint=time;
			healthDecaySpeed+=0.1f;
		}
	}

	private Vector2 processMouseInput(){
		if(Input.GetMouseButton(0)){
			if(Input.GetMouseButtonDown(0)){
				//press
				initialTouchPoint=Input.mousePosition;
			}else{
				//drag
				Vector2 currentTouchPoint=Input.mousePosition;
				float distance=Vector2.Distance(initialTouchPoint,currentTouchPoint);
				if(distance>=MIN_SWIPE_DISTANCE){
					return currentTouchPoint-initialTouchPoint;		
				}
			}

		}else{
			//release
		}
			
//			foreach(Touch touch in Input.touches){
//				switch(touch.phase){
//					case TouchPhase.Began:
//						initialTouchPoint=touch.position;
//						break;
//					case TouchPhase.Moved:
//						Vector2 currentTouchPoint=touch.position;
//						float distance=Vector2.Distance(initialTouchPoint,currentTouchPoint);
//						if(distance>=MIN_SWIPE_DISTANCE){
//							return currentTouchPoint-initialTouchPoint;		
//						}
//						break;
//					case TouchPhase.Ended:
//						break;
//					case TouchPhase.Canceled:
//						break;
//				}
//			}
//		}
		return Vector2.zero;
	}

	void OnMouseDown(){
		Debug.Log("Mouse down");
	}
}
