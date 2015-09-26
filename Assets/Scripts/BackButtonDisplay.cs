using UnityEngine;
using System.Collections;

public class BackButtonDisplay : MonoBehaviour {

	SpriteRenderer spriteRenderer; 
	
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		Show (false);
	}

	void OnEnable() {
		EventManager.OnDisplayBackButton += Show;
	}

	void OnDisable() {
		EventManager.OnDisplayBackButton -= Show;
	}

	void Show (bool show) {
		spriteRenderer.enabled = show;
	}

}
