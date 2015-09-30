using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : MonoBehaviour {
	
	public string Name;
	public List<GameObject> Items;
	int numberOfTidiedItems = 0;

	public static GameObject CurrentRoom;
	public static string CurrentRoomName;
	public static float CurrentRoomAge;

	public static List<string> ExistingNames = new List<string>();

	void Awake() {
		
	}

	void Update() {
		CurrentRoomAge += Time.deltaTime;
	}

	void OnEnable() {
		EventManager.OnItemTidied += OnItemTidied;
		if (!ExistingNames.Contains (Name)) {
			ProgressTracker.AddRoom (Name, Items);
			ExistingNames.Add (Name);
		} else {
			RoomProgress progress = ProgressTracker.GetRoomProgress (Name);
			foreach (ItemProgress item in progress.Items) {
				if (item.IsComplete) {
					numberOfTidiedItems++;
					Transform child = transform.Find (item.Name);
					StateManager stateManager = child.GetComponent<StateManager> ();
					stateManager.SetState ("complete");
					ConnectedItemComponent connectedItemComponent = child.GetComponent<ConnectedItemComponent> ();
					if (connectedItemComponent != null && connectedItemComponent.ConnectedTo != null) {
						connectedItemComponent.ConnectedTo.GetComponent<StateManager> ().SetState ("complete");
					}
				}
			}
		}

		CurrentRoom = transform.gameObject;
		CurrentRoomName = Name;
	}

	void OnDisable() {
		EventManager.OnItemTidied -= OnItemTidied;
	}

	void OnItemTidied () {
		if (++numberOfTidiedItems == Items.Count) {
			EventManager.RoomAction (Name, EventManager.RoomEventType.roomComplete, numberOfTidiedItems, Items.Count);
		} else {
			EventManager.RoomAction (Name, EventManager.RoomEventType.itemTidied, numberOfTidiedItems, Items.Count);
		}
	}

	public static void RemoveCurrentRoom() {
		CurrentRoom = null;
		CurrentRoomName = null;
		CurrentRoomAge = 0.0f;
	}

	public static void Reset() {
		RemoveCurrentRoom ();
		ExistingNames.Clear ();
	}
}
