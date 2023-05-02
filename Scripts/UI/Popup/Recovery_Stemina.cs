using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Recovery_Stemina : UI_Popup
{
    enum GameObjects
    {
        No_Posion,
        Has_Posion,

    }
    enum Texts
    {
        Has_Posion_Text,
        Has_Posion_Button_No_Text,
        Has_Posion_Button_GotoDia_Text,
        Has_Posion_Button_Yex_Text,
        No_Posion_Text,
        No_Posion_Button_No_Text,
        No_Posion_Button_Yex_Text,



    }
    enum Buttons
    {
        Has_Posion_Button_GotoDia,
        Has_Posion_Button_No,
        Has_Posion_Button_Yes,
        No_Posion_Button_No,
        No_Posion_Button_Yes


    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Has_Posion_Button_GotoDia).gameObject
            .BindEvent((PointerEventData data) => GotoDia_Purchase());

        GetButton((int)Buttons.Has_Posion_Button_Yes).gameObject.BindEvent
            ((PointerEventData data) => Purchase_Stemina(true));
        GetButton((int)Buttons.No_Posion_Button_Yes).gameObject.BindEvent
            ((PointerEventData data) => Purchase_Stemina(false));
        GetButton((int)Buttons.Has_Posion_Button_No).gameObject.BindEvent
            ((PointerEventData data) => Managers.UI.ClosePopupUI());
        GetButton((int)Buttons.No_Posion_Button_No).gameObject.BindEvent
            ((PointerEventData data) => Managers.UI.ClosePopupUI());

        Get<GameObject>((int)GameObjects.Has_Posion).SetActive(false);
        Get<GameObject>((int)GameObjects.No_Posion).SetActive(false);
        Player_First_SHow();

    }
    private void GotoDia_Purchase()
    {
        Get<GameObject>((int)GameObjects.Has_Posion).SetActive(false);
        Get<GameObject>((int)GameObjects.No_Posion).SetActive(true);
    }
    private void Purchase_Stemina(bool IsUseDia)
    {
        if (IsUseDia)
        {
            Item.Consume consumitem = (Item.Consume)Managers.ItemInventory.Items[5000];
            Managers.Player.Stamina += consumitem.Value;
            Managers.ItemInventory.FindItemAndRemove(Managers.ItemInventory.Items[5000]);

        }
        else
        {
            if (Managers.Player.DiaMond < 60)
            {
                Managers.UI.ShowPopupUI<Diamonde_Alert>().Alertcode = 1;
                return;
            }
            Managers.Player.Stamina += 60;
            Managers.Player.DiaMond -= 60;
        }

        Managers.UI.ClosePopupUI();
    }
    private void Player_First_SHow()
    {
        if (Managers.ItemInventory.FindItem(5000))
        {
            Get<GameObject>((int)GameObjects.Has_Posion).SetActive(true);

        }
        else
        {
            Get<GameObject>((int)GameObjects.No_Posion).SetActive(true);
        }
    }

}
