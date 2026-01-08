using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameEndEvents : MonoBehaviour
{
    private UIDocument _gameEndDocument;
    private Button _playAgainButton;
    private Button _mainMenuButton;
    private Label _playerWonText;
    private VisualElement _playerWonSprite;
    private List<Button> _gameEndMenuButtons = new List<Button>();

    [SerializeField] private WinnerNameSO winner;


    public string _playerWhoWon;

    private void Awake()
    {
        _gameEndDocument = GetComponent<UIDocument>();

        _playAgainButton = _gameEndDocument.rootVisualElement.Q("PlayAgain") as Button;
        _playAgainButton.RegisterCallback<ClickEvent>(OnPlayAgainClick);

        _mainMenuButton = _gameEndDocument.rootVisualElement.Q("MainMenu") as Button;
        _mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);

        _playerWonText = _gameEndDocument.rootVisualElement.Q("PlayerWonText") as Label;
        _playerWonText.text = "" + Constants.WinnerName + " Has Won The Game";
        _playerWonSprite = _gameEndDocument.rootVisualElement.Q("PlayerWonSprite") as VisualElement;
        _playerWonSprite.style.backgroundImage = new StyleBackground(Constants.WinnerSprite);
        _gameEndMenuButtons = _gameEndDocument.rootVisualElement.Query<Button>().ToList();
        
        for (int i = 0; i < _gameEndMenuButtons.Count; ++i) 
        {
            _gameEndMenuButtons[i].RegisterCallback<ClickEvent>(AllButtonsClick);
        }

        Constants.WinnerName = String.Empty;
    }

    private void OnDisable()
    {
        _playAgainButton.UnregisterCallback<ClickEvent>(OnPlayAgainClick);
        _mainMenuButton.UnregisterCallback<ClickEvent>(OnMainMenuClick);

        for (int i = 0; i < _gameEndMenuButtons.Count; ++i)
        {
            _gameEndMenuButtons[i].UnregisterCallback<ClickEvent>(AllButtonsClick);
        }
    }

    private void OnPlayAgainClick (ClickEvent evt)
    {
        SceneManager.LoadScene("GameSceneRace");
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AllButtonsClick(ClickEvent evt)
    {
        Debug.Log("A button was clicked");
    }

    public void SetPlayerWhoWon(ShipController player)
    {
        _playerWhoWon = Constants.WinnerName;
        Constants.WinnerName = String.Empty;
    }
}


