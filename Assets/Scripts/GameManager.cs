using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShipController[] _ships;
    [SerializeField] private TMP_Text[] _pointTexts;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _countdownText;
    private GameEndEvents _gameEndEvents;

    [SerializeField] private int _islandCount = 4;
    public int IslandCount => _islandCount;

    [SerializeField] private WinnerNameSO winner;

    [SerializeField] private List<StartPositions> startPosition;
    [SerializeField] private Sequence[] _sequences;

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private List<Island> _islands = new List<Island>();
    public List<Island> Islands => _islands;

    [SerializeField] private UIDocument _pauseScreen;

    private float _pointCounter = 10;
    private float _timer = 150;

    private KeyboardSequenceController _keyboardSequenceController;

    public bool StartGame { private set; get; } = false;
    private float startUpTime = 0;

    private Button _resumeButton;
    private Button _mainMenuButton;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        InputManager.EnableInput();
    }

    private void Start()
    {        
        Debug.Log("Starting to setup buttons");
        
        TogglePauseScreen(false);  
        startUpTime = Constants.CountDown + 1;
        StartCoroutine(StartGameCoroutine(Constants.CountDown));
    }

    private void Update()
    {
        if (!StartGame)
        {
            startUpTime -= Time.deltaTime;
            
            _countdownText.text = ((int)startUpTime).ToString();
            //Debug.Log($"Start Up Time: {startUpTime}");

            if (startUpTime < 1)
            {
                _countdownText.text = $"GO!";
                StartGame = true;
            }
        }
        
        UpdatePointsText();
        //UpdatePointCounter(); // The following method ensured to update points over time depending on amount of conquered islands
        //UpdateTime();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        InputManager.TogglePause(!hasFocus);
        TogglePauseScreen(!hasFocus);
        
        if (!hasFocus)
        {
            Debug.Log("Game lost focus → Pausing");
        }
    }

    private IEnumerator StartGameCoroutine(float startUpTime)
    {
        InputManager.DisableInput();
        
        yield return new WaitForEndOfFrame();
        
        _islands = FindObjectsByType<Island>(FindObjectsSortMode.None).ToList();
        _gameEndEvents = GetComponent<GameEndEvents>();
        _keyboardSequenceController = FindAnyObjectByType<KeyboardSequenceController>();

        foreach (StartPositions position in startPosition)
        {
            position.SequencePanel.ToggleText(false);
            position.Player.SetIsland(position.StartingIsland);
        }

        yield return new WaitUntil(() => StartGame);
        InputManager.EnableInput();
        
        foreach (StartPositions position in startPosition)
        {
            position.SequencePanel.ToggleText(true);
        }

        yield return new WaitForSeconds(1f);
        
        Debug.Log($"Game Started!");
        _countdownText.text = $"";
    }

    public Island ReturnClosestIsland(ShipController ship)
    {
        if (ship.TargetIsland) return ship.TargetIsland;

        Island newTarget = null;

        foreach (Island island in _islands)
        {
            if (island == ship.MostRecentIsland) continue;

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
        if (ship.TargetIsland) return ship.TargetIsland;

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
                if (ship.Points > mostPoints.Points)
                {
                    mostPoints = ship;
                }
            }

            winner._winnerName = mostPoints.PlayerName;
            SceneManager.LoadScene("GameEndMenu");
        }

        _timer -= Time.deltaTime;
        _timerText.text = GetMinAndSec(_timer);
    }

    public void DisableSequence(ShipController player)
    {
        foreach (StartPositions position in startPosition)
        {
            if (position.Player.Player == player.Player)
            {
                position.SequencePanel.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void WinScreen(string winnerName)
    {
        InputManager.ResetInput();
        Constants.WinnerName = winnerName;
        SceneManager.LoadScene("GameEndMenu");
    }

    private string GetMinAndSec(float timeInSec)
    {
        int min = (int)(timeInSec / 60);
        int sec = (int)(timeInSec % 60);

        return $"{min:00} : {sec:00}";
    }

    public void SetSequence(ShipController player, char[] sequence)
    {
        foreach (StartPositions position in startPosition)
        {
            if (position.Player == player)
            {
                position.SequencePanel.SetText(sequence);
            }
        }
    }

    public void TogglePauseScreen (bool paused)
    {
        _pauseScreen.gameObject.SetActive(paused);
    }

    public void SetupPauseMenuButtons()
    {
        _resumeButton = _pauseScreen.rootVisualElement.Q<Button>("ResumeGame");
        _mainMenuButton = _pauseScreen.rootVisualElement.Q<Button>("MainMenu");

        _resumeButton.RegisterCallback<ClickEvent>(OnResumePressed);
        _mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuPressed);
    }

    private void OnResumePressed(ClickEvent e)
    {
        Debug.Log($"Resume Pressed!");
        TogglePauseScreen(false);
        InputManager.TogglePause(false);
    }
    private void OnMainMenuPressed(ClickEvent e)
    {
        SceneManager.LoadScene("MainMenu");
    }
}


[System.Serializable]
public struct StartPositions
{
    public ShipController Player;
    public Island StartingIsland;
    public Sequence SequencePanel;
}