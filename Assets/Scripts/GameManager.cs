using System;
using UnityEngine;
using TMPro;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ShipController[] _ships;
    [SerializeField] private TMP_Text[] _pointTexts;

    [SerializeField] private TMP_Text _timerText;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private Island[] _islands;

    private float _pointCounter = 10;
    private float _timer = 60;
    
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

    private void Update()
    {
        UpdatePointsText();
        UpdatePointCounter();
        UpdateTime();
    }

    public Island ReturnClosestIsland(ShipController ship)
    {
        if(ship.TargetIsland) return ship.TargetIsland;

        Island newTarget = null;
        
        foreach (Island island in _islands)
        {
            float distance = Mathf.Abs(Vector3.Distance(island.Center, ship.transform.position));

            if (distance <= island.Radius)
            {
                newTarget = island;
                break;
            }
        }
        
        return newTarget;
    }

    private void OnDestroy()
    {
        InputManager.DisableInput();
    }

    private void UpdatePointsText()
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            _pointTexts[i].text = "Player " + (i+1) + ": " + _ships[i].Points;
        }
    }

    private void UpdatePointCounter()
    {
        _pointCounter -= Time.deltaTime;

        if (_pointCounter <= 0)
        {
            foreach (var ship in _ships)
            {
                ship.UpdatePoints(ship.ConqueredIslands.Count);
            }

            _pointCounter = 5;
        }
    }

    private void UpdateTime()
    {
        if (_timer <= 0) {return;}
        
        _timer -= Time.deltaTime;
        _timerText.text = "" +  Mathf.RoundToInt(_timer); 
    }
}
