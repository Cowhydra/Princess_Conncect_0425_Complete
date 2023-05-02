using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Title;
        //ó���� ������ �ִ� Scene �� ������ ���ÿ�, ���� ScreenType�� �ʱ�ȭ
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
