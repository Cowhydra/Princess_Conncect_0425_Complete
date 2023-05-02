using UnityEngine;

public class Equipment_Area : MonoBehaviour
{
    public int CharacterCode = -1;
    private void Start()
    {
        CharacterCode = GetComponentInParent<Character_Information_Can>().CharCode;
        Init();
    }

    private void Init()
    {
        Debug.Log("Equipment_Area Init");

        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        for (int i = 0; i < (int)Define.ItemType.Consume; i++)
        {
            if (CharacterCode.Equals(-1)) return;
            Equipment_Icon go = Managers.UI.ShowSceneUI<Equipment_Icon>();
            go.transform.SetParent(gameObject.transform);
            go._itemType = (Define.ItemType)i;
            go.CharacterCode = CharacterCode;


        }

    }


}
