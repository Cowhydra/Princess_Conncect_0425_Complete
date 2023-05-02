using UnityEngine;
using UnityEngine.UI;

public class Concept2_Character_MouseEvent : MonoBehaviour
{
    public int CharCode = -1;
    Color PrevColor;
    private void Start()
    {
        PrevColor = GetComponent<Image>().color;
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().size = new Vector2(20, 20);
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.GetOrAddComponent<Canvas>().overrideSorting = true;
        gameObject.GetOrAddComponent<Canvas>().sortingOrder = 6000;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Position"))
        {

            Position_Button_UI_Concept2 PositionUI = collision.GetComponent<Position_Button_UI_Concept2>();
            PositionUI.Change_Color();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Position"))
        {
            Position_Button_UI_Concept2 PositionUI = collision.GetComponent<Position_Button_UI_Concept2>();
            PositionUI.Charnge_Color_Origin();

        }
    }
    public void Setting(int charcode)
    {
        Color color = GetComponent<Image>().color;
        color.a = 0.8f;
        GetComponent<Image>().color = color;
        CharCode = charcode;

    }
}
