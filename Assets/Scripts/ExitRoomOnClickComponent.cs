using UnityEngine;
using System.Collections;

public class ExitRoomOnClickComponent : MonoBehaviour {

	void OnMouseDown() {
		LayerDisplayManager.Show ("RoomThumbnails");
		Destroy (RoomManager.CurrentRoom);
		RoomManager.CurrentRoom = null;
		RoomManager.CurrentRoomName = null;
		EventManager.DisplayBackButton (false);
	}
}
