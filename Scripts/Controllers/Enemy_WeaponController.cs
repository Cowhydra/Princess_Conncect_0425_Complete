using System.Collections;
using UnityEngine;

public class Enemy_WeaponController : MonoBehaviour
{

    Enemy_CharacterController Enemy_Controller;
    private bool isFire = false;

    private void Start()
    {
        Enemy_Controller = gameObject.transform.parent.GetComponent<Enemy_CharacterController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //적 무기가 플레이어와 충돌한 경우 공격
        //이전에 살펴본 CreatureWeaponController랑 차이가 없습니다.
        if (collision.CompareTag("Character"))
        {
            Enemy_Controller.CreatureState = Define.CreatureState.ATK;
            Vector3 MoveDir = (collision.transform.position - gameObject.transform.position).normalized;
            if (!isFire)
            {
                StartCoroutine(nameof(Start_Attack_CO), MoveDir);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Character"))
        {
            Debug.Log("Exited Excute! MYcHARACTER2222");
            StopCoroutine(nameof(Start_Attack_CO));
            isFire = false;
            Enemy_Controller.CreatureState = Define.CreatureState.RUN;
            Debug.Log($"{gameObject.name} Attack -> {collision.name}");
            if (Enemy_Controller.isDie)
            {
                Enemy_Controller.CreatureState = Define.CreatureState.DIE;
            }

        }

    }
    private IEnumerator Start_Attack_CO(Vector3 MoveDir)
    {
        isFire = true;
        while (Enemy_Controller.CreatureState.Equals(Define.CreatureState.ATK))
        {


            GameObject go = Managers.Resource.Instantiate($"UI/Projectile/Projectile{Enemy_Controller.gameObject.name}");
            go.GetComponent<Projectile>().isCharacterProjectfile = false;
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


