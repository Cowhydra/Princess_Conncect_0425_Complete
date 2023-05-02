using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AboutStage : UI_Popup
{
    private void Start()
    {
        Init();
    }
    enum GameObjects
    {
        Stage_Interaction,
    }
    enum Buttons
    {
        Stage_CloseButton,

    }

    enum Texts
    {
        Stage_Information_Text,

    }
    enum Images
    {
        Stage_BackGround,
        Stage_Information,
        Stage_Information_Icon,

    }
    //스테이지 관련된 UI
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Managers.Stage.StageClear_UI -= SetStar;
        Managers.Stage.StageClear_UI += SetStar;


        SetStar();


    }
    private void OnDisable()
    {
        Managers.Stage.StageClear_UI -= SetStar;
    }
    private void SetStar()
    {
        foreach (Transform transforom in Get<GameObject>((int)GameObjects.Stage_Interaction).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (int i in Managers.Data.StageDataDict.Keys)
        {

            Stage_Button go = Managers.UI.ShowSceneUI<Stage_Button>();
            go.StageCode = i;
            go.transform.SetParent(Get<GameObject>((int)GameObjects.Stage_Interaction).transform);
            go.Acquire_Star = Managers.Stage.StageClearData[i];

            //스테이지 미클리어 시 화면 알파값 조정 및 버튼 Raycast 비활성
            if (i.Equals(1)) continue;
            else
            {
                if (Managers.Stage.StageClearData[i - 1].Equals(0))
                {
                    go.GetComponent<GraphicRaycaster>().enabled = false;
                    Color clor = go.GetComponent<Image>().color;
                    clor.a = 0.5f;
                    go.GetComponent<Image>().color = clor;
                }

            }

        }
        GetButton((int)Buttons.Stage_CloseButton).gameObject.BindEvent((PointerEventData data) => ClosePopupUI());

    }

}
