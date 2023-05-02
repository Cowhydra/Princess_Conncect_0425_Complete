using System;
using System.Collections;
using UnityEngine;


public class My_CharacterController : Creature_Controller
{
    public Action CharacterHMPUI;

    public int Char_MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Char_Mp
    {
        get { return _mp; }

        set
        {
            _mp = value;
            CharacterHMPUI?.Invoke();
        }
    }
    public int Char_Def { get { return _def; } set { _def = value; } }
    public int Char_Attack { get { return _attack; } set { _attack = value; } }
    public int Char_MaxMp { get { return _maxmp; } }
    public int Char_Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {
                _hp = 0;
                Die();
                StopCoroutine(nameof(Recovery));
                Managers.Stage.CharacterCount--;

                if (!Managers.Stage.IsGameClear.Equals((int)Define.IsGameClear.None))
                {
                    Set_EnemyState();
                    Debug.Log("IDLESTATE !");
                    GameEnd();
                }
            }
            CharacterHMPUI?.Invoke();
        }
    }

    private int charactercode = -1;
    private void Start()
    {

        Init();
    }
    public override void Init()
    {
        base.Init();
        //던전에 만들어진 캐릭터들이며,
        //해당 캐릭터들은 자신의 공격 및 방어.. 등의 스텟을 받아옵니다.
        //MyCharacters에서 스텟을 저장할 때 장비의 스텟을 고려하여 저장하므로, 장비를 여기서 굳이 따로 가져올 필요는 없습니다.

        _enemyTransform = GameObject.FindGameObjectsWithTag("Enemy");
        charactercode = int.Parse(gameObject.name);
        _maxhp = Managers.CharacterInventory.MyCharacters[charactercode].MaxHp;
        _maxmp = Managers.CharacterInventory.MyCharacters[charactercode].MaxMana;
        _attack = Managers.CharacterInventory.MyCharacters[charactercode].Attack
            + Managers.CharacterInventory.MyCharacters[charactercode].MagicAttack;
        _def = Managers.CharacterInventory.MyCharacters[charactercode].Def
            + Managers.CharacterInventory.MyCharacters[charactercode].MagicDef;
        Char_Hp = _maxhp;

        StartCoroutine(nameof(Recovery));

    }


    private IEnumerator Recovery()
    {
        while (true)
        {
            Char_Hp += 5;
            if (Char_Hp > Char_MaxHp)
            {
                Char_Hp = Char_MaxHp;
            }
            else if (Char_Hp <= 0)
            {
                Char_Hp = 0;
                yield break;
            }
            Char_Mp += 10;
            if (Char_Mp > Char_MaxMp)
            {
                Char_Mp = Char_MaxMp;
            }
            CharacterHMPUI?.Invoke();
            yield return new WaitForSeconds(2.0f);

        }

    }


}
