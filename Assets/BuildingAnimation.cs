using UnityEngine;
using System.Collections;

public class BuildingAnimation : MonoBehaviour {
  
  private GameObject buildingParent;
  private Component[] buildingRenderers;
  private int delta = 1;

	// Use this for initialization
	void Start() {
    buildingParent = GameObject.Find("BuildingParent");
    buildingRenderers = buildingParent.GetComponentsInChildren(typeof(Renderer));
	}
	
	// Update is called once per frame
	void Update() {
    foreach(Renderer renderer in buildingRenderers) {
      Color old = renderer.material.color;
      if (old.a > 1) {
        delta = -1;
      } else if (old.a < 0) {
        delta = 1;
      }
      old.a += 0.1f * delta;
      renderer.material.color = old;
    }
	}
}
