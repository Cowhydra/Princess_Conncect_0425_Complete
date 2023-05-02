using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Title : UI_Scene
{
    public float duration = 1.0f; // 알파값 변화 시간 간격

    private bool isIncreasing = true;
    private void Start()
    {
        Init();

    }
    enum GameObjects
    {
        BackGround,
        GameTitle

    }
    enum Buttons
    {
        GameStart,
        BGM_Container
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.GameStart).gameObject.BindEvent((PointerEventData data) =>
        {
            Debug.Log("GameStart_Button Clicked");
            Managers.Scene.LoadScene(Define.Scene.Game);
        });

        Managers.Sound.Play("First_Princess");
        GetButton((int)Buttons.BGM_Container).gameObject.BindEvent((PointerEventData data) => { BGMSTart(); });
        StartCoroutine(nameof(ImagesA_Change), GetButton((int)Buttons.GameStart).GetComponent<Image>());
        StartCoroutine(nameof(ImagesA_Change), Get<GameObject>((int)GameObjects.GameTitle).GetComponent<Image>());
    }
    private void BGMSTart()
    {
        Managers.Sound.Play("Opening_BGM");
        Managers.Resource.Destroy(GetButton((int)Buttons.BGM_Container).gameObject);
        GetComponentInChildren<VideoPlayer>().Play();
    }


    private IEnumerator ImagesA_Change(Image image)
    {
        float t = 0.0f;

        while (true)
        {
            float alpha = image.color.a;

            if (isIncreasing)
            {
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float blend = Mathf.Clamp01(t / duration);
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, blend));
                    yield return null;
                }

                isIncreasing = false;
                t = 0.0f;
            }
            else
            {
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float blend = Mathf.Clamp01(t / duration);
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, blend));
                    yield return null;
                }

                isIncreasing = true;
                t = 0.0f;
            }

            yield return null;
        }
    }

}


