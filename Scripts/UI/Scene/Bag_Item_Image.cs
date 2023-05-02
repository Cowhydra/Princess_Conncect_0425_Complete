using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bag_Item_Image : UI_Scene
{
    public Bag_UI Bag_UI;
    public int ItemCode = 0;
    enum GameObjects
    {
        Bag_Item_Image,
        Bag_Item_Count
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().overrideSorting = false;
        Bind<GameObject>(typeof(GameObjects));
        SetUI();
        gameObject.BindEvent((PointerEventData data) =>
        Bag_UI.SetText(ItemCode)
        , Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) =>
        Bag_UI.SetText(0), Define.UIEvent.PointerExit);
    }

    void SetUI()
    {
        if (ItemCode.Equals(0)) return;

        Get<GameObject>((int)GameObjects.Bag_Item_Image).GetComponent<Image>()
            .sprite = Managers.Resource.Load<Sprite>(Managers.Data.ItemDataDict[ItemCode].iconpath);

        Get<GameObject>((int)GameObjects.Bag_Item_Count).GetComponent<TextMeshProUGUI>()
            .text = $"{Managers.ItemInventory.Items[ItemCode].Count}";
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1;
        gameObject.GetComponent<Image>().color = color;
    }



}
