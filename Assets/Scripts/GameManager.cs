using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShipController[] _ships;
    [SerializeField] private TMP_Text[] _pointTexts;

    [SerializeField] private TMP_Text _timerText;
    private GameEndEvents _gameEndEvents;
    
    [SerializeField] private int _islandCount = 4;
    public int IslandCount => _islandCount;

    [SerializeField] private WinnerNameSO winner;

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private List<Island> _islands = new List<Island>();
    public List<Island> Islands => _islands;

    private float _pointCounter = 10;
    private float _timer = 150;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        InputManager.EnableInput();
    }

    private void Start()
    {
        _islands = FindObjectsByType<Island>(FindObjectsSortMode.None).ToList();
        _gameEndEvents = GetComponent<GameEndEvents>();
    }

    private void Update()
    {
        UpdatePointsText();
        //UpdatePointCounter(); // The following method ensured to update points over time depending on amount of conquered islands
        //UpdateTime();
    }

    public Island ReturnClosestIsland(ShipController ship)
    {
        if(ship.TargetIsland) return ship.TargetIsland;

        Island newTarget = null;
        
        foreach (Island island in _islands)
        {
            float distance = Mathf.Abs(Vector3.Distance(island.Center, ship.transform.position));

            if (distance < island.Axes.x || distance < island.Axes.y)
            {
                newTarget = island;
                break;
            }
        }
        
        return newTarget;
    }

    public Island ReturnClosestIslandOnWallCollision(ShipController ship)
    {
        if(ship.TargetIsland) return ship.TargetIsland;

        float tempClosestDistance = 10000f;
        Island newTarget = null;
        
        foreach (Island island in _islands)
        {
            float distance = Mathf.Abs(Vector3.Distance(island.Center, ship.transform.position));

            if (distance <= tempClosestDistance)
            {
                newTarget = island;
                tempClosestDistance = distance;
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
        if (_pointTexts.Length <= 0) return;
        
        for (int i = 0; i < _ships.Length; i++)
        {
            _pointTexts[i].text = $"{_ships[i].Points}";
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

    public void AddIsland(Island island)
    {
        _islands.Add(island);
    }

    private void UpdateTime()
    {
        if (_timer <= 0) 
        {
            int randomNumber = UnityEngine.Random.Range(0, 4);

            ShipController mostPoints = _ships[randomNumber];
            foreach (var ship in _ships)
            {
                if (ship.Points > mostPoints.Points) {mostPoints = ship;}
            }
            winner._winnerName = mostPoints.PlayerName;
            SceneManager.LoadScene("GameEndMenu");
            
        }
        
        _timer -= Time.deltaTime;
        _timerText.text = GetMinAndSec(_timer); 
    }

    public void WinScreen(string winnerName)
    {
        InputManager.ResetInput();
        winner._winnerName = winnerName;
        SceneManager.LoadScene("GameEndMenu");
    }

    private string GetMinAndSec(float timeInSec)
    {
        int min = (int)(timeInSec / 60);
        int sec = (int)(timeInSec % 60);

        return $"{min:00} : {sec:00}";
    }
}
