using UnityEngine;
using System.Collections;

public class TrashBin : MonoBehaviour {

	public int category;//0 for recyclable,1 for compost, 2 for landfill

	private GameplayController gameController;

	// Use this for initialization
	void Start () {
		gameController=GameObject.Find("Controller").GetComponent<GameplayController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if ((coll.gameObject.tag == "Recylable" && category==0)
			||(coll.gameObject.tag == "Compost" && category==1)
			||(coll.gameObject.tag == "Compost" && category==2)){
			gameController.SendMessage("CorrectAnswer", coll.gameObject);
		}else{
			gameController.SendMessage("WrongAnswer", coll.gameObject);
		}
	}
}
