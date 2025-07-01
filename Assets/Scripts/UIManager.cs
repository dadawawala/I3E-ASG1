using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game UI")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI keycardText;
    
    [Header("Death Screen")]
    public GameObject DeathScreenPanel;
    
    [Header("Win Screen")]
    public GameObject WinScreenPanel;
    public TextMeshProUGUI winHealthText;
    public TextMeshProUGUI winMessageText;

    void Start()
    {
        if (DeathScreenPanel != null)
            DeathScreenPanel.SetActive(false);
            
        if (WinScreenPanel != null)
            WinScreenPanel.SetActive(false);
    }

    public void UpdateHealth(int value)
    {
        if (healthText != null)
            healthText.text = "Health: " + value;
    }

    public void UpdateKeycards(int collected, int total)
    {
        if (keycardText != null)
            keycardText.text = "Keycards: " + collected + " / " + total;
    }

    public void ShowDeathScreen()
    {
        if (DeathScreenPanel != null)
        {
            DeathScreenPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowWinScreen(int finalHealth)
    {
        Debug.Log("ShowWinScreen called with health: " + finalHealth);
        
        if (WinScreenPanel != null)
        {
            WinScreenPanel.SetActive(true);
            
            if (winMessageText != null)
                winMessageText.text = "Congratulations!\nYou have beaten the game!";
                
            if (winHealthText != null)
            {
                winHealthText.text = "Health Remaining: " + finalHealth;
                
                if (finalHealth > 75)
                    winHealthText.color = Color.green;
                else if (finalHealth > 50)
                    winHealthText.color = Color.yellow;
                else if (finalHealth > 25)
                    winHealthText.color = new Color(1f, 0.5f, 0f);
                else
                    winHealthText.color = Color.red;
            }
        }
        else
        {
            Debug.LogError("WinScreenPanel is not assigned in UIManager!");
        }
        
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}