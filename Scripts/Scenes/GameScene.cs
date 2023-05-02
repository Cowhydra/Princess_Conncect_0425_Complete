using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<PriconeSHOP>();
        Managers.UI.ShowSceneUI<Contents>();
        int CurrentCard = PlayerPrefs.GetInt("BackGround");
        GameObject.FindGameObjectWithTag("BackGround").transform
            .GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/BackGround/{CurrentCard}");
        PlayerPrefs.SetInt("BackGround", CurrentCard);
        Managers.Sound.Play("BGM3", Define.Sound.Bgm);
    }

    public override void Clear()
    {

    }
}
