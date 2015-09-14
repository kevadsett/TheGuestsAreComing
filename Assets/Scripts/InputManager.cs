using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (mouseWorldPosition.x, mouseWorldPosition.y), Vector2.zero, 0f);

			if (hit.transform != null) {
				GameObject hitGO = hit.transform.gameObject;
				StateManager stateManager = hitGO.GetComponent<StateManager> ();
				switch (hitGO.layer) {
				case 8: // moveable objects
					stateManager.SetState ("active");
					break;
				case 9: // moveable object targets
					ConnectedItemComponent connectedItemComponent = hitGO.GetComponent<ConnectedItemComponent> ();
					if (connectedItemComponent.ConnectedIsActivated) {
						stateManager.SetState ("complete");
						connectedItemComponent.ConnectedTo.GetComponent<StateManager> ().SetState ("complete");
					}
					break;
				case 10: // single-click objects
					stateManager.SetState ("complete");
					break;
				}
				EventManager.RevertStates (hitGO);
			} else {
				EventManager.RevertStates ();
			}
		}
	}
}
