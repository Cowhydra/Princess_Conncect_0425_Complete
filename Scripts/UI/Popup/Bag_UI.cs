using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bag_UI : UI_Popup
{


    enum GameObjects
    {
        Bag,

    }
    enum Texts
    {
        Item_Name,
        Item_ToolTip,
        Item_Count,

    }
    enum Buttons
    {
        Close_Button,

    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        SetText(0);
        foreach (Transform trans in Get<GameObject>((int)GameObjects.Bag).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(trans.gameObject);
        }
        for (int i = 0; i < 50; i++)
        {
            Bag_Item_Image bag = Managers.UI.ShowSceneUI<Bag_Item_Image>();
            bag.Bag_UI = this;
            bag.transform.SetParent(Get<GameObject>((int)GameObjects.Bag).transform);
            if (i < Managers.ItemInventory.Items.Count)
            {
                bag.ItemCode = Managers.ItemInventory.Items.Keys.ToList()[i];

            }
        }

        GetButton((int)Buttons.Close_Button).gameObject.BindEvent
            ((PointerEventData data) => Managers.UI.ClosePopupUI());
    }
    public void SetText(int itemCode)
    {
        if (itemCode.Equals(0))
        {
            GetText((int)Texts.Item_Name).text = "";
            GetText((int)Texts.Item_ToolTip).text = "";
            GetText((int)Texts.Item_Count).text = "";
        }
        else
        {
            GetText((int)Texts.Item_Name).text = $"이름 : {Managers.ItemInventory.Items[itemCode].ItemName}";
            GetText((int)Texts.Item_ToolTip).text = $"툴팁 : {Managers.ItemInventory.Items[itemCode].ItemTooltip}";
            GetText((int)Texts.Item_Count).text = $"보유 개수 : {Managers.ItemInventory.Items[itemCode].Count}";
        }

    }

}
