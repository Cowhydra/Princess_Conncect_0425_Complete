using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setting : UI_Popup
{
    enum Buttons
    {
        ClosedButton,
    }
    enum Sliders
    {
        BGM_Volum_Controller,
        Effect_Volum_Controller,

    }


    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        Get<Slider>((int)Sliders.BGM_Volum_Controller).value
            = Managers.Sound.CurrentVolumn(Define.Sound.Bgm);
        Get<Slider>((int)Sliders.Effect_Volum_Controller).value
            = Managers.Sound.CurrentVolumn(Define.Sound.Effect);

        Get<Slider>((int)Sliders.BGM_Volum_Controller).onValueChanged.AddListener(value => SelectVolumn(Define.Sound.Bgm, value));
        Get<Slider>((int)Sliders.Effect_Volum_Controller).onValueChanged.AddListener(value => SelectVolumn(Define.Sound.Effect, value));
        GetButton((int)Buttons.ClosedButton).gameObject.BindEvent
            ((PointerEventData data) => ClosePopupUI());
    }
    private void SelectVolumn(Define.Sound type, float value)
    {
        float volume = Mathf.Lerp(0, 1, value);

        Managers.Sound.SelectVolumn(type, volume);

    }



}
