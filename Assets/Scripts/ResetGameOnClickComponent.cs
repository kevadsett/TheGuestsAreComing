using UnityEngine;
using System.Collections;

public class ResetGameOnClickComponent : MonoBehaviour {

	private ProgressTracker progressTracker;

	void Start() {
		GameObject progressTrackerGO = GameObject.Find ("ProgressTracker");
		if (progressTrackerGO != null) {
			progressTracker = progressTrackerGO.GetComponent<ProgressTracker> ();
		}
	}

	void OnMouseDown () {
		if (progressTracker) {
			progressTracker.Reset ();
		}
		RoomManager.Reset ();
	}
}
