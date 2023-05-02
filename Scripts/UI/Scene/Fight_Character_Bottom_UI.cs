using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fight_Character_Bottom_UI : UI_Scene
{
    public int CharCode;
    public My_CharacterController _myCharacter;
    private void Start()
    {
        Init();
        _myCharacter.CharacterHMPUI -= SetUI;
        _myCharacter.CharacterHMPUI += SetUI;
    }

    enum Texts
    {
        HP_Slider_Text,
        MP_Slider_Text,

    }
    enum Sliders
    {
        HP_Slider,
        MP_Slider,

    }
    enum GameObjects
    {
        Skill_Ready,
    }
    public override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Get<GameObject>((int)GameObjects.Skill_Ready).SetActive(false);

        gameObject.GetComponent<Image>().sprite =
            Managers.Resource.Load<Sprite>($"Images/Characters/Icon/icon{CharCode}");
        gameObject.GetOrAddComponent<Canvas>().sortingOrder = 3200;
        gameObject.BindEvent((PointerEventData data) => StartCoroutine(nameof(Skill_Co)));
        gameObject.AddComponent<GraphicRaycaster>();
        SetUI();
    }
    private void SetUI()
    {
        Get<Slider>((int)Sliders.HP_Slider).value
           = (float)_myCharacter.Char_Hp / _myCharacter.Char_MaxHp;
        Get<Slider>((int)Sliders.MP_Slider).value
          = (float)_myCharacter.Char_Mp / _myCharacter.Char_MaxMp;
        if (_myCharacter.Char_Mp >= 100)
        {
            Get<GameObject>((int)GameObjects.Skill_Ready).SetActive(true);
        }
        else
        {
            Get<GameObject>((int)GameObjects.Skill_Ready).SetActive(false);
        }
        GetText((int)Texts.HP_Slider_Text).text = $"{_myCharacter.Char_Hp}/{_myCharacter.Char_MaxHp}";
        GetText((int)Texts.MP_Slider_Text).text = $"{_myCharacter.Char_Mp}/{_myCharacter.Char_MaxMp}";
        Debug.Log("Battle_UI_Refresh!!!");
    }


    IEnumerator Skill_Co()
    {
        if (_myCharacter.Char_Mp < 100) yield break;
        _myCharacter.Char_Mp -= _myCharacter.Char_Mp;
        GameObject go = Managers.Resource.Instantiate($"UI/Skill/Skill{CharCode}");
        _myCharacter.NotHit = true;
        _myCharacter.CreatureState = Define.CreatureState.SKILL;
        _myCharacter.animator.SetTrigger("SKILL");
        if (go.Equals(null)) yield break;
        go.transform.SetParent(_myCharacter.transform);
        go.transform.localPosition = Vector3.zero;
        Managers.Resource.Destroy(go, 5.0f);
        Debug.Log("SKill 진행 완료!");

        yield break;
    }
}
