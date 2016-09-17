using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour {

  public float health = 100f;
  public float time = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  time += Time.deltaTime;
    health -= Time.deltaTime;
	}
}
