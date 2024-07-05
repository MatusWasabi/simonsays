using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[Serializable]
public enum ColorChoice 
{
    None,
    Blue,
    Green,
    Red,
    Yellow,
}

public class EnumRandomizer
{
    private static readonly Random randomGen = new Random();

    public static T GetRandomEnumValue<T>(T[] enumValues) where T : Enum
    {
        int randomIndex = randomGen.Next(0, enumValues.Length);
        return enumValues[randomIndex];
    }
}
