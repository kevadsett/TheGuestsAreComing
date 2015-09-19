using UnityEngine;
using System.Collections;

public class ChangeSceneOnClickComponent : MonoBehaviour {

	public string SceneName;

	void OnMouseDown () {
		Application.LoadLevel (SceneName);
	}
}
