using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	public Sprite BaseState;
	public Sprite ActiveState;
	public Sprite CompleteState;

	public bool IsActive {
		get;
		private set;
	}
	private bool isComplete = false;

	SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent <SpriteRenderer>();
	}

	void OnEnable() {
		EventManager.OnRevertStates += Reset;
	}

	void OnDisable() {
		EventManager.OnRevertStates -= Reset;
	}

	void Reset(GameObject exception) {
		if (exception != gameObject) {
			SetState ("base");
		}
	}

	public void SetState(string state) {
		if (spriteRenderer == null) {
			spriteRenderer = GetComponent <SpriteRenderer>();
		}
		switch (state) {
		case "base":
			if (!isComplete) {
				IsActive = false;
				spriteRenderer.sprite = BaseState;
			}
			break;
		case "active":
			if (!isComplete) {
				IsActive = true;
				spriteRenderer.sprite = ActiveState;
			}
			break;
		case "complete":
			IsActive = false;
			isComplete = true;
			spriteRenderer.sprite = CompleteState;
			break;
		}
	}
}
