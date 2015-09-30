using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomiseRoomsComponent : MonoBehaviour {

	public List<Vector2> Positions;
	public List<GameObject> Thumbnails;

	void Start () {
		for (int i = 0; i < Thumbnails.Count; i++) {
			Vector2 chosenPosition = Positions [Random.Range (0, Positions.Count)];
			Positions.Remove (chosenPosition);
			GameObject thumbnail = (GameObject)Instantiate(Thumbnails [i], new Vector3 (chosenPosition.x, chosenPosition.y, 0.0f), new Quaternion());
			thumbnail.transform.SetParent (gameObject.transform);
		}
	}
}
