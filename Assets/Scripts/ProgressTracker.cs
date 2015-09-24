using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProgressTracker : MonoBehaviour {

	public GameObject Thumbnails;
	public Transform RoomTick;

	private int roomCount = 3;
	private int completeRoomCount = 0;

	private static bool created = false;

	static Dictionary <string, RoomProgress> roomProgress = new Dictionary<string, RoomProgress>();


	void Awake() {
		if (created) {
			Destroy (transform.gameObject);
		} else {
			DontDestroyOnLoad (transform.gameObject);
			created = true;
		}
	}

	void OnEnable() {
		EventManager.OnRoomAction += OnRoomAction;
		EventManager.OnTimeUp += OnTimeUp;
	}

	void OnDisable() {
		EventManager.OnRoomAction -= OnRoomAction;
		EventManager.OnTimeUp -= OnTimeUp;
	}

	void OnRoomAction (string roomName, EventManager.RoomEventType type, int itemCount, int totalItemCount) {
		RoomProgress progress;
		roomProgress.TryGetValue (roomName, out progress);
		switch (type) {
		case EventManager.RoomEventType.itemTidied:
			progress.CompleteItemCount++;
			break;
		case EventManager.RoomEventType.roomComplete:
			progress.CompleteItemCount = totalItemCount;
			Vector3 thumbnailPosition =  Thumbnails.transform.Find (roomName).transform.position;
			Instantiate (RoomTick, thumbnailPosition, new Quaternion ());
			if (++completeRoomCount == roomCount) {
				Application.LoadLevel ("Results");
			}
			break;
		}
		roomProgress [roomName] = progress;
	}

	void OnLevelWasLoaded (int sceneIndex) {
		if (sceneIndex == 3) { // results scene was loaded
			foreach (KeyValuePair <string, RoomProgress> progressItem in roomProgress) {
				Debug.Log(string.Format ("{0}: {1} of {2}", progressItem.Key, progressItem.Value.CompleteItemCount, progressItem.Value.TotalItems));
			}
		}
	}

	public static void AddRoom (string roomName, List<string>itemNames) {
		Debug.Log ("Add " + roomName);
		roomProgress.Add (roomName, new RoomProgress (roomName, itemNames));
	}

	public static RoomProgress GetRoomProgress(string roomName) {
		Debug.Log ("Get progress for " + roomName);
		return roomProgress [roomName];
	}

	public static void SetItemComplete(string roomName, string itemName) {
		RoomProgress roomProg = GetRoomProgress (roomName);
		ItemProgress itemProg = roomProg.Items.Find (i => i.Name == itemName);
		itemProg.IsComplete = true;
		Debug.Log (itemProg.Name + " was set to complete");
	}

	void OnTimeUp() {
		Application.LoadLevel ("Results");
	}
}
