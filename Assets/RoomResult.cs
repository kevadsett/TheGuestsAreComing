using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomResult : MonoBehaviour {

	public string RoomName;
	public List<Sprite> BadSprites;
	public List<Sprite> MediumSprites;
	public List<Sprite> GoodSprites;

	void Start () {
		RoomProgress roomProgress = ProgressTracker.GetRoomProgress (RoomName);
		showAppropriateSprite ((float)roomProgress.CompleteItemCount / (float)roomProgress.TotalItems);
	}

	void showAppropriateSprite (float completion) {
		if (completion < 0.35f) {
			chooseSprite (BadSprites);
		} else if (completion < 0.65f) {
			chooseSprite (MediumSprites);
		} else {
			chooseSprite (GoodSprites);
		}
	}

	void chooseSprite(List<Sprite> SpriteList) {
		int randomIndex = Random.Range (0, SpriteList.Count);
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
		sr.sprite = SpriteList [randomIndex];
	}
}
