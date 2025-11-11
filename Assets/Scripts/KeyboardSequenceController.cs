using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardSequenceController : MonoBehaviour
{
    
    [SerializeField] Island[] islandArray;

    private char[] randomKeyboardSequence = new char[Constants.SequenceLength];

    void Start()
    {
        islandArray = FindObjectsByType<Island>(FindObjectsSortMode.None);
        Keyboard.current.onTextInput += OnTextInput;
    }

    void OnDestroy()
    {
        if (Keyboard.current != null)
            Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char c)
    {
        if (islandArray == null) {return;}
        
        foreach (var island in islandArray)
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
                island.SequenceKeyPressed();

                island.ArrayPointer += 1;
                

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

    public char[] GenerateRandomSequence()
    {
        randomKeyboardSequence = new char[Constants.SequenceLength];
        for (int i = 0; i < randomKeyboardSequence.Length; i++)
        {
            randomKeyboardSequence[i] =
                Constants.KeyboardSequenceOptions[Random.Range(0, Constants.KeyboardSequenceOptions.Length)];
        }

        Debug.Log("Random sequence: " + new string(randomKeyboardSequence));

        return randomKeyboardSequence;
    }
}
