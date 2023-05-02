using UnityEngine;

public class Skill111 : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.localPosition
            = 90 * Vector2.right + 50 * Vector2.up;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Enemy_CharacterController go = collision.GetComponent<Enemy_CharacterController>();
            if (!go.isDie)
            {

                Debug.Log($"{collision.name} Hitted By SKill101");
                go.Enemy_Hp -= Managers.Data.SkillDataDict[111].skillDamage;
                GetComponentInParent<My_CharacterController>().Char_Hp += Managers.Data.SkillDataDict[111].skillDamage;

            }
            Managers.Resource.Destroy(gameObject, 5.0f);
        }
    }
}
