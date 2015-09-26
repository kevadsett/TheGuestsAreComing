using UnityEngine;
using System.Collections;

public class DisplayRoomOnClickComponent : MonoBehaviour {

	public Transform RoomToDisplay;

	void OnMouseDown () {
		LayerDisplayManager.Hide ("RoomThumbnails");
		Instantiate (RoomToDisplay);
		EventManager.DisplayBackButton (true);
	}
}
