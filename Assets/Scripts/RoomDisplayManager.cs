using UnityEngine;
using System.Collections;

public class RoomDisplayManager : MonoBehaviour {

	void ShowRoom(bool shouldShow, Transform roomToDisplay) {
		LayerDisplayManager.Hide ("RoomThumbnails");

		Instantiate (roomToDisplay);
	}
}
