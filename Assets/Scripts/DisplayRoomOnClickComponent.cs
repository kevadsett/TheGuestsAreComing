using UnityEngine;
using System.Collections;

public class DisplayRoomOnClickComponent : MonoBehaviour {

	public Transform RoomToDisplay;

	void OnMouseDown () {
//		EventManager.ShowRoom (true, RoomToDisplay);
		LayerDisplayManager.Hide ("RoomThumbnails");

		Instantiate (RoomToDisplay);
	}
}
