using System.Collections;
using UnityEngine;

public class Skill105 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            My_CharacterController go = collision.GetComponent<My_CharacterController>();
            StartCoroutine(nameof(Add_ATK), go);
        }
    }

    IEnumerator Add_ATK(My_CharacterController go)
    {
        while (true)
        {
            if (!go.isDie)
            {
                go.Char_Attack += Managers.Data.SkillDataDict[104].skillDamage;
            }
            yield return new WaitForSeconds(3);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Add_ATK));
    }
}
