using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Select_Fight_Character : UI_Scene
{


    enum GameObjects
    {
        Character_Info,
        Enemy_Info,
        GameOption
    }
    enum Buttons
    {

        Character_Select_Button1,
        Character_Select_Button2,
        Character_Select_Button3,
        Character_Select_Button4,
        Character_GameStart_Button,
        Character_Reset_Button,
        Character_Cancel_Button,





    }
    enum Images
    {
        Character_Select_Info_Image1,
        Character_Select_Info_Image2,
        Character_Select_Info_Image3,
        Character_Select_Info_Image4,
        Enemy_Info1,
        Enemy_Info2,
        Enemy_Info3,
        Enemy_Info4,

    }
    enum Texts
    {
        Character_Select_Button1_Text,
        Character_Select_Button2_Text,
        Character_Select_Button3_Text,
        Character_Select_Button4_Text,
        Character_TotalStat_Info_Text1,
        Character_TotalStat_Info_Text2,
        Character_TotalStat_Info_Text3,
        Character_TotalStat_Info_Text4,
        Character_GameStart_Text,
        Character_Reset_Text,
        Character_Cancel_Text,


    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        gameObject.GetComponent<Canvas>().sortingOrder = 2000;

        GetButton((int)Buttons.Character_Cancel_Button).gameObject.BindEvent
            ((PointerEventData data) => Managers.Resource.Destroy(gameObject));
        GetButton((int)Buttons.Character_Reset_Button).gameObject.BindEvent
            ((PointerEventData data) => Managers.Stage.FightCharacter_Reset());

        GetButton((int)Buttons.Character_GameStart_Button).gameObject.BindEvent
            ((PointerEventData data) => GameStart());
        GetButton((int)Buttons.Character_Select_Button1).gameObject.BindEvent
          ((PointerEventData data) => Managers.UI.ShowSceneUI<Character_Select_And_Fight>());
        GetButton((int)Buttons.Character_Select_Button2).gameObject.BindEvent
        ((PointerEventData data) => Managers.UI.ShowSceneUI<Character_Select_And_Fight>());
        GetButton((int)Buttons.Character_Select_Button3).gameObject.BindEvent
         ((PointerEventData data) => Managers.UI.ShowSceneUI<Character_Select_And_Fight>());
        GetButton((int)Buttons.Character_Select_Button4).gameObject.BindEvent
         ((PointerEventData data) => Managers.UI.ShowSceneUI<Character_Select_And_Fight>());
        Managers.Stage.FightCharacter_UIRefresh -= SetUI;
        Managers.Stage.FightCharacter_UIRefresh += SetUI;
        SetUI();

    }
    private void OnDisable()
    {
        Managers.Stage.FightCharacter_UIRefresh -= SetUI;
    }
    private void SetUI()
    {
        for (int i = 0; i < Managers.Stage.FightCharacter.Count; i++)
        {
            if (Managers.Stage.FightCharacter[i].Equals(-1))
            {

                GetImage((i)).sprite = Managers.Resource.Load<Sprite>("Images/Equipment/Others/NonEquipicon");
                GetButton((i)).image.enabled = true;
                if (GetButton((i)).transform.childCount.Equals(2))
                {
                    Managers.Resource.Destroy(GetButton((i)).transform.GetChild(1).gameObject);
                }
                SetText2_void(i);
            }

            else
            {

                GetImage((i)).sprite = Managers.Resource.Load<Sprite>($"{Managers.Data.CharacterDataDict[Managers.Stage.FightCharacter[i]].iconPath}");

                GetText((i)).text = "";

                GetButton((i)).image.enabled = false;
                GameObject go = Managers.Resource.Instantiate($"UI/Character/{Managers.Data.CharacterDataDict[Managers.Stage.FightCharacter[i]].charatercode}");
                if (GetButton((i)).transform.childCount.Equals(2))
                {
                    Managers.Resource.Destroy(GetButton((i)).transform.GetChild(1).gameObject);
                }
                go.transform.SetParent(GetButton(i).transform);
                go.transform.localPosition = new Vector3(0, 0, 0);


                SetText2(i);
            }

        }

    }
    private void SetText2(int i)
    {
        switch (i)
        {
            case 0:
                GetText((int)Texts.Character_TotalStat_Info_Text1).text = $"{Managers.CharacterInventory.MyCharacters[Managers.Stage.FightCharacter[i]].Limit}";
                break;
            case 1:
                GetText((int)Texts.Character_TotalStat_Info_Text2).text = $"{Managers.CharacterInventory.MyCharacters[Managers.Stage.FightCharacter[i]].Limit}";
                break;
            case 2:
                GetText((int)Texts.Character_TotalStat_Info_Text3).text = $"{Managers.CharacterInventory.MyCharacters[Managers.Stage.FightCharacter[i]].Limit}";
                break;
            case 3:
                GetText((int)Texts.Character_TotalStat_Info_Text4).text = $"{Managers.CharacterInventory.MyCharacters[Managers.Stage.FightCharacter[i]].Limit}";
                break;

        }
    }
    private void SetText2_void(int i)
    {
        switch (i)
        {
            case 0:
                GetText((int)Texts.Character_TotalStat_Info_Text1).text = "";
                break;
            case 1:
                GetText((int)Texts.Character_TotalStat_Info_Text2).text = "";
                break;
            case 2:
                GetText((int)Texts.Character_TotalStat_Info_Text3).text = "";
                break;
            case 3:
                GetText((int)Texts.Character_TotalStat_Info_Text4).text = "";
                break;

        }
    }
    private void GameStart_Concept1()
    {
        bool isOkStart = false;
        foreach (int i in Managers.Stage.FightCharacter)
        {
            if (!i.Equals(-1))
            {
                isOkStart = true;
                break;
            }

        }
        if (isOkStart)
        {
            Managers.UI.ShowSceneUI<Stage_Fight_Concept1>();
            Managers.Resource.Destroy(gameObject);
        }
        else
        {
            Managers.UI.ShowWorldUI<Alert_Message>().Errcode = 0;
            Debug.Log("캐릭터가 부족합니다.!");
        }

    }
    private void GameStart_Concept2()
    {
        bool isOkStart = false;
        foreach (int i in Managers.Stage.FightCharacter)
        {
            if (!i.Equals(-1))
            {
                isOkStart = true;
                break;
            }

        }
        if (isOkStart)
        {
            Managers.UI.ShowSceneUI<Stage_Fight_Concept2>();
            Managers.Resource.Destroy(gameObject);
        }
        else
        {
            Managers.UI.ShowWorldUI<Alert_Message>().Errcode = 0;
            Debug.Log("캐릭터가 부족합니다.!");
        }

    }

    private void GameStart()
    {
        if ((Managers.Stage.StageCode % 2).Equals(1))
        {
            GameStart_Concept2();
        }
        else
        {
            GameStart_Concept1();
        }
    }
}
