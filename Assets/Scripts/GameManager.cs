using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private Island[] _islands;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        InputManager.EnableInput();
    }

    private void Start()
    {
        _islands = FindObjectsByType<Island>(FindObjectsSortMode.None);
    }

    private void OnDestroy()
    {
        InputManager.DisableInput();
    }
}
