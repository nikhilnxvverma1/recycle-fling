using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	private float lastSpawnTime=0;
	private float spawnGap=5;
//	private ArrayList<GameObject> currentTrashItems=new GameObject[50];
//	private int currentTrashItemsLength;
	
	// Use this for initialization
	void Start () {
		lastCheckpoint=time;

	}
	
	// Update is called once per frame
	void Update () {
		

		UpdateTimeAndHealth();
		Vector2 swipeDirection=processMouseInput();
		if(swipeDirection!=Vector2.zero && !ignoreCurrentFling){
			applyForceToFallingItems(swipeDirection);
		}

//		if(time-lastSpawnTime>spawnGap){
//			lastSpawnTime=time;
//			//TODO spawn new item
//			GameObject trash=CreateNewTrash();
//			currentTrashItems[currentTrashItemsLength++]=trash;
//		}

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
			spawnGap-=0.2f;//not used
		}

		if(health<0){
			Debug.Log("Loading game over scene");
			SceneManager.LoadScene(3,LoadSceneMode.Single);
		}
	}
	
	private void applyForceToFallingItems(Vector2 direction){

		Vector3 finalPosition;

//		if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y)){
//			//more shift in along the x axis
//			if(direction.x<0){
//				//left
//				Debug.Log("Left");
//				finalPosition=recycleBin.transform.position;
//			}else{
//				//right
//				Debug.Log("Right");
//				finalPosition=landfillBin.transform.position;
//			}
//		}else{
//			//more shift along the vertical axis
//			if(direction.y<0){
//				//down
//				Debug.Log("Down");
//				finalPosition=compostBin.transform.position;
//			}else{
//				//up
//				Debug.Log("Up");
//				finalPosition=compostBin.transform.position;
//			}
//		}
//
		GameObject bin=CorrectBinForFling(direction);//Check inside a funnel
		finalPosition=bin.transform.position;

		//old way of doing applying force and hoping it will reach in time
//		Vector2 moveDirection=finalPosition-currentTrashItem.transform.position;
//		currentTrashItem.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized*300);
//		currentTrashItem.GetComponent<TrashItem>().StartShrinkinig();
//		Rigidbody2D rigidBody=currentTrashItem.GetComponent<Rigidbody2D>();
//		rigidBody.gravityScale=1;

		//new way involves manually calculating the position between two points in each update 
		//over a given duration. This makes it independent of screen resolution
		currentTrashItem.GetComponent<TrashItem>().MoveAndShrinkTo(finalPosition,0.3f);
		
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

		return Vector2.zero;
	}

	private GameObject CorrectBinForFling(Vector2 direction){
		Vector2 finalPosition=initialTouchPoint+direction;
		Vector3 fallingItemPosition=currentTrashItem.transform.position;

		//calculate the funnel angle range for the middle bin
		Vector3 middleBinPosition=compostBin.transform.position;

		//get the bin dimensions in world space
	
//		RectTransform rectTransform=(RectTransform)compostBin.transform;
		float binWidth=1.85f;
		float binHeight=1.13f;
		Debug.Log(" middle bin dimensions= "+binWidth+" "+binHeight);

		Vector3 leftEdge=new Vector3(middleBinPosition.x,middleBinPosition.y,middleBinPosition.z);
		leftEdge.x-=binWidth;

		Vector3 rightEdge=new Vector3(middleBinPosition.x,middleBinPosition.y,middleBinPosition.z);
		rightEdge.x+=binWidth;

		Vector3 leftVector=leftEdge-fallingItemPosition;
		Vector3 rightVector=rightEdge-fallingItemPosition;

		//normalize vectors left,right and direction before comparing x values
		leftVector.Normalize();
		rightVector.Normalize();
		direction.Normalize();

		if(direction.x<leftVector.x){
			return recycleBin;
		}else if(direction.x>rightVector.x){
			return landfillBin;
		}else{
			return compostBin;
		}
	}

	public void CorrectAnswer(GameObject from){
		
		Debug.Log("Correct answer");
		health=health+20>100?100:health+20;			
		DestroyOldAndCreateNewOne(from);
	}

	public void WrongAnswer(GameObject from){

		Debug.Log("Wrong answer");
		health-=20;
		if(health<0){
			SceneManager.LoadScene(3,LoadSceneMode.Single);
		}
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

	private GameObject CreateNewTrash(){
		int randomIndex=Random.Range (0,trashItems.Length);
		GameObject trash=Instantiate(trashItems[randomIndex],new Vector3(0.02f,5.37f,0),Quaternion.identity) as GameObject;
		trash.GetComponent<Rigidbody2D>().gravityScale=0.1f;
		return trash;
	}


}
