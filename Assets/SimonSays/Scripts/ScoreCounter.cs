using TMPro;
using UnityEngine;

namespace SimonSays.Scripts
{
    /// <summary>
    /// For updating the text in TextMeshPro.
    /// </summary>
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text displayText;

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
}