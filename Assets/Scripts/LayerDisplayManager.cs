using UnityEngine;
using System.Collections;

public class LayerDisplayManager : MonoBehaviour {

	static Camera cam;

	void Start () {
		cam = Camera.main;
	}

	// Turn on the bit using an OR operation
	public static void Show (string layerName) {
		cam.cullingMask |= 1 << LayerMask.NameToLayer (layerName);
	}

	// Turn off the bit using an AND operation with the complement of the shifted int
	public static void Hide (string layerName) {
		cam.cullingMask &= ~(1 << LayerMask.NameToLayer (layerName));
	}

	// Toggle the bit using a XOR operation:
	public static void Toggle(string layerName) {
		cam.cullingMask ^= 1 << LayerMask.NameToLayer(layerName);
	}
}
