﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public LayerMask layers;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (mouseWorldPosition.x, mouseWorldPosition.y), Vector2.zero, 0f, layers);

			if (hit.transform != null) {
				GameObject hitGO = hit.transform.gameObject;
				StateManager stateManager = hitGO.GetComponent<StateManager> ();
				switch (hitGO.layer) {
				case 8: // moveable item
					stateManager.SetState ("active");
					break;
				case 9: // moveable item targets
					ConnectedItemComponent connectedItemComponent = hitGO.GetComponent<ConnectedItemComponent> ();
					if (connectedItemComponent.ConnectedIsActivated) {
						stateManager.SetState ("complete");
						connectedItemComponent.ConnectedTo.GetComponent<StateManager> ().SetState ("complete");
						EventManager.ItemTidied ();
					}
					break;
				case 10: // single-click items
					stateManager.SetState ("complete");
					EventManager.ItemTidied ();
					break;
				}
				EventManager.RevertStates (hitGO);
			} else {
				EventManager.RevertStates ();
			}
		}
	}
}
