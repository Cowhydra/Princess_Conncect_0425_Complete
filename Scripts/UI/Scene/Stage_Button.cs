using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage_Button : UI_Scene
{

    public int StageCode;
    public int Acquire_Star;
    private void Start()
    {
        Init();
        GetComponent<Canvas>().sortingOrder = 1015;
    }
    enum Texts
    {
        Stage_Button_Text,

    }
    enum Images
    {
        StageClear_Star_Active1,
        StageClear_Star_Active2,
        StageClear_Star_Active3

    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        GetImage((int)Images.StageClear_Star_Active1).gameObject.SetActive(false);
        GetImage((int)Images.StageClear_Star_Active2).gameObject.SetActive(false);
        GetImage((int)Images.StageClear_Star_Active3).gameObject.SetActive(false);
        if (!StageCode.Equals(0))
        {
            SetText();
        }
        gameObject.BindEvent((PointerEventData data) => Show_StageInfo());
        if ((StageCode % 3).Equals(0))
        {
            gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Images/Stages/StageIcon2");
        }

        if ((StageCode % 10).Equals(0))
        {
            gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Images/Stages/BossIcon");

        }
        SetStageStarUI();

    }
    void SetText()
    {
        GetText((int)Texts.Stage_Button_Text).text =
              $"{Managers.Data.StageDataDict[StageCode].stageName}";

    }
    void SetStageStarUI()
    {


        if (Acquire_Star.Equals(0)) return;
        for (int i = 0; i < Acquire_Star; i++)
        {
            GetImage(i).gameObject.SetActive(true);
        }
    }
    void Show_StageInfo()
    {
        StageInfo_Can StageInfo = Managers.UI.ShowSceneUI<StageInfo_Can>();
        StageInfo.transform.SetParent(Managers.UI.Root.transform);
        StageInfo.StageCode = StageCode;
        Managers.Stage.Set_Monster(StageCode);
        Managers.Stage.StageCode = StageCode;

    }

}
