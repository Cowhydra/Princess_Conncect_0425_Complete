using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PriconeSHOP : UI_Scene
{
    public int DefaultMenu = 0;
    private void OnEnable()
    {
        SetShopUI((ShopType)DefaultMenu);
    }
    enum ShopType
    {
        DIA,
        GOLD,

        MAX_Count,

    }
    private void Awake()
    {
        Init();
    }
    enum GameObjects
    {
        DIASHOP,
        GOLDSHOP,

        Blocker,


        Shop_Menu_Pannel,



    }
    enum Buttons
    {
        Shop_Menu_Diamond_Button,
        Shop_Menu_Gold_Button,
        ShopClose_Button,


        Shop_Menu_NotReady1_Button,
        Shop_Menu_NotReady2_Button,





    }
    enum Images
    {


    }
    enum Texts
    {

        Shop_Menu_Diamond_Text,
        Shop_Menu_Gold_Text,
        Shop_Menu_NotReady1_Text,
        Shop_Menu_NotReady2_Text,

    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GameObject DIASHOP = Get<GameObject>((int)GameObjects.DIASHOP);
        GameObject GOLDSHOP = Get<GameObject>((int)GameObjects.GOLDSHOP);

        #region StroeAllocate
        foreach (Transform transforom in DIASHOP.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (int i in Managers.Data.GoodsDataDict.Keys)
        {
            if ((i % 2).Equals(0))
            {
                Goods go = Managers.UI.ShowWorldUI<Goods>();
                go.GoodsCode = i;
                go.transform.SetParent(DIASHOP.transform);
            }


        }
        foreach (Transform transforom in GOLDSHOP.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (int i in Managers.Data.GoodsDataDict.Keys)
        {
            if (!(i % 2).Equals(0))
            {
                Goods go = Managers.UI.ShowWorldUI<Goods>();
                go.GoodsCode = i;
                go.transform.SetParent(GOLDSHOP.transform);
            }


        }
        #endregion
        GetText((int)Texts.Shop_Menu_Diamond_Text).text = "다이아";
        GetText((int)Texts.Shop_Menu_Gold_Text).text = "골드";
        GetText((int)Texts.Shop_Menu_NotReady1_Text).text = "준비중";
        GetText((int)Texts.Shop_Menu_NotReady2_Text).text = "준비중";
        GetButton((int)Buttons.ShopClose_Button).gameObject.BindEvent
            ((PointerEventData data) => gameObject.SetActive(false));
        GetButton((int)Buttons.Shop_Menu_Diamond_Button).gameObject.BindEvent
            ((PointerEventData data) => SetShopUI((ShopType)(int)Buttons.Shop_Menu_Diamond_Button));
        GetButton((int)Buttons.Shop_Menu_Gold_Button).gameObject.BindEvent
           ((PointerEventData data) => SetShopUI((ShopType)(int)Buttons.Shop_Menu_Gold_Button));






    }

    private void SetShopUI(ShopType shoptype)
    {

        for (int i = 0; i < (int)ShopType.MAX_Count; i++)
        {
            Get<GameObject>(i).SetActive(false);
        }
        Get<GameObject>((int)shoptype).SetActive(true);
    }
    public void Set_active(int i)
    {
        gameObject.SetActive(true);
        SetShopUI((ShopType)i);
        Managers.Sound.Play("108_ub");
    }

}
