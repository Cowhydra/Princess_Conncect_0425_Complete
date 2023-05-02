using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Position_Button_UI_Concept2 : UI_Scene
{
    public int ThisPosition = 0;
    GameObject Character;
    public bool IsOkSummon = true;
    Color Prevcolor;
    private void Start()
    {
        Init();

    }


    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<Button>().enabled = false;
        gameObject.GetComponent<Canvas>().sortingOrder = 3000;
        Prevcolor = GetComponent<Image>().color;
        gameObject.BindEvent((PointerEventData data) => OnDrop(data), Define.UIEvent.OnDrop);
    }

    public Concept2AutoBattleController CreateCharacter(string tag_name, int cdoe = 0, string name = "")
    {
        if (cdoe.Equals(0)) return null;
        IsOkSummon = false;
        //캐릭터의 태그 및 기존에 사용하던 무기 삭제 및 자식 관계설정
        //0일 경우 버튼만 생성됨 , 버튼을 생성 후 
        //유저가 캐릭터 아이콘을 드롭 하면 CrateCharacter를 통해 캐릭터를 만들어 줌
        if (string.IsNullOrEmpty(name))
        {
            Character = Managers.Resource.Instantiate($"UI/Character/{cdoe}");
            if (Character.transform.childCount > 0)
            {
                Managers.Resource.Destroy(Character.transform.GetChild(0).gameObject);

            }


            Character.GetComponent<CapsuleCollider2D>().enabled = false;
            Character.AddComponent<Concept2AutoBattleController>().CharCode = cdoe;


            //체력바 생성
            Concept2_HPBar HPBar = Managers.UI.ShowSceneUI<Concept2_HPBar>();
            HPBar.BattleStat = Character.GetComponent<Concept2AutoBattleController>();
            HPBar.transform.SetParent(Character.transform);

        }
        else
        {
            Character = Managers.Resource.Instantiate($"UI/Character/{name}");

            if (name.Equals("Boss"))
            {
                Character.AddComponent<Concept2AutoBattleController>().CharCode = 9999;
            }
            else
            {
                Character.AddComponent<Concept2AutoBattleController>().CharCode = 9998;
            }

        }

        Character.tag = tag_name;
        Character.transform.SetParent(gameObject.transform);
        Character.transform.localPosition = 50 * Vector2.up;
        if (tag_name.Equals("Enemy"))
        {
            float x = Character.transform.localScale.x;
            float y = Character.transform.localScale.y;
            Character.transform.localScale = new Vector2(-x, y);
        }
        Character.GetComponent<Concept2AutoBattleController>().Position = ThisPosition;



        return Character.GetComponent<Concept2AutoBattleController>();
    }

    public void Change_Color()
    {
        if (IsOkSummon)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }

    }
    public void Charnge_Color_Origin()
    {
        GetComponent<Image>().color = Prevcolor;
    }

    private void OnDrop(PointerEventData eventdata)
    {
        //PointerDrag => 현재 드래그 하고 있는 대상
        if (eventdata.pointerDrag != null && IsOkSummon)
        {

            IsOkSummon = false;
            GameObject go = GameObject.FindGameObjectWithTag("BattleManager");
            go.GetComponent<Concept2BattleManager>().BattleQueue.Push(CreateCharacter("Character", Managers.Stage.SelectCharacterConcept2));
            go.GetComponent<Stage_Fight_Concept2>().CurrCost -= Managers.Data.CharacterDataDict[Managers.Stage.SelectCharacterConcept2].limit;

        }
    }

}
