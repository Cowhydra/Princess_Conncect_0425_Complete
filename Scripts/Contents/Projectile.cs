
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{

    //게임 내 전투의 과정은 모두 프로젝 타일을 발사하고, 맞는 행위로 부터 이루어집니다.

    [SerializeField]
    private float _moveSpeed = 50.0f;

    //이동방향, 캐릭터가 쏜 발사체인지, 쏜 발사체의 characterid
    //처음 기획할 때 몬스터, 플레이어 리로스 및 프리팹을 따로 관리하지 않았사오며,
    //적도 플레이어와 동일한 외형을 가지고 같은 행위를 취합니다.
    //적인지 플레이어인지 구분하여 처리하기위해,isCharacterProjectfile 를 선언했습니다.
    public Vector3 moveDirection = Vector3.zero;
    public bool isCharacterProjectfile = true;
    private int characterid = -1;


    private void Start()
    {
        characterid = int.Parse(gameObject.name.Substring(gameObject.name.Length - 3));
        //프로젝타일의 이름을 Projectile[Characterid]로 짓는 규칙을 설정하여
        //해당 캐릭터 id를 구해줍니다.
        //또한, 캐릭터가 Warrior타입일 경우 근거리 공격을 진행하므로, 유저에게 프로젝타일을 보이면 안되기에
        //프로젝타일의 A값을 수정해 투명처리를 진행합니다.
        if (Managers.Data.CharacterDataDict[characterid].jobType
            .Equals("Warrior"))
        {
            Color color = gameObject.GetComponent<Image>().color;
            color.a = 0;
            gameObject.GetComponent<Image>().color = color;
            _moveSpeed = 20.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //발사체 충돌처리입니다.
        //플레이어,에너미는 생성할 당시에 태그 설정 및 관련 컨트롤러 컴포넌트를 붙여줍니다.
        //그 과정에서 내가쏘고, 맞은 사람이 적이면 적의 Controller를 가져와서
        //죽지 않았으면, 지금 맞지 않는 상태가 아니라면 => 체력을 가해지고, 상태패턴으로 Creatrue상태를 HIT로 변경 후
        // Hit() <== 맞음처리 함수를 실행해줍니다.
        if (isCharacterProjectfile && collision.CompareTag("Enemy"))
        {
            Enemy_CharacterController go = collision.GetComponent<Enemy_CharacterController>();
            if (!go.isDie)
            {
                if (go.NotHit)
                {

                }
                else
                {
                    go.Enemy_Hp -= 2 * gameObject.GetComponentInParent<My_CharacterController>().Char_Attack;
                    go.CreatureState = Define.CreatureState.HIT;
                    //Managers.Resource.Destroy(gameObject);
                    go.Hit(2.0f);
                }



            }

        }
        else if (!isCharacterProjectfile && collision.CompareTag("Character"))
        {
            My_CharacterController go = collision.GetComponent<My_CharacterController>();
            if (!go.isDie)
            {

                if (go.NotHit)
                {
                }
                else
                {
                    Managers.Resource.Destroy(gameObject);

                    //go.CreatureState = Define.CreatureState.HIT;
                    go.Char_Hp -= gameObject.GetComponentInParent<Enemy_CharacterController>().Enemy_Attack;
                    go.Hit(2.0f);
                }



            }

        }
    }
    //프로젝타일의 이동방향을 설정해줍니다.
    void Update()
    {
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void MoveTo(Vector3 direction, float MoveSpeed)
    {
        moveDirection = direction;
        _moveSpeed = MoveSpeed;
    }



}

