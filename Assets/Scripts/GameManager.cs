using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        InputManager.EnableInput();
    }

    private void OnDestroy()
    {
        InputManager.DisableInput();
    }
}
