using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditosPanel;

    void Start()
    {
        if (creditosPanel != null) creditosPanel.SetActive(false);
    }

    public void AbrirCreditos()
    {
        if (creditosPanel != null) creditosPanel.SetActive(true);
    }

    public void FecharCreditos()
    {
        if (creditosPanel != null) creditosPanel.SetActive(false);
    }

    public void PlayFun()
    {
        PlayerPrefs.SetString("modo", "Fun");
        SceneManager.LoadScene("Arena");
    }

    public void PlayDesafio()
    {
        PlayerPrefs.SetString("modo", "Desafio");
        SceneManager.LoadScene("Desafio");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}