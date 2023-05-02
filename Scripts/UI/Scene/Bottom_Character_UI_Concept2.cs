using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottom_Character_UI_Concept2 : UI_Scene
{

    public int CharCode;
    public Stage_Fight_Concept2 Stage_Conecept2;


    private Transform canvas;          //UI가 소속되어 있는 최상단의 Canvas Transform
    private Transform PreviousParent; // 해당 오브젝트가  직전에 소속되어 있던  부모 Transform
    private RectTransform rect;          //UI의 위치 제어를 위한 rect Transform
    private int PrevSiblingIndex;
    //   private CanvasGroup canvasGroup;     //UI의 알파값과 상호작용을 위한 CanvasGroup

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
        //드래그 직전에 소속되어 있는 부모의 transform을 저장



        /*
        PreviousParent = transform.parent;
        PrevSiblingIndex = transform.GetSiblingIndex();
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        //<=== 내가 설정해둔 canvas의 최 하단으로 보내는 작업
        //그렇게 함으로써 이 드래그 중인 아이템(?)등을 확인할 수 있습니다.
      
        */
        //캐릭터 생성 및 A 값 변경 및, 컴포넌트 추가 => 여기서 충돌 감지  생성 비생성 지역 알려줌

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
        //드롭에 실패했을 경우 이전의  위치
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

