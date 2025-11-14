using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument _mainMenuDocument;
    [SerializeField] private UIDocument _setupDocument;

    private Button _startButton;
    private Button _setupButton;
    
    private Button _setupStartButton;
    private Button _backButton;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SetMainMenuDocReferences()
    {
        _startButton = _mainMenuDocument.rootVisualElement.Q<Button>("StartGame");
        _setupButton = _mainMenuDocument.rootVisualElement.Q<Button>("Setup");
        
        _startButton.RegisterCallback<ClickEvent>(OnStartPressed);
        _setupButton.RegisterCallback<ClickEvent>(OnSetupPressed);
    }

    public void SetSetupDocumentReferences()
    {
        _setupStartButton = _setupDocument.rootVisualElement.Q<Button>("Start");
        _backButton = _setupDocument.rootVisualElement.Q<Button>("Exit");
        
        _setupStartButton.RegisterCallback<ClickEvent>(OnStartPressed);
        _backButton.RegisterCallback<ClickEvent>(OnBackPressed);
    }

    public void UnsetMainMenuDoc()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnStartPressed);
        _setupButton.UnregisterCallback<ClickEvent>(OnSetupPressed);
    }

    public void UnsetSetupDocument()
    {
        _setupStartButton.UnregisterCallback<ClickEvent>(OnStartPressed);
        _backButton.UnregisterCallback<ClickEvent>(OnBackPressed);
    }

    private void OnStartPressed(ClickEvent e)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnSetupPressed(ClickEvent e)
    {
        _mainMenuDocument.gameObject.SetActive(false);
        _setupDocument.gameObject.SetActive(true);
    }

    private void OnBackPressed(ClickEvent e)
    {
        _mainMenuDocument.gameObject.SetActive(true);
        _setupDocument.gameObject.SetActive(false);
    }
}
