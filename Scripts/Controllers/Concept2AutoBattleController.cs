using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Concept2AutoBattleController : MonoBehaviour
{
    GameObject BattleManager;
    Concept2BattleManager Concept2_BattleManager;

    public Action SliderBar_Refresh;

    public Animator animator;
    [SerializeField]
    private float speed;
    private float attack;
    [SerializeField]
    private float hp;
    private float mp;
    //자신만의 기 에픽세븐 속도 느낌

    [SerializeField]
    private float attackGage;
    public int Position;

    public int CharCode;


    public float MP
    {
        get { return mp; }
        set { mp = value; }
    }

    private float requireGage;
    public float AttackGage
    {
        get { return attackGage; }
        set
        {
            attackGage = value;
            if (attackGage >= requireGage)
            {
                attackGage -= requireGage;
                Concept2_BattleManager.BattleQueue.Enqueue(this);
            }
        }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            StartCoroutine(nameof(Hit_color_Change_Co));
            SliderBar_Refresh?.Invoke();
            if (hp <= 0)
            {
                if (gameObject.tag.Equals("Character"))
                {
                    gameObject.GetComponentInParent<Position_Button_UI_Concept2>().IsOkSummon = true;
                }
                if (CharCode.Equals(9999))
                {
                    //보스
                    Managers.Stage.IsGameClear = 1;
                }
                else if (CharCode.Equals(9998))
                {
                    Managers.Stage.IsGameClear = 2;
                }
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
    public float Attack
    {
        get { return attack; }
        set
        {
            attack = value;
        }
    }
    public float MaxHp
    {
        get
        {
            return _maxhp;
        }
        set
        {
            _maxhp = value;
        }
    }
    private float _maxhp;
    void Start()
    {
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager");
        Concept2_BattleManager = BattleManager.GetComponent<Concept2BattleManager>();
        animator = GetComponent<Animator>();
        requireGage = 100.0f;

        if (CharCode.Equals(9999) || CharCode.Equals(9998))
        {
            speed = 20;
            attack = 20;
            _maxhp = Managers.Stage.BossHp;
            hp = _maxhp;
            mp = 0;
        }
        else
        {
            speed = Managers.Data.CharacterDataDict[CharCode].attackspeed
                   + (Managers.Data.CharacterDataDict[CharCode].attack
                   + Managers.Data.CharacterDataDict[CharCode].magicattack) * 0.5f;
            attack = Managers.Data.CharacterDataDict[CharCode].attack;
            _maxhp = Managers.Data.CharacterDataDict[CharCode].maxhp;
            mp = 0;
            hp = _maxhp;
        }

        StartCoroutine(nameof(Income_Speed_CO));
    }

    IEnumerator Income_Speed_CO()
    {
        while (true)
        {
            AttackGage += speed;

            yield return new WaitForSeconds(2.5f);
        }

    }
    private void OnDisable()
    {

        StopCoroutine(nameof(Income_Speed_CO));
        StopCoroutine(nameof(Hit_color_Change_Co));
    }
    IEnumerator Hit_color_Change_Co()
    {
        Color Aftercolor = Color.red;
        Color Origincolor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = Aftercolor;

        yield return new WaitForSeconds(2.0f);

        gameObject.GetComponent<Image>().color = Origincolor;


    }

}
