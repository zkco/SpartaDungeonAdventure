using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public GameObject Item;
    public Condition Condition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        Controller = GetComponent<PlayerController>();
    }
}
