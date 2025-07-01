using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject deathScreenPanel;
    public Text deathText;
    public Button restartButton;
    
    [Header("Settings")]
    public float fadeInTime = 1f;
    
    private CanvasGroup canvasGroup;
    
    void Start()
    {
        canvasGroup = deathScreenPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = deathScreenPanel.AddComponent<CanvasGroup>();
        }
        
        HideDeathScreen();
        
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }
    
    public void ShowDeathScreen()
    {
        Time.timeScale = 0f;
        
        deathScreenPanel.SetActive(true);
        
        StartCoroutine(FadeIn());
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void HideDeathScreen()
    {
        deathScreenPanel.SetActive(false);
        canvasGroup.alpha = 0f;
        
        Time.timeScale = 1f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInTime);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}