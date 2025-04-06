using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // <-- Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public FoundAnimalManager foundAnimalManager;
    public GameObject winScreen;
    public GameObject loseScreen;
    public PlayerController playerController;
    public TMP_Text timerText; // <-- Using TMP_Text instead of Text

    [Header("Game Settings")]
    public int totalGarbagePiles = 13;
    public float timeLimit = 600f; // 10 minutes

    private int garbageCollected = 0;
    private float timeRemaining;
    private bool hasGameEnded = false;

    void Start()
    {
        timeRemaining = timeLimit;

        // Hide Win/Lose screens at start
        if (winScreen != null)  winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);

        // Initialize timer display once
        UpdateTimerDisplay(timeRemaining);
    }

    void Update()
    {
        if (hasGameEnded) return;

        // Count down the time
        timeRemaining -= Time.deltaTime;

        // Update the on-screen timer
        UpdateTimerDisplay(timeRemaining);

        if (timeRemaining <= 0f)
        {
            ShowLoseScreen();
        }
        else
        {
            if (CheckWinCondition())
            {
                ShowWinScreen();
            }
        }
    }

    private void UpdateTimerDisplay(float currentTime)
    {
        // Clamp time so it doesn't go below 0
        currentTime = Mathf.Max(currentTime, 0f);

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Format "MM:SS" and set the TMP text
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Called from GarbagePile when garbage is collected
    public void OnGarbageCollected()
    {
        garbageCollected++;
        if (CheckWinCondition())
        {
            ShowWinScreen();
        }
    }

    private bool CheckWinCondition()
    {
        bool allGarbageCollected = (garbageCollected >= totalGarbagePiles);

        bool allAnimalsFound =
            foundAnimalManager.foundGreenAnole &&
            foundAnimalManager.foundBrownAnole &&
            foundAnimalManager.foundDeer &&
            foundAnimalManager.foundBat &&
            foundAnimalManager.foundSnake &&
            foundAnimalManager.foundTortoise;

        return (allGarbageCollected && allAnimalsFound);
    }

    private void ShowWinScreen()
    {
        if (hasGameEnded) return;
        hasGameEnded = true;

        // Disable movement
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Show Win UI
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        Debug.Log("You Win!");
    }

    private void ShowLoseScreen()
    {
        if (hasGameEnded) return;
        hasGameEnded = true;

        // Disable movement
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Show Lose UI
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }

        Debug.Log("You Lose!");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
