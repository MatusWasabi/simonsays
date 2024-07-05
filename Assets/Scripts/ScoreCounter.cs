using System;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText; // Reference to your TMP_Text for displaying interpolated text

    private void Awake()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int newScore)
    {
        string interpolatedText = $"Score: {newScore}";
        
        displayText.text = interpolatedText;
    }
    
}