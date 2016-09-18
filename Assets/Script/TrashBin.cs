using UnityEngine;
using System.Collections;

public class TrashBin : MonoBehaviour {

	public int category;//0 for recyclable,1 for compost, 2 for landfill
	public GameObject checkMark;
	public GameObject crossMark;

	private GameplayController gameController;

	// Use this for initialization
	void Start () {
		gameController=GameObject.Find("Controller").GetComponent<GameplayController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if ((coll.gameObject.tag == "Recyclable" && category==0)
			||(coll.gameObject.tag == "Compost" && category==1)
			||(coll.gameObject.tag == "Landfill" && category==2)){
			gameController.SendMessage("CorrectAnswer", coll.gameObject);
			//Throw a check mark
			GameObject correctHint=Instantiate(checkMark,transform.position,Quaternion.identity) as GameObject;
			Destroy(correctHint,0.5f);
		}else{
			gameController.SendMessage("WrongAnswer", coll.gameObject);
			//Throw a cross mark
			GameObject wrongHint=Instantiate(crossMark,transform.position,Quaternion.identity) as GameObject;
			Destroy(wrongHint,0.5f);
		}
	}
}
