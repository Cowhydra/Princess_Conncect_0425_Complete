using UnityEngine;

public class Skill100 : MonoBehaviour
{
    private Vector2 MoveDir;
    Rigidbody2D rigid;
    private float _addForce = 30.0f;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Init();
    }

    private void Init()
    {
        int x = Random.Range(100, 201);
        int y = Random.Range(300, 401);
        MoveDir = new Vector2(x, y).normalized;
        rigid.AddForce(_addForce * MoveDir);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Vector3 incomingVector = MoveDir;
            incomingVector = incomingVector.normalized;
            // 충돌한 면의 법선 벡터를 구해낸다.
            Vector3 normalVector = collision.contacts[0].normal;
            // 법선 벡터와 입사벡터을 이용하여 반사벡터를 알아낸다.
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각
            reflectVector = reflectVector.normalized;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(_addForce * reflectVector);
            MoveDir = reflectVector;


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy_CharacterController go = collision.GetComponent<Enemy_CharacterController>();
            if (!go.isDie)
            {
                if (go.NotHit)
                {
                }
                else
                {
                    Debug.Log($"{collision.name} Hitted By SKill100");
                    go.Enemy_Hp -= Managers.Data.SkillDataDict[100].skillDamage;
                    go.CreatureState = Define.CreatureState.HIT;
                    go.Hit(1.0f);
                }
            }
            Managers.Resource.Destroy(gameObject, 5.0f);
        }
    }

}
