using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Information_Can : UI_Scene
{
    public int CharCode = -1;
    private void Start()
    {
        if (CharCode.Equals(-1)) return;
        Init();
    }
    private void OnDisable()
    {
        Managers.CharacterInventory.MyCharacters[CharCode].InvenUI_ReFresh -= SetUI;

    }
    enum GameObjects
    {
        Blocker,
        Character_Information,
        Character_Limit_Information,
        Equipment_Area,

    }
    enum Buttons
    {
        Character_Information_CloseButton,
        Character_Limit_IncreaseButton,
        Character_Limit_DecreaseButton,
        Auto_Equip,

    }
    enum Images
    {

        Character_Information_CardImage,
        Character_Limit_Star1,
        Character_Limit_Star2,
        Character_Limit_Star3,
        Character_Limit_Star4,
        Character_Limit_Star5,
        Character_Limit_Star_Active1,
        Character_Limit_Star_Active2,
        Character_Limit_Star_Active3,
        Character_Limit_Star_Active4,
        Character_Limit_Star_Active5,

    }
    enum Texts
    {
        Character_Information_NameText,
        CharacterStat_Text_HP,
        CharacterStat_Text_ATK,
        CharacterStat_Text_MANA,
        CharacterStat_Text_DEF,
        CharacterStat_Text_SKill,
        CharacterStat_Text_JOB,
        CharacterStat_Text_TMI,
        Character_Limit_Upgrade_Text,
        CharacterStat_Text_MagicATK,
        CharacterStat_Text_MagicDEF,
        CharacterStat_Text_Level,
        CharacterStat_Text_RequrieExp,
        CharacterStat_Text_Exp,
        CharacterStat_Text_HpRe,
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.Equipment_Area).GetComponent<Equipment_Area>().CharacterCode = CharCode;
        Debug.Log("Character_Information_Can Init()!");
        Managers.CharacterInventory.MyCharacters[CharCode].InvenUI_ReFresh -= SetUI;
        Managers.CharacterInventory.MyCharacters[CharCode].InvenUI_ReFresh += SetUI;

        GetButton((int)Buttons.Auto_Equip).gameObject.BindEvent
            ((PointerEventData data) => Auto_Equip());


        SetUI();


    }
    private void SetUI()
    {
        Debug.Log($"{CharCode} 의 장비창입니다.");
        gameObject.GetComponent<Canvas>().sortingOrder = 999;
        GetImage((int)Images.Character_Information_CardImage).sprite = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[CharCode].cardPath);
        GetButton((int)Buttons.Character_Information_CloseButton).gameObject.BindEvent((PointerEventData data) => Managers.Resource.Destroy(gameObject));
        GetText((int)Texts.Character_Information_NameText).text = "이름 : " + Managers.Data.CharacterDataDict[CharCode].name;
        GetText((int)Texts.CharacterStat_Text_Level).text = "레벨 : " + Managers.CharacterInventory.MyCharacters[CharCode].Level.ToString();
        GetText((int)Texts.CharacterStat_Text_HP).text = "체력 : " + Managers.Data.CharacterDataDict[CharCode].maxhp.ToString()
             + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_MaxHp})";
        GetText((int)Texts.CharacterStat_Text_ATK).text = "물공 : " + Managers.Data.CharacterDataDict[CharCode].attack.ToString()
            + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_Attack})";

        GetText((int)Texts.CharacterStat_Text_MANA).text = "마나 : " + Managers.Data.CharacterDataDict[CharCode].maxmana.ToString()
            + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_MaxMp})";

        GetText((int)Texts.CharacterStat_Text_DEF).text = "물방 : " + Managers.Data.CharacterDataDict[CharCode].def.ToString()
             + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_Def})";

        GetText((int)Texts.CharacterStat_Text_SKill).text = "스킬 : " + Managers.Data.CharacterDataDict[CharCode].Skill;
        GetText((int)Texts.CharacterStat_Text_JOB).text = "직업 : " + Managers.Data.CharacterDataDict[CharCode].jobType;//Check
        GetText((int)Texts.CharacterStat_Text_TMI).text = "TMI : " + Managers.Data.CharacterDataDict[CharCode].tmi;
        GetText((int)Texts.CharacterStat_Text_MagicATK).text = "마공 : " + Managers.Data.CharacterDataDict[CharCode].magicattack
             + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_MagicAttack})";
        GetText((int)Texts.CharacterStat_Text_MagicDEF).text = "마방 : " + Managers.Data.CharacterDataDict[CharCode].magicdef
             + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_MagicDef})";
        GetText((int)Texts.CharacterStat_Text_RequrieExp).text = "경험치 : " + Managers.CharacterInventory.MyCharacters[CharCode].Exp;
        GetText((int)Texts.CharacterStat_Text_Exp).text = "남은경험치 : " + Managers.CharacterInventory.MyCharacters[CharCode].RequireExp;
        GetText((int)Texts.CharacterStat_Text_HpRe).text = "체젠 : 0"
            + $"+ ({Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.EQUIP_HpRecovery})";

    }

    private void Auto_Equip()
    {
        //무식한 방법으로 장비창의 총 개수인 8개를 탐색
        //장착가능한 장비가 존재하면 장착
        for (int i = 0; i < (int)Define.ItemType.Consume; i++)
        {
            foreach (Item item in Managers.ItemInventory.Items.Values)
            {

                if (Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.AvailableEquip(item))
                {
                    Managers.CharacterInventory.MyCharacters[CharCode].EQUIP.Equip(item);
                    break;
                }
            }



        }
    }
}
