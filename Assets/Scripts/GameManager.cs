using System;
using UnityEngine;
using TMPro;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ShipController _ship1;
    [SerializeField] private ShipController _ship2;
    [SerializeField] private ShipController _ship3;
    [SerializeField] private ShipController _ship4;

    [SerializeField] private TMP_Text _pointsTextPlayer1;
    [SerializeField] private TMP_Text _pointsTextPlayer2;
    [SerializeField] private TMP_Text _pointsTextPlayer3;
    [SerializeField] private TMP_Text _pointsTextPlayer4;

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
        _pointsTextPlayer1.text = "Player 1: " + _ship1.Points;
        _pointsTextPlayer2.text = "Player 2: " + _ship2.Points;
        _pointsTextPlayer3.text = "Player 3: " + _ship3.Points;
        _pointsTextPlayer4.text = "Player 4: " + _ship4.Points;
    }

    private void UpdatePointCounter()
    {
        _pointCounter -= Time.deltaTime;

        if (_pointCounter <= 0)
        {
            
            _ship1.UpdatePoints(_ship1.ConqueredIslands.Count);
            _ship2.UpdatePoints(_ship2.ConqueredIslands.Count);
            _ship3.UpdatePoints(_ship3.ConqueredIslands.Count);
            _ship4.UpdatePoints(_ship4.ConqueredIslands.Count);

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
