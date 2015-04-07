using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	private Animator GUIAnimator;

	private static InventoryManager thisInventory;
	private GameObject[] items;
	private InventorySlot[] inventorySlots;
	private int items_Index;
	private int selectedIndex;
	private int prevSelectedIndex;
	private GameObject itemNameLabel;
	private GameObject itemInfoLabel;

	private InventorySlot selectedSlot;
	private InventorySlot itemSlot;

	private GameObject ExaminePanel;
	private Transform ExaminePanelCamera;
	private bool openState;
	

	public static InventoryManager instance {
		get {
			if (thisInventory == null) {
				thisInventory = GameObject.FindObjectOfType<InventoryManager>();

			}
			return thisInventory;
		}
	}

	void Awake() {
		items = new GameObject[15];
		inventorySlots = new InventorySlot[15];
		selectedIndex = -1;
		prevSelectedIndex = -1;
		GUIAnimator = GameObject.Find ("Player GUI").GetComponent<Animator> ();
		ExaminePanel = transform.parent.FindChild ("Examine").gameObject;
		ExaminePanelCamera = transform.parent.FindChild ("Examine").transform.FindChild ("Examine Camera");
		itemNameLabel = transform.FindChild ("Item Info").FindChild ("Name").gameObject;
		itemInfoLabel = transform.FindChild ("Item Info").FindChild ("Info").gameObject;
	}

	void Start() {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 5; j++) {
				inventorySlots [(i*5)+j] = new InventorySlot(GameObject.Find ("Player GUI").transform.FindChild("Inventory Panel").FindChild ("Inventory").FindChild ("Items").FindChild ("Row"+i).FindChild ("ItemClickable "+j).gameObject);

				//Disable all buttons of the slots. Buttons get enabled when an item goes into the slot
				inventorySlots [(i*5)+j].getInvSlot().GetComponent<UIButton>().enabled = false;
			}
		}
		ExaminePanel.SetActive (false);
		ExaminePanelCamera.gameObject.SetActive (false);
	}

	public void itemPickedUp(GameObject obj) {
		if (items_Index != 16) {
			inventorySlots[items_Index].setObj(obj);
			inventorySlots[items_Index].setIndex(items_Index);
			inventorySlots[items_Index].setSlot();
			inventorySlots[items_Index].getInvSlot().GetComponent<UIButton>().enabled = true;
			items_Index++;
		}
	}

	public void itemSelected(GameObject obj) {
		int row = obj.transform.parent.name[obj.transform.parent.name.Length-1] - '0';
		int slot = obj.name[obj.name.Length-1] - '0';
		selectedIndex = (row * 5) + slot;
		itemSlot = inventorySlots [selectedIndex];

		if (prevSelectedIndex < 0) {
			itemSlot.triggerOn ();
			selectedSlot = itemSlot;
			setItemInfo(true);
		} else {
			if (prevSelectedIndex == selectedIndex) {
				itemSlot.triggerState ();
				if (selectedSlot == null) {
					selectedSlot = itemSlot;
					setItemInfo(true);
				} else {
					selectedSlot = null;
					setItemInfo(false);
				}
			}
			if (prevSelectedIndex != selectedIndex) {
				inventorySlots [prevSelectedIndex].triggerOff ();
				itemSlot.triggerOn ();
				selectedSlot = itemSlot;
				setItemInfo(true);
			}
		}
		prevSelectedIndex = selectedIndex;
	}

	public void examineItem() {
		if (selectedSlot != null) {
			ExaminePanel.SetActive(true);
			ExaminePanelCamera.gameObject.SetActive(true);
			GUIAnimator.SetTrigger ("Examine");
			selectedSlot.showObj();
			ExamineObjects.instance.SetObj(selectedSlot.getObj(), ExaminePanelCamera);
		}
	}

	public void closeExamine() {
		GUIAnimator.SetTrigger ("Examine");
		ExaminePanel.SetActive (false);
		ExaminePanelCamera.gameObject.SetActive (false);
	}

	public bool canOpen() {
		return openState;
	}
	
	public void setOpen(bool b) {
		openState = b;
	}

	public void setItemInfo(bool b) {
		if (b) {
			itemNameLabel.GetComponent<UILabel> ().text = InvDatabase.FindByName (selectedSlot.getObj().name).name;
			itemInfoLabel.GetComponent<UILabel> ().text = InvDatabase.FindByName (selectedSlot.getObj().name).description;
		} else {
			itemInfoLabel.GetComponent<UILabel> ().text = "";
			itemNameLabel.GetComponent<UILabel> ().text = "";
		}
	}
}
