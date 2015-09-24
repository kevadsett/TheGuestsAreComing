using UnityEngine;
using System;

public class ItemProgress
{
	public string Name;
	public bool IsComplete;

	public ItemProgress (string name) {
		Name = name;
		IsComplete = false;
	}
}
