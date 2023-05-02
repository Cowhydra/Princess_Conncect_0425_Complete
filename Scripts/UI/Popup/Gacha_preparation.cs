using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
public class Gacha_preparation : UI_Popup
{

    public Action Gacha_UI_Refresh;
    enum GameObjects
    {
        VideoPlayer,
        Gacha_Info,


    }
    enum Texts
    {
        My_Diamond_Text,
        Gacha_Point_Count_Text,
        Gacha_Info_Text,
        Day_Limit_Pickup_Price_Text,
        Pickup_One_Buttion_Price_Text,
        Pickup_Ten_Buttion_Price_Text,

    }
    enum Images
    {
        Gacha_Main_Image,

    }

    enum Buttons
    {
        PickUp_Gacha,
        Platinum_Gacha,
        Nomal_Gacha,
        Day_Limit_Pickup_Button,
        Pickup_One_Buttion,
        Pickup_Ten_Buttion,
        Closed_Button,


    }
    void Start()
    {
        Init();

    }


    public override void Init()
    {
        base.Init();
        Gacha_UI_Refresh -= SetUI;
        Gacha_UI_Refresh += SetUI;
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        Get<GameObject>((int)GameObjects.VideoPlayer).GetComponent<VideoPlayer>().Play();
        GetButton((int)Buttons.Platinum_Gacha).gameObject
            .BindEvent((PointerEventData data) => Debug.Log("준비되지 않음"));
        GetButton((int)Buttons.Nomal_Gacha).gameObject
           .BindEvent((PointerEventData data) => Debug.Log("준비되지 않음"));
        GetButton((int)Buttons.Day_Limit_Pickup_Button).gameObject.
            BindEvent((PointerEventData data) => Gacha(1, 50));
        GetButton((int)Buttons.Pickup_One_Buttion).gameObject.
            BindEvent((PointerEventData data) => Gacha(1, 150));
        GetButton((int)Buttons.Closed_Button).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        GetButton((int)Buttons.Pickup_Ten_Buttion).gameObject.
       BindEvent((PointerEventData data) => Gacha(10, 150));
        SetUI();

    }


    private void Gacha(int n, int price)
    {
        if (Managers.Player.DiaMond < n * price)
        {
            Managers.UI.ShowPopupUI<Diamonde_Alert>();
            Debug.Log("다이아 부족!");
            return;

        }

        Gacha_Result _gachaResult = Managers.UI.ShowPopupUI<Gacha_Result>();
        _gachaResult.Gacha_Prepare = this;
        _gachaResult.Gacha_Count = n;
        _gachaResult.Gacha_Price = price;

        Managers.Player.DiaMond -= price * n;
        Managers.Player.Charcter_Exchage_Count += n;
    }
    private void SetUI()
    {
        GetText((int)Texts.Gacha_Point_Count_Text).text
    = $"{ Managers.Player.Charcter_Exchage_Count}";

        GetText((int)Texts.My_Diamond_Text).text =
            $"{Managers.Player.DiaMond}";
        if (Managers.Player.DiaMond >= 1500)
        {
            GetText((int)Texts.Day_Limit_Pickup_Price_Text).color
             = Color.blue;
            GetText((int)Texts.Pickup_One_Buttion_Price_Text).color
            = Color.blue;
            GetText((int)Texts.Pickup_Ten_Buttion_Price_Text).color
             = Color.blue;
        }
        else if (Managers.Player.DiaMond >= 150)
        {
            GetText((int)Texts.Day_Limit_Pickup_Price_Text).color
             = Color.blue;
            GetText((int)Texts.Pickup_One_Buttion_Price_Text).color
             = Color.blue;
            GetText((int)Texts.Pickup_Ten_Buttion_Price_Text).color
             = Color.red;
        }
        else if (Managers.Player.DiaMond >= 50)
        {
            GetText((int)Texts.Day_Limit_Pickup_Price_Text).color
             = Color.blue;
            GetText((int)Texts.Pickup_One_Buttion_Price_Text).color
          = Color.red;
            GetText((int)Texts.Pickup_Ten_Buttion_Price_Text).color
             = Color.red;

        }

    }
    private void OnDisable()
    {
        Gacha_UI_Refresh -= SetUI;
    }

}
