using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTest : MonoBehaviour
{
    public Image _fill;

    private float time = 10.0f;

    private float fillPerTap = 0.1f;

    private float decreasePerSec = 0.01f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.EnableInput();
    }

    private void Update()
    {
        if (InputManager.GetTapPressed(InputManager.Player.Player1))
        {
            _fill.fillAmount += fillPerTap;
            InputManager.SetTapToFalse(InputManager.Player.Player1);
        }

        else
        {
            if (_fill.fillAmount <= 0.0f)
                return;
            
            _fill.fillAmount -= fillPerTap * Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        InputManager.DisableInput();
    }
}
