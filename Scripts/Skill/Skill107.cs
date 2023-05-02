using System.Collections;
using UnityEngine;

public class Skill107 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            My_CharacterController go = collision.GetComponent<My_CharacterController>();
            StartCoroutine(nameof(Add_HpRe), go);
        }
    }

    IEnumerator Add_HpRe(My_CharacterController go)
    {
        while (true)
        {
            if (!go.isDie)
            {
                go.Char_Hp += Managers.Data.SkillDataDict[107].skillDamage;
            }
            yield return new WaitForSeconds(3);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Add_HpRe));
    }
}
