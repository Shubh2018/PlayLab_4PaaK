using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private Button setupButton;
    private List<Button> menubuttons = new List<Button>();

    [SerializeField] private GameObject setup;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("StartGame") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        setupButton = document.rootVisualElement.Q("Setup") as Button;
        setupButton.RegisterCallback<ClickEvent>(OnSetupClick);

        menubuttons = document.rootVisualElement.Query<Button>().ToList();
        
        for (int i = 0; i < menubuttons.Count; ++i) 
        {
            menubuttons[i].RegisterCallback<ClickEvent>(AllButtonsClick);
        }

    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        setupButton.UnregisterCallback<ClickEvent>(OnSetupClick);

        for (int i = 0; i < menubuttons.Count; ++i)
        {
            menubuttons[i].UnregisterCallback<ClickEvent>(AllButtonsClick);
        }
    }

    private void OnPlayGameClick (ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnSetupClick(ClickEvent evt)
    {
        SceneManager.LoadScene("SetupMenu");
    }

    private void AllButtonsClick(ClickEvent evt)
    {
        Debug.Log("A button was clicked");
    }
}


