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

    private void Awake()
    {
        _startButton = _mainMenuDocument.rootVisualElement.Q<Button>("StartGame");
        _setupButton = _mainMenuDocument.rootVisualElement.Q<Button>("Setup");
        
        _startButton.RegisterCallback<ClickEvent>(OnStartPressed);
        _setupButton.RegisterCallback<ClickEvent>(OnSetupPressed);
        
        _setupStartButton = _setupDocument.rootVisualElement.Q<Button>("Start");
        _backButton = _setupDocument.rootVisualElement.Q<Button>("Exit");
        
        _setupStartButton.RegisterCallback<ClickEvent>(OnStartPressed);
        _backButton.RegisterCallback<ClickEvent>(OnBackPressed);
        
        _mainMenuDocument.gameObject.SetActive(true);
        _setupDocument.gameObject.SetActive(false);
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
