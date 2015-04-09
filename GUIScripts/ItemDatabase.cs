using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	private static ItemDatabase db;
	private List<Item> items = new List<Item>();

	public static ItemDatabase instance {
		get {
			if (db == null) {
				db = GameObject.FindObjectOfType<ItemDatabase> ();
			}
			return db;
		}
	}

	public void add(Item i) {
		items.Add (i);
	}

	public List<Item> itemList() {
		return items;
	}

	public string[] itemNames() {
		string[] nameArray = new string[items.Count];
		int index = 0;
		foreach (Item i in items) {
			//nameArray [index] = i.name;
			nameArray[index] = itemNamesHelper (nameArray, i.name);
			index++;
		}
		return nameArray;
	}

	private string itemNamesHelper(string[] s, string name) {
		int count = 0;
		foreach (string str in s) {
			if (str != null && str.Substring(0,str.Length-2) == name) {
				count++;
			}
		}
		return name + " " + count;
	}

}
