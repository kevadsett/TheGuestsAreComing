using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void RevertAction(GameObject exception);
	public static event RevertAction OnRevertStates;

	public delegate void SecondTickAction(int currentSecond);
	public static event SecondTickAction OnSecondTick;

	public delegate void ItemTidiedAction();
	public static event ItemTidiedAction OnItemTidied;

	public delegate void RoomCompleteAction(string roomName);
	public static event RoomCompleteAction OnRoomComplete;

	public static void RevertStates(GameObject exception = null) {
		if (OnRevertStates != null) {
			OnRevertStates (exception);
		}
	}

	public static void SecondTick(int currentSecond = 0) {
		if (OnSecondTick != null) {
			OnSecondTick (currentSecond);
		}
	}

	public static void ItemTidied() {
		if (OnItemTidied != null) {
			OnItemTidied ();
		}
	}

	public static void RoomComplete(string roomName) {
		if (OnRoomComplete != null) {
			OnRoomComplete (roomName);
		}
	}
}
