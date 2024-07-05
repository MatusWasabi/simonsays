using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


public class SimonSequence : MonoBehaviour
{
    [FormerlySerializedAs("_buttons")] [SerializeField] private List<Button> buttons;
    [SerializeField] private List<int> _sequences = new List<int>();
    [SerializeField] private UnityEvent<int> onScoreUpdated;
    [SerializeField] private UnityEvent onGameEnded;
    [SerializeField] private UnityEvent onButtonHighlighted;
    
    private int playerIndex = 0;
    private bool playerTurn = false;
    private float _secondDelay = 0.5f;
    

    private void OnEnable()
    {
        ButtonIndexNotifier.OnButtonSelected += ButtonPressed;
    }

    private void OnDisable()
    {
        ButtonIndexNotifier.OnButtonSelected -= ButtonPressed;
    }
    
    public void StartGame()
    {
        _sequences.Clear();
        AddSequence();
        StartCoroutine(PlayAllSequences());
    }

    private void AddSequence()
    {
        
        int randomButton = Random.Range(0, buttons.Count);
        _sequences.Add(randomButton);
    }

    public IEnumerator PlayAllSequences()
    {
        playerTurn = false;
        foreach (int buttonIndex in _sequences)
        {
            Button hightlighedButton = buttons[buttonIndex];
            Image _image = hightlighedButton.GetComponent<Image>();
            yield return new WaitForSeconds(_secondDelay);
            onButtonHighlighted.Invoke();
            
            // Highlight it 
            _image.color = hightlighedButton.colors.highlightedColor;
            yield return new WaitForSeconds(_secondDelay);
            
            // Normalize it back to default state
            _image.color = Color.white;
        }
        playerTurn = true;
    }


    public void ButtonPressed(int buttonIndex)
    {
        if (!playerTurn) return;

        if (_sequences[playerIndex] != buttonIndex)
        {
            onGameEnded.Invoke();
            return;
        }
        
        playerIndex++;
        if (playerIndex == _sequences.Count)
        {
            playerIndex = 0;
            onScoreUpdated.Invoke(_sequences.Count);
            AddSequence();
            StartCoroutine(PlayAllSequences());
        }
    }
}
