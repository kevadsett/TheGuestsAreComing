using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : MonoBehaviour {
	
	public string Name;
	public List<string> Items;
	int numberOfTidiedItems = 0;

	public static GameObject CurrentRoom;
	public static string CurrentRoomName;

	static List<string> ExistingNames = new List<string>();

	void Awake() {
		
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
				Debug.Log (item.Name + ": " + item.IsComplete);
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
			LayerDisplayManager.Show ("RoomThumbnails");
			EventManager.RoomAction (Name, EventManager.RoomEventType.roomComplete, numberOfTidiedItems, Items.Count);
			Destroy (gameObject);
		} else {
			EventManager.RoomAction (Name, EventManager.RoomEventType.itemTidied, numberOfTidiedItems, Items.Count);
		}
	}
}
