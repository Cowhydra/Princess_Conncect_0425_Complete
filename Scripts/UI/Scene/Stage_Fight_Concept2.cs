using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage_Fight_Concept2 : UI_Scene
{
    float Realtime;
    private const float MaxCost = 25f;
    private float _currcost = 0;
    private float initialcost = 5;
    private float MyTimeSclae = 1.0f;
    private List<int> RewardCode = new List<int>();
    public float CurrCost
    {
        get { return _currcost; }
        set
        {
            _currcost = value;
        }
    }
    public GameObject MyCharacterPostion;
    public GameObject EnemyCharacterPostion;
    private List<int> EnemyPosition;
    private Slider CostBar;
    private void Start()
    {
        Init();
        Realtime = Time.time;
    }

    enum GameObjects
    {
        BattleStart,
        MyCharacter_Position,
        Enemy_Position,
        Bottom_Interaction_UI,
        Creature_Position,
        WinCase,
        FailedCase,
        Star1,
        Star2,
        Star3,

    }
    enum Buttons
    {
        Fast_Button,
        FastFast_Button,
        Pause_Button,
        Exit_Button,

    }
    enum Sliders
    {
        Cost_Slider,

    }
    enum Texts
    {
        Cost_Slider_Text,
        BattleStart_Text,

    }
    enum Images
    {
        Reward1,
        Reward2,
    }
    public override void Init()
    {
        base.Init();
        int monstercount = 0;
        _currcost = initialcost;
        Managers.Sound.Pause_Bgm();
        Managers.Sound.Play("BGM6");
        Managers.Stage.StageClear_UI_Concept2 -= GameEnd;
        Managers.Stage.StageClear_UI_Concept2 += GameEnd;

        Managers.Stage.CharacterCount = Managers.Stage.GetCharacterCount();
        Managers.Stage.MonsterCount = Managers.Stage.GetMonsterCount();
        EnemyPosition = new List<int>(Managers.Stage.MonsterCount);
        gameObject.GetComponent<Canvas>().sortingOrder = 2000;

        for (int i = 0; i < Managers.Stage.MonsterCount; i++)
        {
            while (true)
            {
                int c = UnityEngine.Random.Range(1, 100 + 50 * i) % 8;
                if (c.Equals(5)) continue;
                if (!EnemyPosition.Contains(c))
                {
                    EnemyPosition.Add(c);
                    break;
                }
            }

        }
        Bind<GameObject>(typeof(GameObjects));
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        CostBar = Get<Slider>((int)Sliders.Cost_Slider);
        MyCharacterPostion = Get<GameObject>((int)GameObjects.MyCharacter_Position);
        EnemyCharacterPostion = Get<GameObject>((int)GameObjects.Enemy_Position);

        #region  초기 적 생성 및 캐릭터 블록 생성, 내캐릭터 UI 갱신 ( 적도 AI로 생성하면 좋으나 시간 관계상)
        foreach (Transform trans in MyCharacterPostion.transform.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(trans.gameObject);
        }
        for (int i = 0; i < 9; i++)
        {
            Position_Button_UI_Concept2 MyPos = Managers.UI.ShowSceneUI<Position_Button_UI_Concept2>();
            MyPos.ThisPosition = i;
            MyPos.transform.SetParent(MyCharacterPostion.transform);
            MyPos.tag = "Position";
            MyPos.IsOkSummon = true;
            if (i.Equals(3))
            {
                MyPos.CreateCharacter("Character", 1, "Player");
                MyPos.IsOkSummon = false;
            }

        }
        foreach (Transform trans in EnemyCharacterPostion.transform.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(trans.gameObject);
        }
        for (int i = 0; i < 9; i++)
        {

            Position_Button_UI_Concept2 EnemyPos = Managers.UI.ShowSceneUI<Position_Button_UI_Concept2>();
            EnemyPos.ThisPosition = i;
            EnemyPos.transform.SetParent(EnemyCharacterPostion.transform);
            EnemyPos.tag = "Position";
            EnemyPos.IsOkSummon = false;
            if (EnemyPosition.Contains(i))
            {
                EnemyPos.CreateCharacter("Enemy", Managers.Stage.FightMonster[monstercount]);
                monstercount++;
            }
            else if (i.Equals(5))
            {
                EnemyPos.CreateCharacter("Enemy", 1, "Boss");
            }

        }
        foreach (Transform trans in Get<GameObject>((int)GameObjects.Bottom_Interaction_UI).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(trans.gameObject);
        }
        for (int i = 0; i < Managers.Stage.FightCharacter.Count; i++)
        {
            if (Managers.Stage.FightCharacter[i].Equals(-1)) continue;
            Bottom_Character_UI_Concept2 MyCharacter_Bottom_UI = Managers.UI.ShowSceneUI<Bottom_Character_UI_Concept2>();
            MyCharacter_Bottom_UI.Stage_Conecept2 = this;
            MyCharacter_Bottom_UI.transform.SetParent(Get<GameObject>((int)GameObjects.Bottom_Interaction_UI).transform);
            MyCharacter_Bottom_UI.CharCode = Managers.Stage.FightCharacter[i];
            // MyCharacter_Bottom_UI.SetUI();


        }

        #endregion

        Managers.Resource.Destroy(Get<GameObject>((int)GameObjects.BattleStart).gameObject, 2.5f);

        for (int i = 0; i < Managers.Data.StageDataDict[Managers.Stage.StageCode].rewarditemCode
          .Split(',').Length; i++)
        {
            int code = int.Parse(Managers.Data.StageDataDict[Managers.Stage.StageCode].rewarditemCode.Split(',')[i]);
            GetImage(i).sprite =
             Managers.Resource.Load<Sprite>
             (Managers.Data.ItemDataDict[code].iconpath);
            RewardCode.Add(code);
        }
        Get<GameObject>((int)GameObjects.Star1).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Star2).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Star3).gameObject.SetActive(false);

        Get<GameObject>((int)GameObjects.WinCase).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.FailedCase).gameObject.SetActive(false);
        GetButton((int)Buttons.Exit_Button).gameObject.BindEvent((PointerEventData data) =>
        Managers.Resource.Destroy(gameObject));





        GetText((int)Texts.BattleStart_Text).text = Managers.Data.StageDataDict[Managers.Stage.StageCode].stageName;

        GetButton((int)Buttons.Fast_Button).gameObject.BindEvent((PointerEventData data) =>
       TimesclaeConvert(2.0f));
        GetButton((int)Buttons.FastFast_Button).gameObject.BindEvent((PointerEventData data) =>
        TimesclaeConvert(5.0f));
        GetButton((int)Buttons.Pause_Button).gameObject.BindEvent((PointerEventData data) =>
        TimesclaeConvert(0));
        GetButton((int)Buttons.Exit_Button).gameObject.SetActive(false);

    }
    private void LateUpdate()
    {
        Refresh_SliderBar();

    }
    private void Refresh_SliderBar()
    {
        _currcost += Time.deltaTime;
        _currcost = Mathf.Clamp(_currcost, 0, MaxCost);
        CostBar.value = CurrCost / MaxCost;
        GetText((int)Sliders.Cost_Slider).text = $"{(int)_currcost}";
    }


    public void Set_Character()
    {

    }
    public void GameEnd()
    {
        float endTime = Time.time;
        if (Managers.Stage.IsGameClear.Equals((int)Define.IsGameClear.Clear))
        {
            Get<GameObject>((int)GameObjects.WinCase).gameObject.SetActive(true);
            Debug.Log($"Realtime : {Realtime}");
            Debug.Log($"Endtime : {endTime}");
            if ((endTime - Realtime).CompareTo(60) < 0)
            {
                Managers.Stage.ClearDataRenew(3);
                Get<GameObject>((int)GameObjects.Star3).gameObject.SetActive(true);
                Get<GameObject>((int)GameObjects.Star2).gameObject.SetActive(true);
                Get<GameObject>((int)GameObjects.Star1).gameObject.SetActive(true);
            }
            else if ((endTime - Realtime).CompareTo(90) < 0)
            {
                Managers.Stage.ClearDataRenew(2);
                Get<GameObject>((int)GameObjects.Star2).gameObject.SetActive(true);
                Get<GameObject>((int)GameObjects.Star1).gameObject.SetActive(true);
            }
            else
            {
                Managers.Stage.ClearDataRenew(1);
                Get<GameObject>((int)GameObjects.Star1).gameObject.SetActive(true);
            }
            foreach (int i in RewardCode)
            {
                Managers.ItemInventory.Add(i);
            }
            Managers.Stage.StageClear_UI?.Invoke();
            Managers.Sound.Play("104_win");
        }
        else
        {
            Get<GameObject>((int)GameObjects.FailedCase).gameObject.SetActive(true);
            Managers.Sound.Play("104_lose");
        }
        GetButton((int)Buttons.Exit_Button).gameObject.SetActive(true);
        Time.timeScale = 1;
        foreach (int i in Managers.Stage.FightCharacter)
        {
            if (!i.Equals(-1))
            {
                Managers.Stage.GetExp();
            }
        }
        Managers.Sound.StopEffect();
        Managers.Sound.UnPause_Bgm();
    }
    private void OnDisable()
    {
        Managers.Stage.StageClear_UI_Concept2 -= GameEnd;
        Managers.Stage.IsGameClear = (int)Define.IsGameClear.None;
    }
    private void TimesclaeConvert(float n)
    {
        if (MyTimeSclae.Equals(1))
        {
            Time.timeScale = n;
            MyTimeSclae = n;
        }
        else
        {
            Time.timeScale = 1;
            MyTimeSclae = 1;
        }
    }
}
