using System;

public class Player
{

    //이 게임에서는 플레이어는 유일한 존재이며 특성으로 플레이어 레벨, 골드, 다이아를 가지고 있습니다.
    //다이아를 사용하여 캐릭터 뽑기 등 가능, 플레이어 레벨에 따라 최대 스테미너 증가
    public Action UIExchange;

    private int _gold;
    public int Gold
    {
        get { return _gold; }
        set
        {

            _gold = value;
            UIExchange?.Invoke();
            //골드 값이 변경될 떄 마다 UIExchange 이벤트를 실행시켜줍니다.
            //관련된 UI를 모두 UI Exchage에 구독 신청해서, 모두 실행해줌
        }
    }
    private int _diamond;
    public int DiaMond
    {
        get { return _diamond; }
        set
        {
            _diamond = value;
            UIExchange?.Invoke();
        }
    }
    private int _playerlevel = 1;


    public int PlayerLevel
    {
        get { return _playerlevel; }
        set
        {

            _playerlevel = value;
            UIExchange?.Invoke();
        }
    }
    public int TotalUseMoney { get; set; }
    private int _stamina = 100;
    private int _maxstamina = 100;
    public int MaxStamina
    {
        get { return _maxstamina; }
        set
        {

            _maxstamina = value;
            UIExchange?.Invoke();
            //던전 입장 시 필요한 재화
        }
    }
    public int Stamina
    {
        get { return _stamina; }
        set
        {

            _stamina = value;
            UIExchange?.Invoke();
        }
    }
    private int _exp;
    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            if (Exp > RequireExp)
            {
                _playerlevel++;
                Exp -= RequireExp;
                MaxStamina = _stamina + _playerlevel * 10;
                Stamina = MaxStamina;
                UIExchange?.Invoke();
            }

        }
    }
    public int RequireExp { get { return _playerlevel * 20 + 8; } }

    private int _select_UI_CharacterCode;
    public int Select_UI_CharacterCode { get { return _select_UI_CharacterCode; } set { _select_UI_CharacterCode = value; } }

    private int _charcter_Exchage_Count;
    public int Charcter_Exchage_Count
    {
        get { return _charcter_Exchage_Count; }
        set { _charcter_Exchage_Count = value; }
    }
}
