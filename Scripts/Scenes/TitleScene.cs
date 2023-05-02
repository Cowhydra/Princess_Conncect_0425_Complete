using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Title;
        //처음에 가지고 있는 Scene 씬 생성과 동시에, 현재 ScreenType을 초기화
    }
    private void Start()
    {
        Init();
        Managers.UI.ShowWorldUI<Title>();

        Debug.Log("TitleScene Init()");

    }
    public override void Clear()
    {

        Debug.Log("TItleScene Clear!");
    }

}
