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
        //ĳ������ �±� �� ������ ����ϴ� ���� ���� �� �ڽ� ���輳��
        //0�� ��� ��ư�� ������ , ��ư�� ���� �� 
        //������ ĳ���� �������� ��� �ϸ� CrateCharacter�� ���� ĳ���͸� ����� ��
        if (string.IsNullOrEmpty(name))
        {
            Character = Managers.Resource.Instantiate($"UI/Character/{cdoe}");
            if (Character.transform.childCount > 0)
            {
                Managers.Resource.Destroy(Character.transform.GetChild(0).gameObject);

            }


            Character.GetComponent<CapsuleCollider2D>().enabled = false;
            Character.AddComponent<Concept2AutoBattleController>().CharCode = cdoe;


            //ü�¹� ����
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
        //PointerDrag => ���� �巡�� �ϰ� �ִ� ���
        if (eventdata.pointerDrag != null && IsOkSummon)
        {

            IsOkSummon = false;
            GameObject go = GameObject.FindGameObjectWithTag("BattleManager");
            go.GetComponent<Concept2BattleManager>().BattleQueue.Push(CreateCharacter("Character", Managers.Stage.SelectCharacterConcept2));
            go.GetComponent<Stage_Fight_Concept2>().CurrCost -= Managers.Data.CharacterDataDict[Managers.Stage.SelectCharacterConcept2].limit;

        }
    }

}
