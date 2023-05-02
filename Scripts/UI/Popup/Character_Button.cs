using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Button : UI_Popup
{
    private void Start()
    {
        Init();
        SetImage(CharacterCode);
    }
    public int CharacterCode = -1;
    //�� ���ܿ���. ĳ���͸� ���� �� �ڵ带 �ο����ָ� ��
    enum Buttons
    {
        Character_Button,

    }
    enum Images
    {
        Character_Frame,
    }

    public override void Init()
    {

        Util.GetOrAddComponent<Character_Button>(gameObject);

        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        //  

        gameObject.BindEvent((PointerEventData data) => SetCharacter_InformationUI());


    }

    private void SetCharacter_InformationUI()
    {
        Character_Information_Can Char_INFO = Managers.UI.ShowSceneUI<Character_Information_Can>();
        Char_INFO.CharCode = CharacterCode;
        Char_INFO.transform.SetParent(gameObject.transform);

    }

    private void SetImage(int CharacterCode)
    {
        if (CharacterCode.Equals(-1)) return;

        gameObject.GetComponent<Image>().sprite
            = Managers.Resource.Load<Sprite>
          (Managers.Data.CharacterDataDict[CharacterCode].iconPath);
    }
    //��ư�� ������ ĳ���� ����â ���� �鼭, �ش� ĳ�� code �� �°� ��ȭ ��������մϴ�.
    // itemCode�� ���� ������ �����ͼ� ������ ���� ���� 
    //�� ���� ������ Charter Info�� ���;���.. ..��.. �׳� �̾��ְų�  �ڽ����� �ٱ� ������.. �׷��� 20���� ���� �ٸ�
    // �ֵ��� ��Ÿ���Ƿ�.. �޸� ���� ����.. �׷��� �׳� parent ���� ���� GeComponoet�� ���ݽô�.
    //���⼭ ���� �� ���ܿ���.. �ع����� �ɵ�?

}
