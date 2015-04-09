using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor {

	private Item item;
	private int index;
	private string itemName;
	private string description;
	public GameObject itemObj;
	private Sprite sprite;

	public override void OnInspectorGUI() {
		ItemDatabase db = (ItemDatabase)target;

		if (ItemDatabase.instance.itemList ().Count > 0) {
			//item = db.itemList () [index];
			item = ItemDatabase.instance.itemList()[index];
		} else {
			index = 0;
			item = null;
		}

		GUI.backgroundColor = Color.green;
		if (GUILayout.Button ("Add Item")) {
			GUI.FocusControl(null);
			Item newItem = new Item();
			newItem.name = "New Item";
			newItem.description = "Enter Description";
			//db.add(newItem);
			ItemDatabase.instance.add(newItem);
			item = newItem;
			index = ItemDatabase.instance.itemList().Count-1;
			//index = db.itemList().Count-1;
		}
		GUI.backgroundColor = Color.white;

		if (item != null) {

			//Drop down menu of items in database
			index = EditorGUILayout.Popup(index, ItemDatabase.instance.itemNames());

			//Indexing
			GUILayout.BeginHorizontal(); 
			{
				if (index < 1) {
					GUI.backgroundColor = Color.gray;
				}
				if (GUILayout.Button("Prev") && GUI.backgroundColor != Color.gray) {
					GUI.FocusControl(null);
					index--;
				}
				GUI.backgroundColor = Color.white;
				GUILayout.Label("Current/Total");
				index = EditorGUILayout.IntField(index + 1) - 1;
				GUILayout.Label("/" + ItemDatabase.instance.itemList().Count);
				if (index+1 == ItemDatabase.instance.itemList().Count) {
				//GUILayout.Label("/" + db.itemList().Count);
				//if (index+1 == db.itemList().Count) {
					GUI.backgroundColor = Color.gray;
				}
				if (GUILayout.Button("Next") && GUI.backgroundColor != Color.gray) {
					GUI.FocusControl(null);
					index++;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();


			//Target prefab for current item
			itemObj = (GameObject)EditorGUILayout.ObjectField("Object", item.prefab, typeof(GameObject), false);


			sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", item.sprite, typeof(Sprite), false);
				
			//Name of item and option to delete
			GUILayout.BeginHorizontal ();
			{
				itemName = EditorGUILayout.TextField ("Item Name", item.name);
				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("Delete", GUILayout.Width(55f))) {
					ItemDatabase.instance.itemList().RemoveAt(index);
					if (index > ItemDatabase.instance.itemList().Count-1) {
						index--;
					}
					//db.itemList().RemoveAt(index);
				}
				GUI.backgroundColor = Color.white;

			}
			GUILayout.EndHorizontal ();


			//Description of item
			description = GUILayout.TextArea (item.description, 200, GUILayout.Height (100f));


			//Sets item data if data gets edited
			if (!description.Equals(item.description) || 
			    !itemName.Equals(item.name) ||
			    itemObj != item.prefab ||
			    sprite != item.sprite) {

				item.description = description;
				item.name = itemName;
				item.prefab = itemObj;
				item.sprite = sprite;
			}

		}
	}
}
