using System.Collections;
using UnityEngine;
public class Alert_Image_For_Shop : UI_Popup
{

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = 1000;
        StartCoroutine(nameof(DestoryCouroutine));
    }
    IEnumerator DestoryCouroutine()
    {
        yield return new WaitForSeconds(2.2f);

        Managers.UI.ClosePopupUI();

        yield break;
    }

}
