using System.Collections;
using UnityEngine;

public class Skill104 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            My_CharacterController go = collision.GetComponent<My_CharacterController>();
            StartCoroutine(nameof(Heal_MP), go);
        }
    }

    IEnumerator Heal_MP(My_CharacterController go)
    {
        while (true)
        {
            if (!go.isDie)
            {
                go.Char_Mp += Managers.Data.SkillDataDict[104].skillDamage;
            }
            yield return new WaitForSeconds(3);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Heal_MP));
    }


}


