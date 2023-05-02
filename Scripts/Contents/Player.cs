using System;

public class Player
{

    //�� ���ӿ����� �÷��̾�� ������ �����̸� Ư������ �÷��̾� ����, ���, ���̾Ƹ� ������ �ֽ��ϴ�.
    //���̾Ƹ� ����Ͽ� ĳ���� �̱� �� ����, �÷��̾� ������ ���� �ִ� ���׹̳� ����
    public Action UIExchange;

    private int _gold;
    public int Gold
    {
        get { return _gold; }
        set
        {

            _gold = value;
            UIExchange?.Invoke();
            //��� ���� ����� �� ���� UIExchange �̺�Ʈ�� ��������ݴϴ�.
            //���õ� UI�� ��� UI Exchage�� ���� ��û�ؼ�, ��� ��������
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
            //���� ���� �� �ʿ��� ��ȭ
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
