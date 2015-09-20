using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressTracker : MonoBehaviour {

	public GameObject Thumbnails;
	public Transform RoomTick;

	private int roomCount = 3;
	private int completeRoomCount = 0;

	private static bool created = false;

	private struct roomProgressItem {
		public int completeItemCount, totalItems;

		public roomProgressItem(int itemCount, int total) {
			completeItemCount = itemCount;
			totalItems = total;
		}
	}

	static Dictionary <string, roomProgressItem> roomProgress = new Dictionary<string, roomProgressItem>();


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
	}

	void OnDisable() {
		EventManager.OnRoomAction -= OnRoomAction;
	}

	void OnRoomAction (string roomName, EventManager.RoomEventType type, int itemCount, int totalItemCount) {
		roomProgressItem progress;
		roomProgress.TryGetValue (roomName, out progress);
		switch (type) {
		case EventManager.RoomEventType.itemTidied:
			progress.completeItemCount++;
			break;
		case EventManager.RoomEventType.roomComplete:
			progress.completeItemCount = totalItemCount;
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
			foreach (KeyValuePair <string, roomProgressItem> progressItem in roomProgress) {
				Debug.Log(string.Format ("{0}: {1} of {2}", progressItem.Key, progressItem.Value.completeItemCount, progressItem.Value.totalItems));
			}
		}
	}

	public static void AddRoom (string roomName, int totalItemCount) {
		roomProgress.Add (roomName, new roomProgressItem (0, totalItemCount));
	}
}
