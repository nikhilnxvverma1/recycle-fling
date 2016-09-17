using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public void loadGameplayScene() {
		Application.LoadLevel("Gameplay");
	}

	public void loadInstructionsScene() {
		Application.LoadLevel("Instructions");
	}

	public void loadMenuScene() {
		Application.LoadLevel("Menu");
	}
}
