using UnityEngine;
using System.Collections;

public class ConnectedItemComponent : MonoBehaviour {
	public GameObject ConnectedTo = null;
	public bool ConnectedIsActivated {
		get { return ConnectedTo.GetComponent<StateManager> ().IsActive; }
	}
}
