using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {
	public float time=0f;
	public float health=100f;
	public GameObject [] trashItems;
	private float healthDecaySpeed=0.4f;
	private float lastCheckpoint;
	private GameObject currentTrashItem;
	private Vector2 initialTouchPoint;
	private const float MIN_SWIPE_DISTANCE=0.5f;
	private bool ignoreCurrentFling=false;
	public GameObject recycleBin;
	public GameObject landfillBin;
	public GameObject compostBin;
	
	// Use this for initialization
	void Start () {
		lastCheckpoint=time;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTimeAndHealth();
		Vector2 swipeDirection=processMouseInput();
		if(swipeDirection!=Vector2.zero && !ignoreCurrentFling){
			//			Debug.Log("applying force");
//			Debug.Log("Swiped at "+swipeDirection.x+","+swipeDirection.y);
			applyForceToFallingItems(swipeDirection);
		}
		
		//only needed for first iteration
		if(currentTrashItem==null){
			currentTrashItem=Instantiate(trashItems[0],new Vector3(0.02f,5.37f,0),Quaternion.identity) as GameObject;
			currentTrashItem.GetComponent<Rigidbody2D>().gravityScale=0.1f;
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
	
	private void applyForceToFallingItems(Vector2 direction){

		Vector3 finalPosition;

		if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y)){
			//more shift in along the x axis
			if(direction.x<0){
				//left
				Debug.Log("Left");
				finalPosition=recycleBin.transform.position;
			}else{
				//right
				Debug.Log("Right");
				finalPosition=landfillBin.transform.position;
			}
		}else{
			//more shift along the vertical axis
			if(direction.y<0){
				//down
				Debug.Log("Down");
				finalPosition=compostBin.transform.position;
			}else{
				//up
				Debug.Log("Up");
				finalPosition=compostBin.transform.position;
			}
		}
		Vector2 moveDirection=finalPosition-currentTrashItem.transform.position;
		currentTrashItem.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized*100);
		currentTrashItem.GetComponent<TrashItem>().StartShrinkinig();
//		if(direction.y<0){
//			Rigidbody2D rigidBody=currentTrashItem.GetComponent<Rigidbody2D>();
//			rigidBody.AddForce(direction.normalized*100);
//			rigidBody.gravityScale=1;
//		}else{
//			//if the user swipes up , just drop the trash vertically down
//			Vector2 verticallyDown=new Vector2(0,-1);
//			Rigidbody2D rigidBody=currentTrashItem.GetComponent<Rigidbody2D>();
//			rigidBody.AddForce(verticallyDown.normalized*50);
//			rigidBody.gravityScale=1;
//		}
		
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
			ignoreCurrentFling=false;
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
	
	public void CorrectAnswer(GameObject from){
		
		Debug.Log("Correct answer");
		health=health+5>100?100:health+5;			
		DestroyOldAndCreateNewOne(from);
	}

	public void WrongAnswer(GameObject from){

		Debug.Log("Wrong answer");
		health=health-5<0?100:health-5;
		DestroyOldAndCreateNewOne(from);
	}

	private void DestroyOldAndCreateNewOne(GameObject toBeDestroyed){
		//destroy the current trash and instantiate a new random one
		Destroy(toBeDestroyed);
		int randomIndex=Random.Range (0,trashItems.Length);
		currentTrashItem=Instantiate(trashItems[randomIndex],new Vector3(0.02f,5.37f,0),Quaternion.identity) as GameObject;
		currentTrashItem.GetComponent<Rigidbody2D>().gravityScale=0.1f;
		ignoreCurrentFling=true;
	}
}
