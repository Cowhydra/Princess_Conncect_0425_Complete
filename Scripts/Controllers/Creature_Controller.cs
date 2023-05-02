using System.Collections;
using UnityEngine;


public class Creature_Controller : MonoBehaviour
{

    //����� ��Ʈ�ѷ�, ���� �� ĳ���Ͱ� ���������� ���� �Ӽ��� �������ݴϴ�.
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

    //������ 2���� �������� ������ ����Ǹ�, �� ��Ʈ�ѷ��� ù��° ���� ���Ŀ����� ���˴ϴ�.
    protected Stage_Fight_Concept1 Stage_Concept1;

    private void Start()
    {
        Init();
        CharacterCount = Managers.Stage.GetCharacterCount();
        MonsterCount = Managers.Stage.GetMonsterCount();
        //�� ĳ������ ����, ������ ���� �ʱ�ȭ �������ݴϴ�.
        //���������� �ϳ��� ������ �� �����Ƿ�, �̱����� �̿��� �߾ӿ��� �������ݴϴ�.

    }
    public virtual void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Stage_Concept1 = GetComponentInParent<Stage_Fight_Concept1>();
    }

    private void LateUpdate()
    {

        //���������Դϴ�.
        //�Ϻ� Hit, Die , SKIll�Ӽ��� ��� �ش� ���°� �Ǹ�
        //�ѹ��� ����Ǳ⿡, ���������� �ƴ� ������ ó�����־����ϴ�.
        //�޸���, ���߱�, ���� ���� velocity�� �̿��� �������־�����,
        //animator�� ��� setBool�� �̿��� �ִϸ��̼��� ����� �� �ֵ��� �������ݴϴ�.
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
                //�ѹ��� ����� ���̱⿡ ���� ������Ƽ�� ����
                rigidbody2D.velocity = Vector2.zero;
                break;
            case Define.CreatureState.HIT:
                //���� ����...
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
        //�Ʊ� ���� ����, ���Ϳ��� �� �� ���¸� �������ݴϴ�.
        // ������ ���� ��, �����ִ� ���� �� �Ʊ� ĳ���͵��� IDLE�� ���·� �������ݴϴ�.
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
        //������ ���õ� ó�� �Դϴ�.
        //������ �Ӽ� IsdDIe�� True�� �������ָ�, ���� ���� �ִϸ��̼� ����
        // -> �浹�� �Ͼ �ٸ� ĳ���Ͱ� ���� �������� ��ü�� �޴� ������ �����ϱ� ����, COllider�� ��Ȱ��
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
    {//������ time �ð����� HIt���·� �����ʴ� �������� �Դϴ�.
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
        //������ ���� ��� ���� ó���Դϴ�.
        //���� ������ ��� ����, Ȥ�� ĳ������ ���ڰ�0�� �Ǹ� ����Ǹ�,
        //���Ͱ� �̰��� ��, �÷��̾ �̰����� �� �б�� StageScript���� �����մϴ�.
        Stage_Concept1.GameEnd_Infor();
    }


}
