using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Icon : UI_Scene
{
    public int CharacterCode = -1;
    Character_Information_Can Char_INFO;
    private void Start()
    {
        Init();
        SetImage(CharacterCode);
        Char_INFO = transform.parent.parent.GetComponentInChildren<Character_Information_Can>();
        //Character 스텟 정보 표기를 위한 데이터 전송을 위해 만들어졌습니다.
        // 해당 방법보다는 추후에는 ACtion 을 배워서 Action을 사용했습니다.
        GetComponent<Canvas>().sortingOrder = 250;
    }
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

        // base.Init();
        Util.GetOrAddComponent<Character_Icon>(gameObject);

        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        //  


        Get<Button>((int)Buttons.Character_Button).gameObject
            .BindEvent((PointerEventData data) => SetCharacter_InformationUI());

    }

    private void SetCharacter_InformationUI()
    {

        Char_INFO = Managers.UI.ShowSceneUI<Character_Information_Can>();
        Char_INFO.CharCode = CharacterCode;
        Char_INFO.transform.SetParent(gameObject.transform);



    }

    private void SetImage(int CharacterCode)
    {
        if (CharacterCode.Equals(-1)) return;
        Image images = Get<Button>((int)Buttons.Character_Button).GetComponent<Image>();
        images.sprite
            = Managers.Resource.Load<Sprite>
          (Managers.Data.CharacterDataDict[CharacterCode].iconPath);

        if (!Managers.CharacterInventory.MyCharacters[CharacterCode].IsActive)
        {
            Color color = images.color;
            color.a = 0.5f;
            images.color = color;
        }
    }

}
