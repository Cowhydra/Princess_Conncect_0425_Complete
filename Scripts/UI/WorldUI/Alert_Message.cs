using TMPro;
using UnityEngine;

public class Alert_Message : UI_Scene
{

    TextMeshProUGUI texts;
    public int Errcode = 1;
    void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();
        texts = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Canvas>().sortingOrder = 13333;
        texts.text = ErrorCase(Errcode);
        Destroy(gameObject, 1.5f);
    }

    private string ErrorCase(int n)
    {
        string text = string.Empty;
        switch (n)
        {
            case 0:
                text = "캐릭터가 부족합니다.";
                break;
            case 1:
                break;
        }

        return text;
    }


}
