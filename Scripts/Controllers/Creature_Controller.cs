using System.Collections;
using UnityEngine;


public class Creature_Controller : MonoBehaviour
{

    //공통된 컨트롤러, 적과 내 캐릭터가 공통적으로 갇는 속성을 정의해줍니다.
    [SerializeField]
    protected GameObject[] _enemyTransform;
    protected Rigidbody2D rigidbody2D;
    public Animator animator;
    public Define.CreatureState CreatureState = Define.CreatureState.RUN;
    public int CharCode = -1;
    protected int movedir = 1;
    public bool isDie = false;
    public int CharacterCount = 0;
    public int MonsterCount = 0;

    #region Stat
    protected int _maxhp;
    [SerializeField]
    protected int _hp = 10;
    [SerializeField]
    protected int _mp;
    protected int _maxmp;
    protected int _def;
    [SerializeField]
    protected int _attack;
    public bool NotHit;
    #endregion

    //게임이 2개의 컨셉으로 나누어 진행되며, 이 컨트롤러는 첫번째 전투 형식에서만 사용됩니다.
    protected Stage_Fight_Concept1 Stage_Concept1;

    private void Start()
    {
        Init();
        CharacterCount = Managers.Stage.GetCharacterCount();
        MonsterCount = Managers.Stage.GetMonsterCount();
        //내 캐릭터의 수와, 몬스터의 수를 초기화 진행해줍니다.
        //스테이지도 하나만 입장할 수 있으므로, 싱글톤을 이용해 중앙에서 관리해줍니다.

    }
    public virtual void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Stage_Concept1 = GetComponentInParent<Stage_Fight_Concept1>();
    }

    private void LateUpdate()
    {

        //상태패턴입니다.
        //일부 Hit, Die , SKIll속성의 경우 해당 상태가 되면
        //한번만 실행되기에, 상태패턴이 아닌 별도로 처리해주었습니다.
        //달리기, 멈추기, 공격 등은 velocity를 이용해 구현해주었으며,
        //animator의 경우 setBool을 이용해 애니메이션이 재생될 수 있도록 설정해줍니다.
        switch (CreatureState)
        {
            case Define.CreatureState.IDLE:
                rigidbody2D.velocity = Vector2.zero;
                animator.SetTrigger("IDLE");

                break;
            case Define.CreatureState.ATK:
                rigidbody2D.velocity = Vector2.zero;
                animator.SetBool("ATK", true);
                break;
            case Define.CreatureState.DIE:
                //한번만 실행될 것이기에 따로 프로퍼티에 구현
                rigidbody2D.velocity = Vector2.zero;
                break;
            case Define.CreatureState.HIT:
                //위와 동일...
                break;
            case Define.CreatureState.SKILL:
                break;
            case Define.CreatureState.RUN:
                rigidbody2D.velocity = movedir * new Vector2Int(200, 0);
                animator.SetBool("RUN", true);
                animator.SetBool("ATK", false);
                break;
        }
    }
    public void Set_EnemyState()
    {
        //아군 에겐 몬스터, 몬스터에겐 적 의 상태를 조정해줍니다.
        // 전투가 끝난 후, 남아있는 몬스터 및 아군 캐릭터들을 IDLE한 상태로 변경해줍니다.
        for (int i = 0; i < _enemyTransform.Length; i++)
        {
            if (_enemyTransform[i] != null)
            {
                _enemyTransform[i].GetComponent<Creature_Controller>().CreatureState = Define.CreatureState.IDLE;
            }
        }

    }
    public virtual void Die()
    {
        while (!isDie)
        {
            StartCoroutine(nameof(Die_Co));
        }
    }
    IEnumerator Die_Co()
    {
        //죽음과 관련된 처리 입니다.
        //죽으면 속성 IsdDIe를 True로 변경해주며, 상태 변경 애니메이션 실행
        // -> 충돌이 일어나 다른 캐릭터가 받을 데미지를 시체가 받는 현상을 제거하기 위해, COllider를 비활성
        isDie = true;
        CreatureState = Define.CreatureState.DIE;
        animator.SetTrigger("DIE");
        Debug.Log("SetTrigger DIE");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Managers.Resource.Destroy(gameObject.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.localPosition = new Vector3(1000, 1000, 1000);
        Managers.Resource.Destroy(gameObject, 0.1f);
        yield break;
    }
    IEnumerator Player_Hit_AnimCo(float time)
    {//정해진 time 시간동안 HIt상태로 맞지않는 무적상태 입니다.
        NotHit = true;
        animator.SetTrigger("HIT");
        yield return new WaitForSeconds(time);
        NotHit = false;
    }
    public virtual void Hit(float time)
    {
        StartCoroutine(nameof(Player_Hit_AnimCo), time);
    }
    public void GameEnd()
    {
        //게임이 끝날 경우 행할 처리입니다.
        //게임 오버의 경우 몬스터, 혹은 캐릭터의 숫자가0이 되면 실행되며,
        //몬스터가 이겼을 때, 플레이어가 이겼을떄 의 분기는 StageScript에서 진행합니다.
        Stage_Concept1.GameEnd_Infor();
    }


}
