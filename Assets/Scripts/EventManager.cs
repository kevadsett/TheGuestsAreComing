using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void RevertAction(GameObject exception);
	public static event RevertAction OnRevertStates;

	public delegate void SecondTickAction(int currentSecond);
	public static event SecondTickAction OnSecondTick;

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
}
