using UnityEngine;

public class Skill106 : MonoBehaviour
{
    private int attack;
    My_CharacterController go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            go = collision.GetComponent<My_CharacterController>();
            if (!go.isDie)
            {

                Debug.Log($"{collision.name} Hitted By SKill103");
                attack = go.Char_Attack;
                go.Char_Attack += Managers.Data.SkillDataDict[106].skillDamage;
            }
            Managers.Resource.Destroy(gameObject, 10.0f);
        }
    }
    private void OnDisable()
    {
        go.Char_Attack = attack;
    }
}
