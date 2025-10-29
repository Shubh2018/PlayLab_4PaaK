using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private Island[] _islands;

    private ShipController[] _ships;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        InputManager.EnableInput();
    }

    private void Start()
    {
        _islands = FindObjectsByType<Island>(FindObjectsSortMode.None);

        _ships = FindObjectsByType<ShipController>(FindObjectsSortMode.None);
    }

    public ShipController ReturnShip(InputManager.Team team)
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            if(_ships[i].Team == team)
                return _ships[i];    
        }

        return null;
    }

    private void OnDestroy()
    {
        InputManager.DisableInput();
    }
}