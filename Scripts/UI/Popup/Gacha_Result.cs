using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gacha_Result : UI_Popup
{
    public int Gacha_Count;
    public Gacha_preparation Gacha_Prepare;
    public int Gacha_Price;
    private List<int> Gacha_Character_List;
    enum GameObjects
    {
        Gacha_Result_OnDisplay,
    }
    enum Texts
    {
        Gacha_Point_Count_Text,
    }
    enum Buttons
    {
        Cancel_Button,
        Retry_Button,
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Gacha_Prepare.Gacha_UI_Refresh -= SetUI;
        Gacha_Prepare.Gacha_UI_Refresh += SetUI;
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Gacha_Character_List = new List<int>();
        foreach (int i in Managers.Data.CharacterDataDict.Keys)
        {
            Gacha_Character_List.Add(i);
        }

        GetButton((int)Buttons.Cancel_Button).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        GetButton((int)Buttons.Retry_Button).gameObject
          .BindEvent((PointerEventData data) => ReGacha(Gacha_Count, Gacha_Price));
        SetUI();
        GachaExcute(Gacha_Count);


    }
    private void GachaExcute(int n)
    {
        foreach (Transform trans in Get<GameObject>((int)GameObjects.Gacha_Result_OnDisplay).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(trans.gameObject);
        }
        for (int i = 0; i < n; i++)
        {
            Character_Gacha_Result Gacha_Result = Managers.UI.ShowSceneUI<Character_Gacha_Result>();
            Gacha_Result.transform.SetParent(Get<GameObject>((int)GameObjects.Gacha_Result_OnDisplay).transform);
            Gacha_Result.Gacha_Result_Code = Pick_Character();
            Gacha_Result.ReleaseTime = i * 0.5f;
        }
        Gacha_Prepare.Gacha_UI_Refresh?.Invoke();
    }
    private void ReGacha(int n, int price)
    {
        if (Managers.Player.DiaMond < n * price)
        {
            Managers.UI.ShowPopupUI<Diamonde_Alert>();
            Debug.Log("다이아가 부족합니다. UI 추가 필요!");
            return;
        }
        Managers.Player.Charcter_Exchage_Count += n;
        Managers.Player.DiaMond -= n * price;
        GachaExcute(n);
    }

    private int Pick_Character()
    {
        int charactercode = Random.Range(0, Gacha_Character_List.Count);
        return Gacha_Character_List[charactercode];
    }
    void SetUI()
    {
        GetText((int)Texts.Gacha_Point_Count_Text).text = $"{Managers.Player.Charcter_Exchage_Count}";
    }

    private void OnDisable()
    {
        Gacha_Prepare.Gacha_UI_Refresh -= SetUI;
    }

}
