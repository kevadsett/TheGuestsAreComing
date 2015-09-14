using UnityEngine;
using System.Collections;

public class TimeMoveComponent : MonoBehaviour {

	float startingX;

	void Start() {
		Debug.Log (transform.position);
		startingX = Mathf.Abs (transform.position.x);
	}

	void OnEnable() {
		EventManager.OnSecondTick += moveAlong;
	}

	void OnDisable() {
		EventManager.OnSecondTick -= moveAlong;
	}

	void moveAlong(int currentSecond) {
		float newX = ((2.0f * startingX) / 30.0f * (float)currentSecond) - startingX;
		Debug.Log (newX);
		transform.position = new Vector3 (newX, 2.88f, 0.0f);
	}
}
