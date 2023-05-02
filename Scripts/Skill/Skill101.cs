using UnityEngine;

public class Skill101 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Enemy_CharacterController go = collision.GetComponent<Enemy_CharacterController>();
            if (!go.isDie)
            {

                Debug.Log($"{collision.name} Hitted By SKill101");
                go.Enemy_Hp -= Managers.Data.SkillDataDict[101].skillDamage;


            }
            Managers.Resource.Destroy(gameObject, 2.0f);
        }
    }
}
