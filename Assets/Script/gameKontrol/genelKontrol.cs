using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class genelKontrol : MonoBehaviour
{
    public AudioSource oyuniciSes;
    public GameObject pauseCanvas;
    public static bool oyunDurdumu;


    void Start()
    {
        oyuniciSes = GetComponent<AudioSource>();
        oyuniciSes.Play();

        oyunDurdumu = false;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !genelKontrol.oyunDurdumu)
        {
            pause();
        }
    }

    public void bastanBasla()
    {
        // aktif sahneyi yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // mermilerin tekrar set edilmesi için ilgili anahtarý temizle
        PlayerPrefs.DeleteKey("yeniOyunBaslangici");

        // oyunu oynatýyoruz
        Time.timeScale = 1;

        oyunDurdumu = false;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
    }

    public void pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        oyunDurdumu = true;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
    }

    public void anaMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void devamEt()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        oyunDurdumu = false;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindWithTag("karakter").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
    }

}
