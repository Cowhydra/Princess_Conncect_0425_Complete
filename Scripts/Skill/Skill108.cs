using UnityEngine;

public class Skill108 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
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
                    Debug.Log($"{collision.name} Hitted By SKill 108");
                    go.Enemy_Hp -= Managers.Data.SkillDataDict[108].skillDamage;
                    go.CreatureState = Define.CreatureState.HIT;
                    go.transform.position = gameObject.transform.position;
                    go.Hit(1.0f);
                    Managers.Resource.Destroy(gameObject);
                }
            }
        }
    }
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2Int(250, 0);


    }
}
