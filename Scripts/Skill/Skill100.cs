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
            // �浹�� ���� ���� ���͸� ���س���.
            Vector3 normalVector = collision.contacts[0].normal;
            // ���� ���Ϳ� �Ի纤���� �̿��Ͽ� �ݻ纤�͸� �˾Ƴ���.
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //�ݻ簢
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
