using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenuControl : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingSliderBar;

    private void Start()
    {
        loadingSliderBar.value = 0;
    }
    public void oyunaBasla()
    {
       
        StartCoroutine(sahneYukleme());
    }

    IEnumerator sahneYukleme()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float ilerleme = Mathf.Clamp01(operation.progress / .9f);
            loadingSliderBar.value = ilerleme;

            // sekronize iþlemlerde ve while döngülerinde çok daha kullanýþlý
            yield return null;
        }


    }

    public void oyundanCik()
    {
        Application.Quit();
    }
}
