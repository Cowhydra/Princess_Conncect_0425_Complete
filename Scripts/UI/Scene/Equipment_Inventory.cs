using UnityEngine;

public class Equipment_Inventory : MonoBehaviour
{


    public int CharacterCode = -1;
    public Define.ItemType _itemType;
    private void Start()
    {
        Init();
        Debug.Log($"Equipment_Inventory Start!! {CharacterCode} - {_itemType}");
        gameObject.GetOrAddComponent<Canvas>().sortingOrder = 1005;


    }
    private void OnDisable()
    {
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh -= SetUI;
    }
    public void Init()
    {
        Debug.Log("Equip_Inventory Init!!");

        SetUI();
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh -= SetUI;
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh += SetUI;
    }
    public void SetUI()
    {
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (Item item in Managers.ItemInventory.Items.Values)
        {
            if (item.ItemType.Equals(_itemType)
                && Managers.CharacterInventory.MyCharacters[CharacterCode].EQUIP.AvailableEquip(item))//
            {
                Equipable_icon icon = Managers.UI.ShowSceneUI<Equipable_icon>();
                icon.Equip_Code = item.ItemCode;
                icon.transform.SetParent(gameObject.transform);
                icon.CharacterCode = CharacterCode;
            }
        }

    }

}
