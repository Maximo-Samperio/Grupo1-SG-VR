using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;    // Or TMPro if you prefer

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI Settings")]
    [Tooltip("A UI Text (or TextMeshProUGUI) to show the current score.")]
    public TextMeshProUGUI scoreText;  // ⇢ change to TextMeshProUGUI if needed

    private int currentScore = 0;

    void Awake()
    {
        // Simple singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // Optional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
    }
}
