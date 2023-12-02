using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingControl : MonoBehaviour
{
    public Slider progressbar;
    public TextMeshProUGUI loadtext;

    private AsyncOperation operation;

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        if(SceneManagerEX.Instance.beforeSceneName == "MainMenu")
        {
            operation = SceneManager.LoadSceneAsync("TutorialMap");
        }
        else if (SceneManagerEX.Instance.beforeSceneName == "CharacterSelect")
        {
            operation = SceneManager.LoadSceneAsync("Stage1");
        }
        else if (SceneManagerEX.Instance.beforeSceneName == "Stage1")
        {
            operation = SceneManager.LoadSceneAsync("Stage2");
        }

            operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            Debug.Log(operation.allowSceneActivation);
            Debug.Log(progressbar.value);
            yield return null;
            if (progressbar.value < 0.9f)
            {
                Debug.Log("progressbar.value is less than 0.9");
                Debug.Log("Time.deltaTime: " + Time.deltaTime / 2);
                Debug.Log("Time.Timer" + Time.time);
                progressbar.value += Time.deltaTime / 2;
            }
            if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1.0f, Time.deltaTime / 2);
            }
            if (progressbar.value >= 1.0f)
            {
                loadtext.text = "Touch Any Area";
            }
            if ((Input.touchCount>0 || Input.GetKeyDown(KeyCode.Space)) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                StopAllCoroutines();
            }
        }


    }

}
