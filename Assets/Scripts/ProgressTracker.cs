using UnityEngine;
using System.Collections;

public class ProgressTracker : MonoBehaviour {

	public GameObject Thumbnails;
	public Transform RoomTick;

	int completeRoomCount = 0;

	void OnEnable() {
		EventManager.OnRoomComplete += OnRoomComplete;
	}

	void OnDisable() {
		EventManager.OnRoomComplete -= OnRoomComplete;
	}

	void OnRoomComplete (string roomName) {
		Vector3 thumbnailPosition =  Thumbnails.transform.Find (roomName).transform.position;
		Instantiate (RoomTick, thumbnailPosition, new Quaternion ());
		completeRoomCount++;
	}
}
