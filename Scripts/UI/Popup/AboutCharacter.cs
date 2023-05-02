using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AboutCharacter : UI_Popup
{
    Character_Inventory character_Inventory;
    private void OnEnable()
    {
        Init();
        character_Inventory = GetComponentInChildren<Character_Inventory>();
    }
    enum GameObjects
    {
        Blocker,
        Character_Inventory,
        Character_Title,


    }
    enum Buttons
    {

        Character_CloseButton,
        SortByName,
        SortByActive,
        SortByMagicAttack,
        SortByAttack,


    }

    enum Texts
    {
        Charcter_TitleText,
        SortByName_Text,
        SortByActive_Text,
        SortByMagicAttack_Text,
        SortByAttack_Text


    }

    //캐릭터에 관련된 UI
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));


        Get<Button>((int)Buttons.Character_CloseButton).gameObject
        .BindEvent((PointerEventData data) => ClosePopupUI());


        Get<Button>((int)Buttons.SortByName).gameObject
        .BindEvent((PointerEventData data) => character_Inventory.Sorting(Define.Characters_SortingOption.CharacterName));
        Get<Button>((int)Buttons.SortByActive).gameObject.
         BindEvent((PointerEventData data) => character_Inventory.Sorting(Define.Characters_SortingOption.CharacterActive));
        Get<Button>((int)Buttons.SortByAttack).gameObject.
         BindEvent((PointerEventData data) => character_Inventory.Sorting(Define.Characters_SortingOption.CharacterAttack));
        Get<Button>((int)Buttons.SortByMagicAttack).gameObject.
         BindEvent((PointerEventData data) => character_Inventory.Sorting(Define.Characters_SortingOption.CharacterMagicAttack));


        GetText((int)Texts.SortByName_Text).text = "이름";
        GetText((int)Texts.SortByMagicAttack_Text).text = "마공";
        GetText((int)Texts.SortByAttack_Text).text = "물공";
        GetText((int)Texts.SortByActive_Text).text = "활성";

        Get<TextMeshProUGUI>((int)Texts.Charcter_TitleText).text = "캐릭터";



    }





}
