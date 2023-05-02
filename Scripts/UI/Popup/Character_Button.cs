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
    //더 윗단에서. 캐릭터를 만들 때 코드를 부여해주면 됨
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
    //버튼을 누르면 캐릭터 인포창 나오 면서, 해당 캐릭 code 에 맞게 변화 시켜줘야합니다.
    // itemCode를 통해 정보를 가져와서 정보를 전부 변경 
    //이 턴을 누르면 Charter Info가 나와야함.. ..음.. 그냥 이어주거나  자식으로 줄까 했지만.. 그러면 20개의 각기 다른
    // 애들이 나타나므로.. 메모리 낭비 같은.. 그래서 그냥 parent 등을 통해 GeComponoet를 해줍시다.
    //여기서 말고 더 윗단에서.. 해버리면 될듯?

}
