using UnityEngine;
using System.Collections;

public class MatchBarToHealth : MonoBehaviour {

	private GameplayController gameController;
	private MeshFilter meshFilter;
	private float worldWidth;
	private const float BAR_HEIGHT=0.3f;
	private Camera camera;


	// Use this for initialization
	void Start () {
		gameController=GetComponentInParent<GameplayController>();
		meshFilter=GetComponent<MeshFilter>();
//		gameObject.transform.position.Set(0,0,0);
		camera=Camera.main;

		//viewport coordinates go from (0,0) to (1,1)
		worldWidth=camera.ViewportToWorldPoint(new Vector2(1,0)).x + (-1*camera.ViewportToWorldPoint(new Vector2(0,0)).x) ; 
		Debug.Log("start width "+worldWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController!=null){
			float health=gameController.health;
			float width=worldWidth*(health/100);
//			Debug.Log("width is "+width);
			meshFilter.mesh=GetBarMeshrOfWidth(width);
		}
	}

	private Mesh GetBarMeshrOfWidth(float width){
		Mesh mesh=new Mesh();
		mesh.vertices=new Vector3[]{
			
			new Vector3(0,0,0),
			new Vector3(0,BAR_HEIGHT,0),
			new Vector3(width,0,0),
			new Vector3(width,BAR_HEIGHT,0),		
		};
		Vector3 lastV=mesh.vertices[3];
		mesh.uv=new Vector2[]{
			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0)
		};
		mesh.triangles=new int[]{0,1,2,1,3,2};
		return mesh;
	}
}
