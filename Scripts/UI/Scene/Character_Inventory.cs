using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character_Inventory : MonoBehaviour
{
    private void OnEnable()
    {
        Init();



    }
    public void Init()
    {
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        List<MyCharacter> SortedList = Managers.CharacterInventory.MyCharacters.Values.ToList();
        foreach (MyCharacter myCharacter in SortedList.OrderByDescending(s => s.IsActive))
        {
            Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
            go.CharacterCode = myCharacter.CharacterCode;
            go.transform.SetParent(gameObject.transform);
        }
    }
    public void Sorting(Define.Characters_SortingOption SortType)
    {
        List<MyCharacter> SortedList = Managers.CharacterInventory.MyCharacters.Values.ToList();
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        switch (SortType)
        {
            case Define.Characters_SortingOption.CharacterName:

                foreach (MyCharacter myCharacter in SortedList.OrderBy(s => s.TMI))
                {
                    Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
                    go.CharacterCode = myCharacter.CharacterCode;
                    go.transform.SetParent(gameObject.transform);
                }
                break;
            case Define.Characters_SortingOption.CharacterActive:
                foreach (MyCharacter myCharacter in SortedList.OrderByDescending(s => s.IsActive))
                {
                    Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
                    go.CharacterCode = myCharacter.CharacterCode;
                    go.transform.SetParent(gameObject.transform);
                }
                break;
            case Define.Characters_SortingOption.CharacterAttack:
                foreach (MyCharacter myCharacter in SortedList.OrderByDescending(s => s.Attack))
                {
                    Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
                    go.CharacterCode = myCharacter.CharacterCode;
                    go.transform.SetParent(gameObject.transform);
                }
                break;
            case Define.Characters_SortingOption.CharacterMagicAttack:
                foreach (MyCharacter myCharacter in SortedList.OrderByDescending(s => s.MagicAttack))
                {
                    Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
                    go.CharacterCode = myCharacter.CharacterCode;
                    go.transform.SetParent(gameObject.transform);
                }
                break;
            default:
                foreach (MyCharacter myCharacter in SortedList.OrderByDescending(s => s.IsActive))
                {
                    Character_Icon go = Managers.UI.ShowSceneUI<Character_Icon>();
                    go.CharacterCode = myCharacter.CharacterCode;
                    go.transform.SetParent(gameObject.transform);
                }
                break;
        }

    }

}
