using System.Collections;
using UnityEngine;

public class Creature_WeaponController : MonoBehaviour
{
    My_CharacterController CharacterController;
    public int CharacterCode;
    private bool isFire = false;
    private void Start()
    {
        CharacterController = gameObject.transform.parent.GetComponent<My_CharacterController>();
        CharacterCode = int.Parse(gameObject.name.Substring(0, 3));


    }
    private void Update()
    {
        // 왜인지 모르겠으나 빙글빙글 돌아서 COllider 가 망가져 추가한 코드
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Projectile을 발사하는 조건
        //무기가 적과 충돌했을때, 적 방향으로 불체를 발사하고, Atttck을 처리해줍니다.
        if (collision.CompareTag("Enemy"))
        {
            CharacterController.CreatureState = Define.CreatureState.ATK;
            Enemy_CharacterController EnemyStat = collision.GetComponent<Enemy_CharacterController>();
            Vector3 MoveDir = (collision.transform.position - gameObject.transform.position).normalized;
            if (!isFire)
            {
                StartCoroutine(nameof(Start_Attack_CO), MoveDir);
                //시선처리 
                gameObject.transform.LookAt(EnemyStat.transform);
                Debug.Log($"{gameObject.name} Attack -> {collision.name}");
            }

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //무기가 캐릭터와 충돌이 끝난다면
        //공격을 멈추고, 달리기 상태로 바꾸어, 적을  찾도록 실행합니다.
        //만약 내가 죽어서 끝났을 경우 => 상태를 Die로 변경해줍니다.
        if (collision.tag.Equals("Enemy"))
        {
            StopCoroutine(nameof(Start_Attack_CO));
            isFire = false;
            Debug.Log("Exited Excute! MYcHARACTER");
            CharacterController.CreatureState = Define.CreatureState.RUN;
            if (CharacterController.isDie)
            {
                CharacterController.CreatureState = Define.CreatureState.DIE;
            }
        }


    }


    private IEnumerator Start_Attack_CO(Vector3 MoveDir)
    {
        isFire = true;
        //발사! -> 내가 발사했으니, isCharacterProjectfile를 true로 설정해주고,
        //MOVeDIr을 인수로 받아 해당 방향으로 발사체를 움직입니다.
        // 무기의 자식으로 설정하고, lccalpOsition을 0으로 설정해 무기의 중심(piviot)으로 부터
        //발사체가 나갈 수 있도록 설정합니다.
        while (CharacterController.CreatureState.Equals(Define.CreatureState.ATK))
        {

            GameObject go = Managers.Resource.Instantiate($"UI/Projectile/Projectile{CharacterController.gameObject.name}");
            go.GetComponent<Projectile>().isCharacterProjectfile = true;
            go.GetComponent<Projectile>().MoveTo(MoveDir, 500.0f);
            go.transform.SetParent(gameObject.transform.parent);
            go.transform.localPosition = Vector3.zero;

            Destroy(go, 5f);
            yield return new WaitForSeconds(2.1f);
        }



    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Start_Attack_CO));
    }
}

