using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public int NumberOfItems;
	public string Name;
	int numberOfTidiedItems = 0;

	void OnEnable() {
		EventManager.OnItemTidied += OnItemTidied;
	}

	void OnDisable() {
		EventManager.OnItemTidied -= OnItemTidied;
	}

	void OnItemTidied () {
		if (++numberOfTidiedItems == NumberOfItems) {
			LayerDisplayManager.Show ("RoomThumbnails");
			EventManager.RoomComplete (Name);
			Destroy (gameObject);
		}
	}
}
