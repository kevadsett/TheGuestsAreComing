using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void RevertAction(GameObject exception);
	public static event RevertAction OnRevertStates;

	public static void RevertStates(GameObject exception = null) {
		if (OnRevertStates != null) {
			OnRevertStates (exception);
		}
	}
}
