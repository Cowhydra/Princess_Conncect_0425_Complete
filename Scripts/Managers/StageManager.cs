using System;
using System.Collections.Generic;

public class StageManager
{
    public Action FightCharacter_UIRefresh;
    public Action StageClear_UI;
    public Action StageClear_UI_Concept2;
    List<int> fightCharacter = new List<int>(4) { -1, -1, -1, -1 };
    List<int> fightMonster = new List<int>(4) { -1, -1, -1, -1 };
    public List<int> FightCharacter { get { return fightCharacter; } set { fightCharacter = value; } }
    public int IsGameClear
    {
        get { return _isgameClear; }
        set
        {
            _isgameClear = value;
            StageClear_UI_Concept2?.Invoke();

        }

    }
    private int _isgameClear = 0; //0 진행중 , 1 클리어 ,2실패
    public int StageCode = -1;
    public List<int> FightMonster { get { return fightMonster; } }
    public Dictionary<int, int> StageClearData = new Dictionary<int, int>();


    public void init()
    {
        foreach (int i in Managers.Data.StageDataDict.Keys)
        {
            StageClearData.Add(i, 0);
        }

    }
    public void GetExp()
    {
        foreach (int i in fightCharacter)
        {
            if (!i.Equals(-1))
            {
                Managers.CharacterInventory.MyCharacters[i].Exp
                    += Managers.Data.StageDataDict[StageCode].rewardexp;
            }
        }
    }
    public void ClearDataRenew(int value)
    {
        StageClearData[StageCode] = value;
    }

    public void Set_Monster(int StageCode)
    {
        fightMonster.Clear();
        string[] MonsterCode = Managers.Data.StageDataDict[StageCode].monsterCode.Split(',');
        if (MonsterCode.Length > 4) return;
        for (int i = 0; i < MonsterCode.Length; i++)
        {
            fightMonster.Add(int.Parse(MonsterCode[i]));
        }
    }
    public void FightCharacter_Reset()
    {
        fightCharacter = new List<int>(4) { -1, -1, -1, -1 };
        FightCharacter_UIRefresh?.Invoke();
    }
    public bool FInd_Insert_FightCharacter(int i)
    {
        bool isOk = false;
        if (fightCharacter.Contains(-1))
        {
            fightCharacter.Remove(-1);
            fightCharacter.Add(i);
            isOk = true;
        }

        FightCharacter_UIRefresh?.Invoke();
        return isOk;
    }
    public bool FInd_Delete_FightCharacter(int i)
    {
        bool isOk = false;
        if (fightCharacter.Contains(i))
        {
            fightCharacter.Remove(i);
            fightCharacter.Add(-1);
            isOk = true;

        }

        FightCharacter_UIRefresh?.Invoke();
        return isOk;
    }
    private int _charcount = 0;
    private int _moncount = 0;
    public int CharacterCount
    {
        get { return _charcount; }

        set
        {
            _charcount = value;
            if (_charcount.Equals(0))
            {
                _isgameClear = (int)Define.IsGameClear.Fail;

            }
        }
    }
    public int MonsterCount
    {
        get { return _moncount; }

        set
        {
            _moncount = value;
            if (_moncount.Equals(0))
            {
                _isgameClear = (int)Define.IsGameClear.Clear;

            }

        }
    }
    public int GetMonsterCount()
    {
        int count = 0;
        count = Managers.Data.StageDataDict[StageCode].monsterCode.Split(',').Length;
        _moncount = count;
        return count;
    }
    public int GetCharacterCount()
    {
        int count = 0;
        for (int i = 0; i < FightCharacter.Count; i++)
        {
            if (!FightCharacter[i].Equals(-1))
            {
                count++;
            }
        }
        _charcount = count;
        return count;
    }
    #region Concept2
    private int _bossHp = 500;
    private int _playerHp = 500;
    public int BossHp
    {
        get { return _bossHp; }
        set
        {
            _bossHp = value;
            if (_bossHp <= 0)
            {
                IsGameClear = 1;
            }
        }
    }
    public int PlayerHp
    {
        get { return _playerHp; }
        set
        {
            _playerHp = value;
            if (_playerHp <= 0)
            {
                IsGameClear = 2;
            }
        }
    }
    public int SelectCharacterConcept2;


    #endregion
}
