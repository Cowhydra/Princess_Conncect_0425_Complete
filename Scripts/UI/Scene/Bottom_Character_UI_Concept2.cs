using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottom_Character_UI_Concept2 : UI_Scene
{

    public int CharCode;
    public Stage_Fight_Concept2 Stage_Conecept2;


    private Transform canvas;          //UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
    private Transform PreviousParent; // �ش� ������Ʈ��  ������ �ҼӵǾ� �ִ�  �θ� Transform
    private RectTransform rect;          //UI�� ��ġ ��� ���� rect Transform
    private int PrevSiblingIndex;
    //   private CanvasGroup canvasGroup;     //UI�� ���İ��� ��ȣ�ۿ��� ���� CanvasGroup

    GameObject ShowCharacter_Tower;
    private void Start()
    {
        Init();

    }

    enum Images
    {
        Character_Cost_Image,
    }
    enum Texts
    {
        Character_Cost_Text,
    }
    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        gameObject.GetComponent<Canvas>().sortingOrder = 5000;
        canvas = transform.parent.parent;
        rect = GetComponent<RectTransform>();

        gameObject.BindEvent((PointerEventData data) => BeginDrag(data), Define.UIEvent.OnBeginDrag);
        gameObject.BindEvent((PointerEventData data) => OnDrag(data), Define.UIEvent.OnDrag);
        gameObject.BindEvent((PointerEventData data) => EndDrag(), Define.UIEvent.OnEndDrag);
    }

    public void SetUI()
    {


        gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>
                   (Managers.Data.CharacterDataDict[CharCode].iconPath);

        if (Managers.CharacterInventory.MyCharacters[CharCode].Limit > Stage_Conecept2.CurrCost)
        {
            Color color = gameObject.GetComponent<Image>().color;
            color.a = 0.7f;
            gameObject.GetComponent<Image>().color = color;
        }
        else
        {
            Color color = gameObject.GetComponent<Image>().color;
            color.a = 1f;
            gameObject.GetComponent<Image>().color = color;

        }


        GetText((int)Texts.Character_Cost_Text).text = $"{Managers.CharacterInventory.MyCharacters[CharCode].Limit}";


    }
    private void LateUpdate()
    {
        SetUI();
    }
    private void BeginDrag(PointerEventData data)
    {
        //�巡�� ������ �ҼӵǾ� �ִ� �θ��� transform�� ����



        /*
        PreviousParent = transform.parent;
        PrevSiblingIndex = transform.GetSiblingIndex();
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        //<=== ���� �����ص� canvas�� �� �ϴ����� ������ �۾�
        //�׷��� �����ν� �� �巡�� ���� ������(?)���� Ȯ���� �� �ֽ��ϴ�.
      
        */
        //ĳ���� ���� �� A �� ���� ��, ������Ʈ �߰� => ���⼭ �浹 ����  ���� ����� ���� �˷���

        Managers.Stage.SelectCharacterConcept2 = -1;
        if (Managers.CharacterInventory.MyCharacters[CharCode].Limit > Stage_Conecept2.CurrCost)
        {
            return;
        }
        ShowCharacter_Tower = Managers.Resource.Instantiate($"UI/Character/{CharCode}");
        Managers.Stage.SelectCharacterConcept2 = CharCode;
        ShowCharacter_Tower.AddComponent<Concept2_Character_MouseEvent>();
        ShowCharacter_Tower.GetOrAddComponent<Concept2_Character_MouseEvent>().Setting(CharCode);
        ShowCharacter_Tower.tag = "Tower";
        ShowCharacter_Tower.transform.SetParent(canvas);




        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0.5f;
        gameObject.GetComponent<Image>().color = color;
        gameObject.GetComponent<GraphicRaycaster>().enabled = false;

    }

    private void OnDrag(PointerEventData data)
    {
        if (ShowCharacter_Tower == null) return;
        ShowCharacter_Tower.GetComponent<RectTransform>().position = data.position;
    }
    private void EndDrag()
    {
        //��ӿ� �������� ��� ������  ��ġ
        //if (transform.parent.Equals(canvas))
        //{
        //    transform.SetParent(PreviousParent);
        //    rect.position=PreviousParent.GetComponent<RectTransform>().position;
        //    transform.SetSiblingIndex(PrevSiblingIndex);
        //}

        if (ShowCharacter_Tower == null) return;
        Managers.Resource.Destroy(ShowCharacter_Tower.gameObject);
        Debug.Log("EndDrag");

        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1f;
        gameObject.GetComponent<Image>().color = color;
        GetComponent<GraphicRaycaster>().enabled = true;



    }



}

