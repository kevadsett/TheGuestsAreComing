using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public LayerMask layers;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (mouseWorldPosition.x, mouseWorldPosition.y), Vector2.zero, 0f, layers);

			if (hit.transform != null && RoomManager.CurrentRoomAge > 0.5f) {
				GameObject hitGO = hit.transform.gameObject;
				StateManager stateManager = hitGO.GetComponent<StateManager> ();
				ConnectedItemComponent connectedItemComponent = hitGO.GetComponent<ConnectedItemComponent> ();
				bool connectedIsActive = connectedItemComponent && connectedItemComponent.ConnectedIsActivated;
				EventManager.RevertStates ();
				switch (hitGO.layer) {
				case 8: // moveable item
					stateManager.SetState ("active");
					break;
				case 9: // moveable item targets
					if (connectedIsActive) {
						string connectedComponentName = connectedItemComponent.ConnectedTo.name;
						if (ProgressTracker.SetItemComplete (RoomManager.CurrentRoomName, connectedComponentName)) {
							stateManager.SetState ("complete");
							connectedItemComponent.ConnectedTo.GetComponent<StateManager> ().SetState ("complete");
							EventManager.ItemTidied ();
						}
					}
					break;
				case 10: // single-click items
					if (ProgressTracker.SetItemComplete (RoomManager.CurrentRoomName, hitGO.name)) {
						stateManager.SetState ("complete");
						EventManager.ItemTidied ();
					}
					break;
				}
			} else {
				EventManager.RevertStates ();
			}
		}
	}
}
