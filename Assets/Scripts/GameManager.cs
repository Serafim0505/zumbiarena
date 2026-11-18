using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject introPanel;
    public GameObject creditosPanel;
    public GameObject configPanel;

    void Start()
    {
        isGameOver = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (creditosPanel != null) creditosPanel.SetActive(false);
        if (configPanel != null) configPanel.SetActive(false);

        // pro pop up de introdução do game
        if (introPanel != null)
        {
            introPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        // tecla p e esc para abrir configg
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (configPanel != null && configPanel.activeSelf)
            {
                FecharConfig();
            }
            else
            {
                AbrirConfig();
            }
        }
    }

    public void IniciarPartida()
    {
        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowVictory()
    {
        isGameOver = true;

        // pra desativar texto da vitoria quando boneco cruzar a porta
        ZombieSpawner spawner = FindAnyObjectByType<ZombieSpawner>();
        if (spawner != null && spawner.textoAviso != null)
        {
            spawner.textoAviso.gameObject.SetActive(false);
        }

        if (victoryPanel != null) victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene("Menu");
    }

    public void AbrirCreditos()
    {
        if (creditosPanel != null) creditosPanel.SetActive(true);
    }

    public void FecharCreditos()
    {
        if (creditosPanel != null) creditosPanel.SetActive(false);
    }

    public void AbrirConfig()
    {
        if (isGameOver) return;
        if (configPanel != null) configPanel.SetActive(true);
        Time.timeScale = 0f; // pra parar o game apertando botao de pause
    }

    public void FecharConfig()
    {
        if (configPanel != null) configPanel.SetActive(false);
        if (!isGameOver) Time.timeScale = 1f; // despausa e volta de onde parou
    }
}