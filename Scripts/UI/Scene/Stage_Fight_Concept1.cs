using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage_Fight_Concept1 : UI_Scene
{
    // Start is called before the first frame update
    float Realtime;
    public Vector3[] EndPos = new Vector3[4]; //실패
    private float MyTimeSclae = 1.0f;
    private List<int> RewardCode = new List<int>();
    void Start()
    {
        Init();
        Realtime = Time.time;
    }

    enum GameObjects
    {
        Character_Start_Position,
        Enemy_Start_Position,
        BattleStart,
        Fight_Character_Interface,
        Character_End_Position,
        WinCase,
        FailedCase,
        Star1,
        Star2,
        Star3,

    }
    enum Texts
    {
        BattleStart_Text,

    }
    enum Buttons
    {
        Fast_Button,
        FastFast_Button,
        Pause_Button,
        Exit_Button,
    }
    enum Images
    {
        Reward1,
        Reward2,

    }

    public override void Init()
    {
        #region 기초
        base.Init();
        Managers.Sound.Pause_Bgm();
        Managers.Sound.Play("BGM5");

        GetComponent<Canvas>().sortingOrder = 3000;
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        #endregion
        #region 스테이지 초기화
        Managers.Stage.CharacterCount = Managers.Stage.GetCharacterCount();
        Managers.Stage.MonsterCount = Managers.Stage.GetMonsterCount();

        GetText((int)Texts.BattleStart_Text).text = Managers.Data.StageDataDict[Managers.Stage.StageCode].stageName;
        foreach (Transform transforom in Get<GameObject>((int)GameObjects.Fight_Character_Interface).transform.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        for (int i = 0; i < Managers.Stage.FightCharacter.Count; i++)
        {
            if (Managers.Stage.FightCharacter[i].Equals(-1)) continue;
            Transform transform = Get<GameObject>((int)GameObjects.Character_Start_Position).transform.GetChild(i);

            GameObject go = Managers.Resource.Instantiate($"UI/Character/{Managers.Stage.FightCharacter[i]}");

            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            My_CharacterController go_Controller = go.AddComponent<My_CharacterController>();
            go_Controller.CharCode = Managers.Stage.FightCharacter[i];
            go.tag = "Character";
            go.transform.GetChild(0).gameObject.AddComponent<Creature_WeaponController>();
            Fight_Character_Bottom_UI _botUI = Managers.UI.ShowSceneUI<Fight_Character_Bottom_UI>();

            _botUI.transform.SetParent(Get<GameObject>((int)GameObjects.Fight_Character_Interface).transform);
            _botUI.CharCode = Managers.Stage.FightCharacter[i];
            _botUI._myCharacter = go_Controller;

        }
        for (int i = 0; i < Managers.Stage.FightMonster.Count; i++)
        {
            if (Managers.Stage.FightMonster[i].Equals(-1)) continue;
            Transform transform = Get<GameObject>((int)GameObjects.Enemy_Start_Position).transform.GetChild(i);

            GameObject go = Managers.Resource.Instantiate($"UI/Character/{Managers.Stage.FightMonster[i]}");
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            Enemy_CharacterController go_EnemyCOn = go.AddComponent<Enemy_CharacterController>();
            go_EnemyCOn.CharCode = Managers.Stage.FightCharacter[i];
            go.tag = "Enemy";
            go.transform.GetChild(0).gameObject.AddComponent<Enemy_WeaponController>();
        }

        #endregion

        #region 게임 종료후 결과창 

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


        GetButton((int)Buttons.Fast_Button).gameObject.BindEvent((PointerEventData data) =>
        TimesclaeConvert(2.0f));
        GetButton((int)Buttons.FastFast_Button).gameObject.BindEvent((PointerEventData data) =>
        TimesclaeConvert(5.0f));
        GetButton((int)Buttons.Pause_Button).gameObject.BindEvent((PointerEventData data) =>
        TimesclaeConvert(0));
        GetButton((int)Buttons.Exit_Button).gameObject.SetActive(false);
        #endregion
    }

    public void GameEnd_Infor()
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
        Managers.Stage.IsGameClear = (int)Define.IsGameClear.None;
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
