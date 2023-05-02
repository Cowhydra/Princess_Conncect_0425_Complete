using System;
using System.Collections.Generic;

public class CharacterInventoryManager
{

    public Dictionary<int, MyCharacter> MyCharacters { get; } = new Dictionary<int, MyCharacter>();
    //내키릭터 인벤토리 ( 내가 보유한 캐릭터만 알 수 있음 )
    public void Init()
    {
        foreach (int i in Managers.Data.CharacterDataDict.Keys)
        {
            MyCharacters.Add(i, MyCharacter.MakeCharacter(Managers.Data.CharacterDataDict[i]));
        }
    }
    public bool AddCharacter(int charactercode)
    {
        MyCharacter character = MyCharacters[charactercode];
        if (character.IsActive.Equals(false))
        {
            MyCharacters[charactercode].IsActive = true;
        }
        return AddCharacter(character);
    }


    private bool AddCharacter(MyCharacter character)
    {
        if (character.IsActive)
        {
            //아이템 중 캐릭터 조각에 관련된 아이템 코드를 찾아서
            //추가해주는 로직
            //현재는 하드코딩이지만 character의 등급에 따라 개수 차등 적용 쉽게 가능

            Managers.ItemInventory.Add(5001, 20);
        }

        return true;
    }

    public MyCharacter Find(Func<MyCharacter, bool> condition)
    {
        foreach (MyCharacter character in MyCharacters.Values)
        {
            if (condition.Invoke(character))
                return character;
        }

        return null;
    }
    public MyCharacter Get(int charactercode)
    {
        MyCharacter character = null;
        MyCharacters.TryGetValue(charactercode, out character);
        return character;
    }
    public void Clear()
    {
        MyCharacters.Clear();
    }
}



