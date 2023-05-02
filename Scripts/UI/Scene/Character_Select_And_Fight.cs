using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Select_And_Fight : UI_Scene
{
    GameObject Character_Pannel;

    enum GameObjects
    {
        Character_Show_Pannel,
        CharacterSelect_Pannel,

    }
    enum Buttons
    {

        Character_Select_Button1,
        Character_Select_Button2,
        Character_Select_Button3,
        Character_Select_Button4,
        Character_Confirm_Button,
        Character_Reset_Button,


    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<Canvas>().sortingOrder = 2019;
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Character_Pannel = Get<GameObject>((int)GameObjects.Character_Show_Pannel);

        foreach (Transform transforom in Character_Pannel.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (MyCharacter myCharacter in Managers.CharacterInventory.MyCharacters.Values)
        {
            if (myCharacter.IsActive)
            {
                Character_Fight_Icon go = Managers.UI.ShowSceneUI<Character_Fight_Icon>();
                go.CharacterCode = myCharacter.CharacterCode;
                go.transform.SetParent(Character_Pannel.transform);
            }
        }
        SetButtonImage();
        GetButton((int)Buttons.Character_Reset_Button).gameObject.BindEvent((PointerEventData data) =>
        Managers.Stage.FightCharacter_Reset());
        GetButton((int)Buttons.Character_Confirm_Button).gameObject.BindEvent((PointerEventData data) =>
      Managers.Resource.Destroy(gameObject));
        Managers.Stage.FightCharacter_UIRefresh -= SetButtonImage;
        Managers.Stage.FightCharacter_UIRefresh += SetButtonImage;

    }
    private void OnDisable()
    {
        Managers.Stage.FightCharacter_UIRefresh -= SetButtonImage;
    }

    public void SetButtonImage()
    {
        for (int i = 0; i < Managers.Stage.FightCharacter.Count; i++)
        {
            if (!Managers.Stage.FightCharacter[i].Equals(-1))
            {
                GetButton(i).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[Managers.Stage.FightCharacter[i]].iconPath);

            }
            else
            {
                GetButton(i).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Images/Equipment/Others/NonEquipicon");
            }

        }
    }




}