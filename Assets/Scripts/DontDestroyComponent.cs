using UnityEngine;
using System.Collections;

public class DontDestroyComponent : MonoBehaviour {

	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
}
