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
        int count = 0;
        if (islandArray == null) {return;}
        foreach (var island in islandArray)
        {
            count++;
            CheckSequence(island, island.KeyboardSequenceTeam1, c, InputManager.Team.Team1);
            CheckSequence(island, island.KeyboardSequenceTeam2, c, InputManager.Team.Team2);
            /*else
            {
                Debug.Log($"Wrong key! Expected {island.KeyboardSequence[island.ArrayPointer]}, got {c}");
                //Debug.Log(island.keyboardSequence[count]);
            }*/
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

    private void CheckSequence(Island island, char[] characters, char c, InputManager.Team team)
    {
        if (island.ArrayPointer >= characters.Length)
        {
            return;
        }
            
        if (c == characters[island.ArrayPointer])
        {
            Debug.Log($"Key typed: {c}");
                
            island.ArrayPointer += 1;

            if (island.ArrayPointer == characters.Length)
            {
                Debug.Log("Full sequence completed!");

                foreach (var islandClear in islandArray)
                {
                    islandClear.ArrayPointer = 0;
                    Debug.Log(islandClear.ArrayPointer);
                }
                // Run "go to island" method
                    
                //island.ConquerIsland();

                ShipController ship = GameManager.Instance.ReturnShip(team);
                Debug.Log(ship.transform.parent.name);
                
                ship.SetTarget(island);

                return;
            }
        }
    }

    public char[] GenerateRandomSequence(char[] randomSequenceContainer)
    {
        randomKeyboardSequence = new char[10];
        for (int i = 0; i < randomKeyboardSequence.Length; i++)
        {
            randomKeyboardSequence[i] =
                randomSequenceContainer[Random.Range(0, randomSequenceContainer.Length)];
        }

        Debug.Log("Random sequence: " + new string(randomKeyboardSequence));

        return randomKeyboardSequence;
    }
}
