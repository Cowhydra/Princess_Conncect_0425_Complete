using System.Collections;
using UnityEngine;

public class Enemy_CharacterController : Creature_Controller
{


    private int _hpRe = 5;
    private int _mpRe = 5;
    public int Enemy_MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Enemy_Mp { get { return _mp; } set { _mp = value; } }
    public int Enemy_Def { get { return _def; } set { _def = value; } }
    public int Enemy_Attack { get { return _attack; } set { _attack = value; } }
    public int Enemy_Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {
                _hp = 0;
                Die();
                Managers.Stage.MonsterCount--;

                if (!Managers.Stage.IsGameClear.Equals((int)Define.IsGameClear.None))
                {
                    Set_EnemyState();
                    GameEnd();
                }
            }

        }
    }
    private void Start()
    {
        Init();

    }
    public override void Init()
    {
        //적 캐릭터의 스텟, 및 적의 위치를 저장
        //적의 경우 오른쪽 -> 왼쪽으로 움직이니, 게임 오브젝트의 x 스케일 값을 음수로 전환하여 반대를 바라볼 수 있도록 설정

        base.Init();
        Enemy_MaxHp = 1000;
        Enemy_Mp = 0;
        Enemy_Def = 100;
        Enemy_Attack = 10;
        Enemy_Hp = Enemy_MaxHp;
        movedir = -1;
        _enemyTransform = GameObject.FindGameObjectsWithTag("Character");
        gameObject.transform.localScale
            = new Vector3(-gameObject.transform.localScale.x,
            gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        StartCoroutine(nameof(HMP_Recovery_Co));


    }


    private IEnumerator HMP_Recovery_Co()
    {
        //자동 체력 재생 합수입니다. 체력이 0 이되면 COutrtion이 종료되며,
        //그전까지는 2.0초를 기준으로 무한하게 체력을 증가시켜줍니다.
        while (true)
        {
            Enemy_Hp += _hpRe;
            Enemy_Mp += _mpRe;
            if (Enemy_Hp <= 0)
            {
                Enemy_Hp = 0;
                yield break;
            }
            else if (Enemy_Hp >= Enemy_MaxHp)
            {
                Enemy_Hp = Enemy_MaxHp;
            }
            if (Enemy_Mp > _maxmp)
            {
                Enemy_Mp = _maxmp;
            }
            yield return new WaitForSeconds(2.0f);
        }


    }



}
