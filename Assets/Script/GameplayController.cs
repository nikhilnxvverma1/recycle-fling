using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public float time=0f;
	public float health=100f;
	private float healthDecaySpeed=1f;
	private float lastCheckpoint;

	// Use this for initialization
	void Start () {
		lastCheckpoint=time;
	}
	
	// Update is called once per frame
	void Update () {
		time+=Time.deltaTime;
		health-=healthDecaySpeed*Time.deltaTime;// TODO we can probably swap out the delta time for something else

		//every 10 seconds increase the speed
		if(time-lastCheckpoint>10){
			lastCheckpoint=time;
			healthDecaySpeed+=0.1f;
		}
	}
}
