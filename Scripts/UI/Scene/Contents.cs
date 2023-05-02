using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Contents : UI_Scene
{
    PriconeSHOP SHOP;
    private void Start()
    {
        SHOP = GameObject.FindGameObjectWithTag("SHOP").GetComponent<PriconeSHOP>();
        Init();
        SHOP.gameObject.SetActive(false);

        Managers.Player.UIExchange -= RefreshUI;
        Managers.Player.UIExchange += RefreshUI;
    }
    enum Buttons
    {
        Char_Button,
        Stage_Button,
        Travel_Button,
        Gacha_Button,
        Bag_Button,

        /// Goods SHop Button

        GoldButton,
        DiamondButton,
        StaminaButton,

        /// 


        MissionButton,
        ShopButton,
        NoticeButton,

        GiftButton,
        GameEventButton,
        GuideButton,
        SettingButton,
        SelectBackGround_Button,


    }
    enum Texts
    {
        Char_Text,
        Stage_Text,
        Travel_Text,
        Gacha_Text,
        Bag_Text,
        GoldText,
        DiamondText,
        PlayerLevelText,
        StaminaText,
        MissionText,
        NoticeText,
        ShopText,
        GiftText,
        GameEventText,
        GuideText,
        SettingText,
        SelectBackGround_Text,


    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetButton((int)Buttons.Char_Button).gameObject.BindEvent(
            (PointerEventData data) => ShowCharacter());
        GetButton((int)Buttons.Stage_Button).gameObject.BindEvent(
           (PointerEventData data) => Managers.UI.ShowPopupUI<AboutStage>());
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(
            (PointerEventData data) => SHOP.Set_active(0));
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(
          (PointerEventData data) => SHOP.Set_active(1));
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(
       (PointerEventData data) => SHOP.Set_active(0));
        RefreshUI();
        GetButton((int)Buttons.SelectBackGround_Button).gameObject
            .BindEvent((PointerEventData data) => Show_Pickup_MyImage());
        GetButton((int)Buttons.StaminaButton).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<Recovery_Stemina>());
        GetButton((int)Buttons.SettingButton).gameObject
            .BindEvent((PointerEventData data) => ShowSetting());
        GetButton((int)Buttons.Bag_Button).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<Bag_UI>());
        GetButton((int)Buttons.Gacha_Button).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<Gacha_preparation>());
    }

    private void ShowCharacter()
    {
        Managers.UI.ShowPopupUI<AboutCharacter>();
        Managers.Sound.Play("108_ub");
    }
    private void Show_Pickup_MyImage()
    {
        Managers.UI.ShowPopupUI<PickUp_MyImage>();
        Managers.Sound.Play("108_ub");
    }
    private void ShowSetting()
    {
        Managers.UI.ShowPopupUI<Setting>();
        Managers.Sound.Play("108_attack");
    }


    void RefreshUI()
    {
        GetText((int)Texts.GoldText).text = Managers.Player.Gold + "";
        GetText((int)Texts.DiamondText).text = Managers.Player.DiaMond + "";
        GetText((int)Texts.PlayerLevelText).text = Managers.Player.PlayerLevel + "";
        GetText((int)Texts.StaminaText).text = $"{Managers.Player.Stamina} / {Managers.Player.MaxStamina}";

    }
}
