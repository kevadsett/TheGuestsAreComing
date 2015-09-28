using UnityEngine;
using System.Collections;

public class ExitRoomOnClickComponent : MonoBehaviour {

	void OnMouseDown() {
		LayerDisplayManager.Show ("RoomThumbnails");
		Destroy (RoomManager.CurrentRoom);
		RoomManager.CurrentRoom = null;
		RoomManager.CurrentRoomName = null;
		RoomManager.CurrentRoomAge = 0.0f;
		EventManager.DisplayBackButton (false);
	}
}
