using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUp_MyImage : UI_Popup
{
    private int _currentCard = 1;
    public int CurrentCard { get { return _currentCard; } set { _currentCard = value; } }

    Image cardimage;
    enum Buttons
    {
        Next_Button,
        Prev_Button,
        Select_Yes,
        Select_No,
    }
    private void OnEnable()
    {
        CurrentCard = PlayerPrefs.GetInt("BackGround");
    }
    enum Images
    {
        MyImage,
    }
    private void Start()
    {
        Init();

    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        cardimage = Get<Image>((int)Images.MyImage);
        cardimage.sprite = Managers.Resource.Load<Sprite>($"Images/BackGround/{CurrentCard}");

        GetButton((int)Buttons.Select_No).gameObject.BindEvent
            ((PointerEventData data) => ClosePopupUI());

        GetButton((int)Buttons.Next_Button).gameObject
            .BindEvent((PointerEventData data) => ChangeImage(1));
        GetButton((int)Buttons.Prev_Button).gameObject
           .BindEvent((PointerEventData data) => ChangeImage(2));
        GetButton((int)Buttons.Select_Yes).gameObject
         .BindEvent((PointerEventData data) => SelectImage());



    }

   public void ChangeImage(int i)
    {
        if (i.Equals(1))
        {
            CurrentCard += 1;
            if (CurrentCard.Equals((int)Define.Current_Card_Max.CardMax))
            {
                CurrentCard = 1;
            }
        }
        else
        {
            CurrentCard -= 1;
            if (CurrentCard <= 0)
            {
                CurrentCard = (int)Define.Current_Card_Max.CardMax;
            }
        }
        cardimage.sprite = Managers.Resource.Load<Sprite>($"Images/BackGround/{CurrentCard}");
    }
    void SelectImage()
    {
        ClosePopupUI();
        GameObject.FindGameObjectWithTag("BackGround").transform
            .GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/BackGround/{CurrentCard}");
        PlayerPrefs.SetInt("BackGround", CurrentCard);

    }

}

