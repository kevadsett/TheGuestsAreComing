using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProgressTracker : MonoBehaviour {

	public GameObject Thumbnails;
	public Transform RoomTick;

	private int roomCount = 4;
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

	public static void AddRoom (string roomName, List<GameObject>items) {
		roomProgress.Add (roomName, new RoomProgress (roomName, items));
	}

	public static RoomProgress GetRoomProgress(string roomName) {
		return roomProgress [roomName];
	}

	public static bool SetItemComplete(string roomName, string itemName) {
		RoomProgress roomProg = GetRoomProgress (roomName);
		ItemProgress itemProg = roomProg.Items.Find (i => i.Name == itemName);
		if (itemProg.IsComplete) {
			return false;
		} else {
			itemProg.IsComplete = true;
			return true;
		}
	}

	void OnTimeUp() {
		Application.LoadLevel ("Results");
	}

	public void Reset() {
		completeRoomCount = 0;
		roomProgress.Clear ();
	}

	void OnLevelWasLoaded(int index) {
		if (index == 1) { // main scene
			Thumbnails = GameObject.Find ("Thumbnails");
		}
	}
}
