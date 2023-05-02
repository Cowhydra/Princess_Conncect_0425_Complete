using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_Gacha_Result : UI_Scene
{

    public int Gacha_Result_Code;
    private bool isduplication;
    public float ReleaseTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = 3000;
        StartCoroutine(nameof(SetUI_CO), ReleaseTime);

    }
    private IEnumerator SetUI_CO(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<GraphicRaycaster>().enabled = false;
        if (Managers.CharacterInventory.MyCharacters[Gacha_Result_Code].IsActive)
        {
            isduplication = true;
        }
        gameObject.GetComponent<Image>().sprite
          = Managers.Resource.Load<Sprite>($"{Managers.Data.CharacterDataDict[Gacha_Result_Code].iconPath}");
        Managers.CharacterInventory.AddCharacter(Gacha_Result_Code);
        if (isduplication)
        {
            yield return new WaitForSeconds(3.3f);
            gameObject.GetComponent<Image>().sprite
              = Managers.Resource.Load<Sprite>($"Images/Equipment/ETC/5001");
            GetComponentInChildren<TextMeshProUGUI>().text = "20";
        }
        yield break;

    }

    //IEnumerator UI_Change_Effect()
    //{
    //    if (isduplication)
    //    {
    //        yield return new WaitForSeconds(3.3f);
    //        gameObject.GetComponent<Image>().sprite
    //          = Managers.Resource.Load<Sprite>($"Images/Equipment/ETC/5001");
    //        GetComponentInChildren<TextMeshProUGUI>().text = "20";
    //    }
    //    yield break;
    //}
}
