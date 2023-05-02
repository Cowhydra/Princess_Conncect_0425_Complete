public class Define
{
    public enum Scene
    {
        Unknown,
        Title,
        Game,

    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        OnDrag,
        PointerEnter,
        PointerExit,
        OnBeginDrag,
        OnEndDrag,
        OnDrop

    }

    public enum Characters
    {

        MaxCount = 15,

    }
    public enum JobType
    {
        Wizard,
        Warrior,
        Archer,
        Buffer,
        All,

    }

    public enum ItemType
    {
        Weapon,
        Hat,
        Cloth,
        Boot,
        Earring,
        Necklace,
        Accessory,
        Ring,
        Consume,
        ETC,
        Max,

    }



    public enum Characters_Name
    {
        HoMaru
, Jita
, KarYu
, KasuMi
, KoKoRo
, KuRuMi
, MaKoTo
, MaSuMi
, NoJoMi
, PeKo
, RiNo
, Yui

    }

    public enum Characters_SortingOption
    {
        CharacterName,
        CharacterActive,
        CharacterAttack,
        CharacterMagicAttack,

    }
    public enum Current_Card_Max
    {
        CardMax = 18,

    }
    public enum CreatureState
    {
        IDLE,
        ATK,
        DIE,
        HIT,
        SKILL,
        RUN,

    }
    public enum IsGameClear
    {
        None,
        Clear,
        Fail,
    }
}
