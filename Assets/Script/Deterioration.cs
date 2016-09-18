using UnityEngine;
using System.Collections;

public class Deterioration : MonoBehaviour {

	public Sprite calm;
	public Sprite warning;
	public Sprite danger;
	private GameplayController gameController;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		gameController=GetComponentInParent<GameplayController>();
		spriteRenderer=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController!=null){
			float health=gameController.health;

			//we modify the saturation of a yellow theme to tint the sprite
			Color tint=Color.HSVToRGB(38/360,(100-Mathf.Floor(health))/255,255/255);
			tint.a=1;
//			Debug.Log("component "+tint.r);
			spriteRenderer.color=tint;

			//change the sprite based on the current health
			if(health>66){
				//calm
				spriteRenderer.sprite=calm;
			}else if(health<=66 && health>=33){
				//warning
				spriteRenderer.sprite=warning;
			}else{
				//danger
				spriteRenderer.sprite=danger;
			}
		}
	}
}
