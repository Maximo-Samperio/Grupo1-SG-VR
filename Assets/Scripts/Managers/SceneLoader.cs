using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void LoadGamePlayScene() => SceneManager.LoadScene("GameplayMap");

    public void LoadMainMenu() => SceneManager.LoadScene("Main Menu");

    public void LoadScoreboard() => SceneManager.LoadScene("Scoreboard");

    public void ExitGame() => Application.Quit();
}
