using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageInfo_Can : UI_Scene
{
    public int StageCode;
    enum Texts
    {
        Challange_Button_Text,
        Cancel_Button_Text,
        Stage_Reward_Common_Text,
        Stage_Monster_Common_Text,
        Stage_StarReward_common_Text,
        StageInfo_Reward_ImageStar01_Text,
        StageInfo_Reward_ImageStar02_Text,
        StageInfo_Reward_ImageStar03_Text,


    }
    enum Images
    {
        StageInfo_Reward_ImageMon01,
        StageInfo_Reward_ImageMon02,
        StageInfo_Reward_ImageMon03,
        StageInfo_Reward_ImageMon04,
        StageInfo_Reward_ImageItem01,
        StageInfo_Reward_ImageItem02,
        StageInfo_Reward_ImageItem03,
        StageInfo_Reward_ImageItem04,
        StageInfo_Reward_ImageStar01,
        StageInfo_Reward_ImageStar02,
        StageInfo_Reward_ImageStar03,
        StageInfo_Reward_ImageStar04,
        Stage_Reward_Common_Icon,
        Stage_Monster_Common_Icon,
        Stage_StarReward_common_Icon,
        StageInfo_Image,
    }
    enum GameObjects
    {
        StageInfo,
        Stage_Common,
        StageInfo_Reward,

    }
    enum Buttons
    {
        Challange_Button,
        Cancel_Button,

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
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        GetComponent<Canvas>().sortingOrder = 1016;
        GetButton((int)Buttons.Cancel_Button).gameObject
            .BindEvent((PointerEventData data) => Managers.Resource.Destroy(gameObject));
        GetImage((int)Images.StageInfo_Image).sprite = Managers.Resource.Load<Sprite>(
           $"Images/Comic/{StageCode}");
        GetButton((int)Buttons.Challange_Button).gameObject
          .BindEvent((PointerEventData data) => Show_SelectFightCharacter());

        GetText((int)Texts.Stage_Monster_Common_Text).text = "»ó´ë!";
        SetRewardIJewel();
        SetRewardItem();
        SetMonster();

    }
    private void Show_SelectFightCharacter()
    {
        Managers.UI.ShowSceneUI<Select_Fight_Character>();
        Managers.Resource.Destroy(gameObject);

    }
    private void SetRewardIJewel()
    {
        string[] Rewards = Managers.Data.StageDataDict[StageCode].rewardJewel.Split(',');
        GetImage((int)Images.StageInfo_Reward_ImageStar01).sprite
            = Managers.Resource.Load<Sprite>("Images/Goods/Diamond1");
        GetImage((int)Images.StageInfo_Reward_ImageStar02).sprite
           = Managers.Resource.Load<Sprite>("Images/Goods/Diamond1");
        GetImage((int)Images.StageInfo_Reward_ImageStar03).sprite
           = Managers.Resource.Load<Sprite>("Images/Goods/Diamond2");

        GetText((int)Texts.StageInfo_Reward_ImageStar01_Text).text = $"{Rewards[0]}";
        GetText((int)Texts.StageInfo_Reward_ImageStar02_Text).text = $"{Rewards[1]}";
        GetText((int)Texts.StageInfo_Reward_ImageStar03_Text).text = $"{Rewards[2]}";
    }
    private void SetRewardItem()
    {
        string[] Rewards = Managers.Data.StageDataDict[StageCode].rewarditemCode.Split(',');

        GetImage((int)Images.StageInfo_Reward_ImageItem01).sprite
          = Managers.Resource.Load<Sprite>(Managers.Data.ItemDataDict[int.Parse(Rewards[0])].iconpath);
        GetImage((int)Images.StageInfo_Reward_ImageItem02).sprite
           = Managers.Resource.Load<Sprite>(Managers.Data.ItemDataDict[int.Parse(Rewards[1])].iconpath);

    }
    private void SetMonster()
    {
        string[] Monsters = Managers.Data.StageDataDict[StageCode].monsterCode.Split(',');
        GetImage((int)Images.StageInfo_Reward_ImageMon01).sprite
            = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[int.Parse(Monsters[0])].iconPath);
        GetImage((int)Images.StageInfo_Reward_ImageMon02).sprite
            = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[int.Parse(Monsters[1])].iconPath);
        GetImage((int)Images.StageInfo_Reward_ImageMon03).sprite
            = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[int.Parse(Monsters[2])].iconPath);
        GetImage((int)Images.StageInfo_Reward_ImageMon04).sprite
            = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[int.Parse(Monsters[3])].iconPath);
    }
}
