using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipment_Icon : UI_Scene
{
    public int CharacterCode = -1;

    public Define.ItemType _itemType;
    public int Equip_Code = -1;
    GameObject Equipment_Inventory;
    enum GameObjects
    {
        Equipment_Inventory,
        Equipment_Effect,

    }
    private void Start()
    {
        Init();
    }
    private void OnDisable()
    {
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh -= SetUI;

    }
    public override void Init()
    {
        base.Init();
        Debug.Log("Equipment_Icon Init Start");
        gameObject.GetComponent<Canvas>().sortingOrder = 1000;
        Bind<GameObject>(typeof(GameObjects));
        Equipment_Inventory = Get<GameObject>((int)GameObjects.Equipment_Inventory);
        Equipment_Inventory EQINVEN = GetComponentInChildren<Equipment_Inventory>();
        EQINVEN.CharacterCode = CharacterCode;
        EQINVEN._itemType = _itemType;
        gameObject.BindEvent((PointerEventData data) => ActiveAndUnActive(Equipment_Inventory));
        Get<GameObject>((int)GameObjects.Equipment_Effect).SetActive(false);
        SetUI();
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh -= SetUI;
        Managers.CharacterInventory.MyCharacters[CharacterCode].InvenUI_ReFresh += SetUI;
        Equipment_Inventory.SetActive(false);

    }
    public void SetUI()
    {
        Debug.Log($"Equip_Icon llll {gameObject.name}");

        if (Managers.CharacterInventory.MyCharacters[CharacterCode].EQUIP.EQUIP.ContainsKey(_itemType))
        {
            gameObject.GetComponent<Image>().sprite
        = Managers.Resource.Load<Sprite>(Managers.CharacterInventory.MyCharacters[CharacterCode].EQUIP
        .EQUIP[_itemType].ItemIconPath);
            //장착한 장비가 있으면 해당 장비의 이미지를 보여줍니다.
            Get<GameObject>((int)GameObjects.Equipment_Effect).SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite
          = Managers.Resource.Load<Sprite>("Images/Equipment/Others/NonEquipicon");
            //장비가 없으면 장비 이미지를 보여주지 않습니다.
            foreach (Item item in Managers.ItemInventory.Items.Values)
            {
                if (item.ItemType.Equals(_itemType) && Managers.CharacterInventory.MyCharacters[CharacterCode].EQUIP.AvailableEquip(item))
                {
                    Get<GameObject>((int)GameObjects.Equipment_Effect).SetActive(true);
                    //장착 가능한 장비가 있으면 보여준다.
                    break;
                }
            }
        }

    }
    public void ActiveAndUnActive(GameObject go)
    {
        if (go.activeSelf)
        {
            go.SetActive(false);
        }
        else
        {
            go.SetActive(true);
        }

    }
}
