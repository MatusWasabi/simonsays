using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


namespace SimonSays.Scripts
{
    
    public class SimonSequence : MonoBehaviour
    {
        [FormerlySerializedAs("_buttons")] [SerializeField] private List<Button> buttons;
        [FormerlySerializedAs("_sequences")] [SerializeField] private List<int> sequences = new List<int>();
        [FormerlySerializedAs("_secondDelay")] [SerializeField] [Range(0f, 1.0f)] private float secondDelay = 0.5f;
        
        [SerializeField] private UnityEvent<int> onScoreUpdated;
        [SerializeField] private UnityEvent onGameEnded;
        [SerializeField] private UnityEvent onStartHinting;
        [SerializeField] private UnityEvent onEndHinting;
        [SerializeField] private UnityEvent onButtonHighlighted;
    
        private int _playerIndex = 0;
        
        private void OnEnable()
        {
            foreach (Button button in buttons)
            {
                button.GetComponent<ButtonIndexNotifier>().OnButtonClicked += ButtonPressed;
            }
        }

        private void OnDisable()
        {
            foreach (Button button in buttons)
            {
                button.GetComponent<ButtonIndexNotifier>().OnButtonClicked -= ButtonPressed;
            }
        }
    
        public void StartGame()
        {
            AddSequence();
            StartCoroutine(PlayAllSequences());
        }

        public void ClearSequence()
        {
            sequences.Clear();
        }

        #region Private Methods

        private void AddSequence()
        {
            int randomButton = Random.Range(0, buttons.Count);
            sequences.Add(randomButton);
        }

        private IEnumerator PlayAllSequences()
        {
            onStartHinting.Invoke();
            foreach (int buttonIndex in sequences)
            {
                Button hightlighedButton = buttons[buttonIndex];
                Image image = hightlighedButton.GetComponent<Image>();
                yield return new WaitForSeconds(secondDelay);
                
            
                // Highlight it 
                onButtonHighlighted.Invoke();
                image.color = hightlighedButton.colors.highlightedColor;
                yield return new WaitForSeconds(secondDelay);
            
                // Normalize it back to default state
                image.color = Color.white;
            }
            onEndHinting.Invoke();
        }

        private void ButtonPressed(int buttonIndex)
        {
            if (!IsButtonMatchSequence(buttonIndex))
            {
                onGameEnded.Invoke();
                return;
            }
        
            _playerIndex++;
            if (_playerIndex == sequences.Count)
            {
                _playerIndex = 0;
                onScoreUpdated.Invoke(sequences.Count);
                AddSequence();
                StartCoroutine(PlayAllSequences());
            }
        }

        private bool IsButtonMatchSequence(int buttonIndex)
        {
            return sequences[_playerIndex] == buttonIndex;
        }

        #endregion
        
    }
}
