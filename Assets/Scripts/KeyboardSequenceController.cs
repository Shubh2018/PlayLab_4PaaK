using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardSequenceController : MonoBehaviour
{
    private char[] randomKeyboardSequence = new char[Constants.SequenceLength];
    [SerializeField] ParticleSystem particle; 

    void Start()
    {
        Keyboard.current.onTextInput += OnTextInput;
        Keyboard.current.aKey.IsPressed();
    }

    void OnDestroy()
    {
        if (Keyboard.current != null)
            Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char c)
    {
        if (InputManager.Paused) return;

        c = char.ToLower(c);
        
        List<Island> islands = GameManager.Instance.Islands;
        
        if (islands == null) {return;}
        
        foreach (var island in islands)
        {
            if (!island.ShipController) {continue;}
           
            if (island.ArrayPointer >= island.KeyboardSequence.Length)
            {
                continue;
            }
            
            if (c == island.KeyboardSequence[island.ArrayPointer])
            {
                Debug.Log($"Key typed: {c}");
               
                island.KeyboardSequence[island.ArrayPointer] = ' ';
                //island.Test(island.ArrayPointer);
                //particle.Play();
                island.SequenceKeyPressed();

                island.ArrayPointer += 1;
                AudioManager.Instance.KeyPress();

                if (island.ArrayPointer == island.KeyboardSequence.Length)
                {
                    Debug.Log("Full sequence completed!");
                    
                    island.ConquerIsland();
                    
                    island.ArrayPointer = 0;

                    return;
                }
            }
            /*else
            {
                Debug.Log($"Wrong key! Expected {island.KeyboardSequence[island.ArrayPointer]}, got {c}");
                //Debug.Log(island.keyboardSequence[count]);
            }*/
        }

    }

    public void ResetPointer(Island island)
    {
        island.ArrayPointer = 0;    
    }
    
    public char[] GenerateRandomSequence()
    {
        randomKeyboardSequence = new char[Constants.SequenceLength];
        for (int i = 0; i < randomKeyboardSequence.Length; i++)
        {
            randomKeyboardSequence[i] =
                Constants.KeyboardSequenceOptions[Random.Range(0, Constants.KeyboardSequenceOptions.Length)];
        }

        //Debug.Log("Random sequence: " + new string(randomKeyboardSequence));

        return randomKeyboardSequence;
    }
}
