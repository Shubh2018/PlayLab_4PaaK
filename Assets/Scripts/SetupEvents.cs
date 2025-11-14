using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SetupEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private Button startButton;
    private List<Button> menubuttons = new List<Button>();

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("Exit") as Button;
        button.RegisterCallback<ClickEvent>(OnExitClick);
        
        startButton = document.rootVisualElement.Q("Start") as Button;
        startButton.RegisterCallback<ClickEvent>(StartGame);

        menubuttons = document.rootVisualElement.Query<Button>().ToList();
        
        for (int i = 0; i < menubuttons.Count; ++i) 
        {
            menubuttons[i].RegisterCallback<ClickEvent>(AllButtonsClick);
        }
    }

    private void StartGame(ClickEvent e)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);
        button.UnregisterCallback<ClickEvent>(OnExitClick);
        
        for (int i = 0; i < menubuttons.Count; ++i)
        {
            menubuttons[i].UnregisterCallback<ClickEvent>(AllButtonsClick);
        }
    }

    private void OnExitClick (ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AllButtonsClick(ClickEvent evt)
    {
        Debug.Log("A button was clicked");
    }
}


