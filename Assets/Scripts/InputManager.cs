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
				if (hitGO.layer == 8) {
					stateManager.SetState ("active");
				}
				if (hitGO.layer == 9) {
					ConnectedItemComponent connectedItemComponent = hitGO.GetComponent<ConnectedItemComponent> ();
					if (connectedItemComponent.ConnectedIsActivated) {
						stateManager.SetState ("complete");
						connectedItemComponent.ConnectedTo.GetComponent<StateManager> ().SetState ("complete");
					}
				}
				EventManager.RevertStates (hitGO);
			} else {
				EventManager.RevertStates ();
			}
		}
	}
}
