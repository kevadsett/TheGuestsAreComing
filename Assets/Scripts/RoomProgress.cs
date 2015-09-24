using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomProgress
{
	public int CompleteItemCount;
	public int TotalItems { get { return Items.Count; } }
	public string Name;
	public List<ItemProgress> Items;

	public RoomProgress (string name, List<string> itemNames) {
		Name = name;
		CompleteItemCount = 0;
		Items = new List<ItemProgress>();
		foreach (string itemName in itemNames) {
			Items.Add(new ItemProgress(itemName));
		}
	}
}
