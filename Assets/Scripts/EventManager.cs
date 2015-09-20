using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void RevertEvent(GameObject exception);
	public static event RevertEvent OnRevertStates;

	public delegate void SecondTickEvent(int currentSecond);
	public static event SecondTickEvent OnSecondTick;

	public delegate void ItemTidiedEvent();
	public static event ItemTidiedEvent OnItemTidied;

	public enum RoomEventType { itemTidied, roomComplete };
	public delegate void RoomEvent(string roomName, RoomEventType type, int itemCount, int totalItemCount);
	public static event RoomEvent OnRoomAction;

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

	public static void RoomAction(string roomName, RoomEventType type, int itemCount, int totalItemCount) {
		if (OnRoomAction != null) {
			OnRoomAction (roomName, type, itemCount, totalItemCount);
		}
	}
}
