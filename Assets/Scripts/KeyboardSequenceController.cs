using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardSequenceController : MonoBehaviour
{
    
    [SerializeField] Island[] islandArray;

    private char[] randomKeyboardSequence = new char[10];
    //private int arrayPointer = 0;

    void Start()
    {
        //island1.KeyboardSequence = GenerateRandomSequence();
        //island2.KeyboardSequence = GenerateRandomSequence();
        //island3.KeyboardSequence = GenerateRandomSequence();
        //island4.KeyboardSequence = GenerateRandomSequence();
        //island5.KeyboardSequence = GenerateRandomSequence();
        //GenerateRandomSequence();
        // Subscribe to text input
        Keyboard.current.onTextInput += OnTextInput;
    }

    void OnDestroy()
    {
        if (Keyboard.current != null)
            Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char c)
    {
        int count = 0;
        if (islandArray == null) {return;}
        foreach (var island in islandArray)
        {
            count++;
            if (island.ArrayPointer >= island.keyboardSequence.Length)
            {
                return;
            }
            else if (c == island.keyboardSequence[island.ArrayPointer])
            {
                Debug.Log($"Key typed: {c}");
                
                island.ArrayPointer += 1;

                if (island.ArrayPointer == island.keyboardSequence.Length)
                {
                    Debug.Log("Full sequence completed!");

                    foreach (var islandClear in islandArray)
                    {
                        islandClear.ArrayPointer = 0;
                        Debug.Log(islandClear.ArrayPointer);
                    }
                    // Run "go to island" method

                    return;
                }

            }
            else
            {
                Debug.Log($"Wrong key! Expected {island.keyboardSequence[island.ArrayPointer]}, got {c}");
                //Debug.Log(island.keyboardSequence[count]);
            }
            
        }

        /*
        // Only allow characters present in your sequence options
        if (arrayPointer >= randomKeyboardSequence.Length)
            return;

        Debug.Log($"Key typed: {c}");

        if (c == randomKeyboardSequence[arrayPointer])
        {
            Debug.Log($"Correct key! ({c})");
            arrayPointer++;

            if (arrayPointer == randomKeyboardSequence.Length)
                Debug.Log("Full sequence completed!");
        }
        else
        {
            Debug.Log($"Wrong key! Expected {randomKeyboardSequence[arrayPointer]}, got {c}");
            arrayPointer = 0; // reset sequence
        }
        */

    }

    public char[] GenerateRandomSequence()
    {
        randomKeyboardSequence = new char[10];
        for (int i = 0; i < randomKeyboardSequence.Length; i++)
        {
            randomKeyboardSequence[i] =
                Constants.KeyboardSequenceOptions[Random.Range(0, Constants.KeyboardSequenceOptions.Length)];
        }

        Debug.Log("Random sequence: " + new string(randomKeyboardSequence));

        return randomKeyboardSequence;
    }
}
