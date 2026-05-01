using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    
    public Image startButton;
    public Image loadButton;
    public Image quitButton;

    private void Start()
    {
        startButton.alphaHitTestMinimumThreshold = 0.1f;
        loadButton.alphaHitTestMinimumThreshold = 0.1f;
        quitButton.alphaHitTestMinimumThreshold = 0.1f;
    }


    public void Play()
    {
        SceneManager.LoadScene(gameSceneName); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
