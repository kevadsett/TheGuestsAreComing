﻿using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	float timer = 0.0f;
	int currentSeconds = 0;
	bool emittedTimeUp = false;

	void Update () {
		timer += Time.deltaTime;
		if (currentSeconds < 30) {
			if (currentSeconds != (int)timer) {
				currentSeconds = (int)timer;
				EventManager.SecondTick (currentSeconds);
			}
		} else if (!emittedTimeUp) {
			EventManager.TimeUp ();
		}
	}
}
