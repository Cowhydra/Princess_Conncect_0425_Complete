using UnityEngine;
using UnityEngine.UI;

public class Concept2_HPBar : UI_Scene
{
    // Start is called before the first frame update
    public Concept2AutoBattleController BattleStat;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        gameObject.transform.localPosition = new Vector2(0, 30);
        GetComponent<Canvas>().overrideSorting = true;
        GetComponent<Canvas>().sortingOrder = 8000;
        gameObject.transform.localScale = new Vector2(0.2f, 0.2f);
        SetUI();
        BattleStat.SliderBar_Refresh -= SetUI;
        BattleStat.SliderBar_Refresh += SetUI;
    }

    private void OnDisable()
    {
        BattleStat.SliderBar_Refresh -= SetUI;

    }
    private void SetUI()
    {

        gameObject.GetComponent<Slider>().value
            = BattleStat.Hp / BattleStat.MaxHp;

    }

}
