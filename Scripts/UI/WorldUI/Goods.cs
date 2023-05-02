using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Goods : UI_Scene
{
    public int GoodsCode = -1;
    public bool isFristCharge = false;
    private bool IsPurchase_Sucess = false;
    private void Start()
    {
        Init();
    }
    enum GameObjects
    {
        Goods,
        IsRealPurchase_Menu,

    }
    enum Buttons
    {
        Goods_PurChase_Button,
        IsRealPurchase_No_Button,
        IsRealPurchase_Yes_Button,



    }
    enum Images
    {
        Goods_First_Charge_Image,
        Goods_Scripts_Image,
        Goods_Image,
        Goods_Price_Image,




    }
    enum Texts
    {
        Goods_First_Charge_Text,
        Goods_Quantity_Text,
        Goods_PurChase_Text,
        Goods_Price_Text,
        IsRealPurchase_No_Text,
        IsRealPurchase_Yes_Text,

    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        gameObject.GetComponent<Canvas>().sortingOrder = 99;
        SetUI(GoodsCode);
        GetButton((int)Buttons.Goods_PurChase_Button).gameObject.BindEvent
          ((PointerEventData data) => Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(true));
        GetButton((int)Buttons.IsRealPurchase_No_Button).gameObject.BindEvent
         ((PointerEventData data) => Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(false));
        GetButton((int)Buttons.IsRealPurchase_Yes_Button).gameObject.BindEvent
        ((PointerEventData data) => Purchase_Goods());

        Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(false);

    }
    private void SetUI(int goodsCode)
    {
        if (goodsCode.Equals(-1)) return;

        Get<Image>((int)Images.Goods_Image).GetComponent<Image>().sprite
            = Managers.Resource.Load<Sprite>
          (Managers.Data.GoodsDataDict[goodsCode].MyiconPath);
        GetText((int)Texts.Goods_Quantity_Text).text = Managers.Data.GoodsDataDict[goodsCode].quantity + "";

        if (!(GoodsCode % 2).Equals(0)) //%2 ���̾� ����â
        {
            Get<Image>((int)Images.Goods_Price_Image).GetComponent<Image>().sprite
           = Managers.Resource.Load<Sprite>
         (Managers.Data.GoodsDataDict[goodsCode].ObjecticonPath);

            GetText((int)Texts.Goods_Price_Text).text = Managers.Data.GoodsDataDict[goodsCode].price + " ";
            isFristCharge = true;
        }
        else // Ȧ�� �ΰ�� ���̾Ʒ� ��� ����
        {
            GetText((int)Texts.Goods_Price_Text).text = Managers.Data.GoodsDataDict[goodsCode].price + "��";

        }
        if (isFristCharge)
        {
            Color color = GetImage((int)Images.Goods_First_Charge_Image).color;
            color.a = 0;
            GetImage((int)Images.Goods_First_Charge_Image).color = color;
            GetText((int)Texts.Goods_First_Charge_Text).text = "";

        }
    }

    private bool Purchase_Goods()
    {
        //����ó��
        //�÷��̾� ��ȭ�� ���� �ٸ��� ó�� 

        if ((GoodsCode % 2).Equals(0))
        {
            Debug.Log("���� �ý��� �߰� �ʿ�!");
            if (!isFristCharge)
            {
                Managers.Player.DiaMond += 2 * Managers.Data.GoodsDataDict[GoodsCode].quantity;
                Managers.Player.TotalUseMoney += Managers.Data.GoodsDataDict[GoodsCode].price;
                isFristCharge = true;

            }
            else
            {
                Managers.Player.DiaMond += Managers.Data.GoodsDataDict[GoodsCode].quantity;
                Managers.Player.TotalUseMoney += Managers.Data.GoodsDataDict[GoodsCode].price;
            }
            Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(false);
            //TODO ���� ó�� ��ȭ ���ϱ�  ���̾� ���� �κ� 
            SetUI(GoodsCode);


            IsPurchase_Sucess = true;
        }
        else
        {
            if (Managers.Player.DiaMond < Managers.Data.GoodsDataDict[GoodsCode].price)
            {
                //���� �Ұ� UI 
                Diamonde_Alert Alert = Managers.UI.ShowPopupUI<Diamonde_Alert>();
                Alert.Alertcode = 1;

                IsPurchase_Sucess = false;
            }
            //��� ��ȯ �κ� 
            Managers.Player.DiaMond -= Managers.Data.GoodsDataDict[GoodsCode].price;
            Managers.Player.Gold += Managers.Data.GoodsDataDict[GoodsCode].quantity;

            Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(false);
            SetUI(GoodsCode);
            IsPurchase_Sucess = true;

        }
        Get<GameObject>((int)GameObjects.IsRealPurchase_Menu).SetActive(false);
        return IsPurchase_Sucess;

    }

}
