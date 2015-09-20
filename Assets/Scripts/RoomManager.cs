using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public int NumberOfItems;
	public string Name;
	int numberOfTidiedItems = 0;

	void OnEnable() {
		EventManager.OnItemTidied += OnItemTidied;
		ProgressTracker.AddRoom (Name, NumberOfItems);
	}

	void OnDisable() {
		EventManager.OnItemTidied -= OnItemTidied;
	}

	void OnItemTidied () {
		if (++numberOfTidiedItems == NumberOfItems) {
			LayerDisplayManager.Show ("RoomThumbnails");
			EventManager.RoomAction (Name, EventManager.RoomEventType.roomComplete, numberOfTidiedItems, NumberOfItems);
			Destroy (gameObject);
		} else {
			EventManager.RoomAction (Name, EventManager.RoomEventType.itemTidied, numberOfTidiedItems, NumberOfItems);
		}
	}
}
