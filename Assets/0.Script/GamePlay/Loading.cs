using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private static string scene;
    [SerializeField] private float Timer;
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    
    public static void Load(string Scene)
    {
        scene = Scene;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        Debug.Log(scene);
        StartCoroutine(Loading_Start());
    }

    IEnumerator Loading_Start()
    {
        Time.timeScale = 1;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        bool is_timer = false;
        float t = 0;
        float n = 0;
        if(Timer != 0) is_timer = true;
        while(!asyncOperation.isDone)
        {
            yield return null;
            t += Time.deltaTime;
            n += Time.deltaTime;
            text.text = "Loading";

            if (n > 4) n = 0;
            
            for(int i = 0; i < (int)n; i++)
            {
                text.text += ".";
            }



            if(is_timer)
            {
                if(t < Timer)
                {
                    image.fillAmount = Mathf.Lerp((t / Timer) - 0.1f, 0.9f , Time.deltaTime);
                }
                else
                {
                    if (asyncOperation.progress >= 0.9f)
                    {
                        is_timer = false;
                    }
                }
            }
            else
            {

                if(asyncOperation.progress < 0.9f)
                {
                    image.fillAmount = Mathf.Lerp(asyncOperation.progress, 0.9f, Time.deltaTime);
                }
                else
                {
                    image.fillAmount = Mathf.Lerp(image.fillAmount, 1f, Time.deltaTime);
                    if(image.fillAmount >= 0.99f)
                    {
                        asyncOperation.allowSceneActivation = true;
                        GameManager.Instance.is_Game = true;
                    }
                }
            }
        }
    }
}
