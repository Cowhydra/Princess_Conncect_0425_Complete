using UnityEngine;

public class Skill109 : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2Int(250, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Enemy_CharacterController go = collision.GetComponent<Enemy_CharacterController>();
            if (!go.isDie)
            {

                Debug.Log($"{collision.name} Hitted By SKill 109");
                go.Enemy_Hp -= Managers.Data.SkillDataDict[101].skillDamage;
                GetComponentInParent<My_CharacterController>().Char_Hp += Managers.Data.SkillDataDict[101].skillDamage;

            }
            Managers.Resource.Destroy(gameObject, 2.0f);
        }
    }
}
