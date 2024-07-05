using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndexNotifier : MonoBehaviour
{
    public static Action<int> OnButtonSelected;

    public void SendIndex(int index)
    {
        OnButtonSelected.Invoke(index);
    }

}
