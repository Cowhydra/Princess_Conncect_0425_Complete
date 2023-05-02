using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Fight_Icon : UI_Scene
{

    public int CharacterCode = -1;
    public bool isActive = false;
    Image _ActiveImage;
    enum Images
    {
        Character_Fight_Chceker,

    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        if (!CharacterCode.Equals(-1))
        {
            gameObject.GetComponent<Image>().sprite
         = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[CharacterCode].iconPath);
            gameObject.GetComponent<Canvas>().sortingOrder = 2020;
            _ActiveImage = GetImage((int)Images.Character_Fight_Chceker);
            _ActiveImage.gameObject.SetActive(false);
        }
        gameObject.BindEvent((PointerEventData data) => Active_DeActive());
        Set_Image();
        Managers.Stage.FightCharacter_UIRefresh -= Set_Image;
        Managers.Stage.FightCharacter_UIRefresh += Set_Image;
    }
    private void OnDisable()
    {
        Managers.Stage.FightCharacter_UIRefresh -= Set_Image;
    }
    private void Active_DeActive()
    {
        if (_ActiveImage.gameObject.activeSelf)
        {
            if (Managers.Stage.FInd_Delete_FightCharacter(CharacterCode))
            {
                _ActiveImage.gameObject.SetActive(false);

            }
        }
        else
        {

            if (Managers.Stage.FInd_Insert_FightCharacter(CharacterCode))
            {
                _ActiveImage.gameObject.SetActive(true);

            }
        }
    }

    private void Set_Image()
    {
        if (Managers.Stage.FightCharacter.Contains(CharacterCode))
        {
            _ActiveImage.gameObject.SetActive(true);
        }
        else
        {
            _ActiveImage.gameObject.SetActive(false);
        }
    }
}
