using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DestinationIsland : Island
{
    private Image[] _fillImages;
    private Animator[] _buttonAnimator;
    
    [SerializeField] private float fillPerTap = 0.1f;
    [SerializeField] private float decreasePerSec = 0.01f;

    protected override void Start()
    {
        base.Start();

        _fillImages = new Image[GameManager.Instance.FillImage.Length];
        _buttonAnimator = new Animator[GameManager.Instance.ButtonImage.Length];
        
        for (int i = 0; i < _fillImages.Length; i++)
        {
            _fillImages[i] = GameManager.Instance.FillImage[i];
        }

        for (int i = 0; i < _fillImages.Length; i++)
        {
            _buttonAnimator[i] = GameManager.Instance.ButtonImage[i];
            _buttonAnimator[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_controllersNearIsland.Count <= 0) return;
        
        CheckTaps();
    }

    private void CheckTaps()
    {
        foreach (ShipController controller in _controllersNearIsland)
        {
            if (InputManager.GetTapPressed(controller.Player))
            {
                _fillImages[(int)controller.Player - 1].fillAmount += fillPerTap;
                InputManager.SetTapToFalse(controller.Player);
                
                if (_fillImages[(int)controller.Player - 1].fillAmount >= 0.98)
                {
                    decreasePerSec = 0f;
                    _fillImages[(int)controller.Player - 1].fillAmount = 1;
                    GameManager.Instance.WinScreen(controller.name, controller.TeamColor);
                    
                    break;
                }
            }

            else
            {
                if (_fillImages[(int)controller.Player - 1].fillAmount <= 0.0f)
                    continue;
            
                _fillImages[(int)controller.Player - 1].fillAmount -= decreasePerSec * Time.deltaTime;
            }
        }
    }

    public override void EnableFillImages()
    {
        Debug.Log($"EnableFillImages Called before NullCheck!");
        
        if (_controllersNearIsland.Count <= 0) return;

        Debug.Log($"EnableFillImages Called!");
        
        foreach (ShipController controller in _controllersNearIsland)
        {
            _fillImages[(int)controller.Player - 1].fillAmount = 0;
            _buttonAnimator[(int)controller.Player - 1].gameObject.SetActive(true);
            
            // switch (controller.Player)
            // {
            //     case InputManager.Player.Player1: 
            //         break;
            //     case InputManager.Player.Player2: _fillImages[(int)controller.Player - 1].fillAmount = 0; break;
            //     case InputManager.Player.Player3: _fillImages[(int)controller.Player - 1].fillAmount = 0; break;
            //     case InputManager.Player.Player4: _fillImages[(int)controller.Player - 1].fillAmount = 0; break;
            //     default: break;
            // }
        }
    }
}