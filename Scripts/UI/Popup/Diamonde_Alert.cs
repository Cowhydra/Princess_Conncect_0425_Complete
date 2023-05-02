using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Diamonde_Alert : UI_Popup
{

    public int Alertcode = 0;
    enum Buttons
    {
        Button_Yes,
        Button_No,

    }
    enum Texts
    {
        Alert_Text,

    }
    void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetComponent<Canvas>().sortingOrder = 11111;
        GetButton((int)Buttons.Button_Yes).gameObject.BindEvent
            ((PointerEventData data) => GOTOSHOP());
        GetButton((int)Buttons.Button_No).gameObject.BindEvent
          ((PointerEventData data) => Managers.UI.ClosePopupUI());
        Set_Text(Alertcode);
    }

    private void GOTOSHOP()
    {
        Managers.UI.CloseAllPopupUI();
        transform.parent.transform.Find("PriconeSHOP").gameObject
            .GetComponent<PriconeSHOP>().Set_active(0);
    }
    public void Set_Text(int n)
    {
        if (n.Equals(1))
        {
            GetText((int)Texts.Alert_Text).text = $"���̾ư� �����ϴ�, ���̾� ������ ��Ź�帳�ϴ�. " +
                $"���̾Ƹ� �����Ϸ� ���ðڽ��ϱ�?";
        }

    }
}
