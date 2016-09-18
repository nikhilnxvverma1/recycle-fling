using UnityEngine;
using System.Collections;

public class TrashItem : MonoBehaviour {

	private const float targetScale=0.5f;
	private bool shrink=false;

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

	}

	public void StartShrinkinig(){
		shrink=true;
	}
}
