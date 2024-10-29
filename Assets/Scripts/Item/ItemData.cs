using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ItemType
{
    Dura,
    Temp,
}

public enum ValueType
{
    HP,
    Speed,
}

[Serializable]
public class ItemUsuable
{
    public ValueType Type;
    public float Value;
    public float Time;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Item")]
    public string ItemName;
    public string ItemDescription;
    public ItemType ItemType;

    public ItemUsuable[] ItemUsuable;
    //[Header("")]
    //[Header("")]
}
