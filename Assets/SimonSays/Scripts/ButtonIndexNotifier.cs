using System;
using UnityEngine;
using UnityEngine.Events;

namespace SimonSays.Scripts
{
    /// <summary>
    /// Attaching this to the button to notify the game controller
    /// that which specific button is pressed.
    /// </summary>
    public class ButtonIndexNotifier : MonoBehaviour
    {
        public UnityAction<int> OnButtonClicked;

        public void SendIndex(int index)
        {
            OnButtonClicked.Invoke(index);
        }

    }
}
