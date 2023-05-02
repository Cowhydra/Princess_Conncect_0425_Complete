using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipable_icon : UI_Scene
{
    public int Equip_Code = -1;
    GameObject Equipable_Item_Script;
    public int CharacterCode = -1;
    enum GameObjects
    {
        Equipable_Item_Script,

    }
    enum Images
    {

    }
    enum Texts
    {
        Equipable_icon_Count,
        Equipable_Yes_Text,
        Equipable_Item_Script_Name,
        Equipable_Item_Script_ToolTip,
        Equipable_Item_Script_Ablity1,
        Equipable_Item_Script_Ablity2,
        Equipable_Item_Script_Ablity3,
        Equipable_Item_Script_Ablity4,

    }
    enum Buttons
    {
        Equipable_icon,
        Equipable_Yes_Button,


    }
    void Start()
    {
        Init();
        gameObject.GetOrAddComponent<Canvas>().sortingOrder = 1010;
        Equipable_Item_Script.SetActive(false);
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>((typeof(GameObjects)));
        Equipable_Item_Script = Get<GameObject>((int)GameObjects.Equipable_Item_Script);
        gameObject.BindEvent((PointerEventData data) => ACtive_InActive(Equipable_Item_Script)
        );
        GetButton((int)Buttons.Equipable_Yes_Button).gameObject.BindEvent(
            (PointerEventData data) => EquipAndSetUI()
            );
        //
        //gameObject.BindEvent((PointerEventData data) => Equipable_Item_Script.SetActive(false), Define.UIEvent.PointerExit);

        SetUI();
        SetText();


    }
    private void EquipAndSetUI()
    {//Managers.Equip.EquipS(Managers.ItemInventory.Items[Equip_Code], CharacterCode)
        if (Managers.ItemInventory.FindItem(Equip_Code))
        {
            if (Managers.CharacterInventory.MyCharacters[CharacterCode].EQUIP.Equip(Managers.ItemInventory.Get(Equip_Code)))
            {
                Debug.Log($"{CharacterCode} ---- Equip Sucess!!");
                SetUI();
            }

        }

        else
        {
            Debug.Log("���� ����!");
        }
    }
    private void SetUI()
    {
        if (Equip_Code.Equals(-1))
        {

            gameObject.GetComponent<Image>().sprite
         = Managers.Resource.Load<Sprite>("Images/Equipment/Others/NonEquipicon");

        }
        else
        {
            gameObject.GetComponent<Image>().sprite
         = Managers.Resource.Load<Sprite>(Managers.Data.ItemDataDict[Equip_Code].iconpath);
        }

    }

    private void SetText()
    {
        GetText((int)Buttons.Equipable_icon).text = Managers.ItemInventory.Items[Equip_Code].Count + "";
        if (Managers.ItemInventory.Items[Equip_Code].Count > 0)
        {
            GetText((int)Buttons.Equipable_Yes_Button).text = $"����";
        }
        else
        {
            GetText((int)Buttons.Equipable_Yes_Button).text = "";
        }


        GetText((int)Texts.Equipable_Item_Script_Name).text = $"�̸� : {Managers.ItemInventory.Items[Equip_Code].ItemName}";
        GetText((int)Texts.Equipable_Item_Script_ToolTip).text = $"���� : {Managers.ItemInventory.Items[Equip_Code].ItemTooltip}";
        switch (Managers.ItemInventory.Items[Equip_Code].ItemType)
        {
            case Define.ItemType.Weapon:
                Item.Weapon weapon = Managers.ItemInventory.Items[Equip_Code] as Item.Weapon;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {weapon.Attack}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"���� : {weapon.MagicAttack}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {weapon.AttackRange}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Hat:
                Item.Hat hat = Managers.ItemInventory.Items[Equip_Code] as Item.Hat;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {hat.MagicDef}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"���� : {hat.MaxMp}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {hat.JobType}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Cloth:
                Item.Cloth cloth = Managers.ItemInventory.Items[Equip_Code] as Item.Cloth;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"ü�� : {cloth.MaxHp}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"��� : {cloth.Def}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {cloth.JobType}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Boot:
                Item.Boot boot = Managers.ItemInventory.Items[Equip_Code] as Item.Boot;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"��� : {boot.MaxHp}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"ü�� : {boot.Def}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {boot.JobType}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Earring:
                Item.Earring earring = Managers.ItemInventory.Items[Equip_Code] as Item.Earring;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {earring.Attack}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"ü�� : {earring.HpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {earring.MpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Necklace:
                Item.Necklace necklace = Managers.ItemInventory.Items[Equip_Code] as Item.Necklace;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {necklace.Attack}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"���� : {necklace.MaxMp}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"���� : {necklace.MpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
            case Define.ItemType.Accessory:
                Item.Accessory accessory = Managers.ItemInventory.Items[Equip_Code] as Item.Accessory;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {accessory.MaxMp}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"ü�� : {accessory.HpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = $"��� : {accessory.Def}";
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = $"���ݹ��� : {accessory.AttackRange}";
                break;
            case Define.ItemType.Ring:
                Item.Ring ring = Managers.ItemInventory.Items[Equip_Code] as Item.Ring;
                GetText((int)Texts.Equipable_Item_Script_Ablity1).text = $"���� : {ring.MpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity2).text = $"ü�� : {ring.HpRecovery}";
                GetText((int)Texts.Equipable_Item_Script_Ablity3).text = string.Empty;
                GetText((int)Texts.Equipable_Item_Script_Ablity4).text = string.Empty;
                break;
        }


    }

    private void ACtive_InActive(GameObject go)
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
