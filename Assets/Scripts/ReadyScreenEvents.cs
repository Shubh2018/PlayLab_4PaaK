using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class ReadyScreenEvents : MonoBehaviour
{
    private struct KeyMapEntry
    {
        public ButtonControl key;
        public VisualElement element;
        public Texture2D pressedTex;
        public Texture2D notPressedTex;
        public bool hasEverBeenPressed;

        public KeyMapEntry(ButtonControl key, VisualElement element, Texture2D pressedTex, Texture2D notPressedTex)
        {
            this.key = key;
            this.element = element;
            this.pressedTex = pressedTex;
            this.notPressedTex = notPressedTex;
            this.hasEverBeenPressed = false;
        }
    }

    [SerializeField] private UIDocument _readyScreen;
    [SerializeField] private Texture2D[] buttonVisuals;

    private VisualElement key1;
    private VisualElement keyQ;
    private VisualElement keyA;
    private VisualElement keyZ;
    private VisualElement keyX;
    private VisualElement keyC;
    private VisualElement keyB;
    private VisualElement keyN;
    private VisualElement keyM;
    private VisualElement keyK;
    private VisualElement keyO;
    private VisualElement key0;

    private KeyMapEntry[] keyMap;

    void Start()
    {
        key1 = _readyScreen.rootVisualElement.Q("notPressed1");
        keyQ = _readyScreen.rootVisualElement.Q("notPressedQ");
        keyA = _readyScreen.rootVisualElement.Q("notPressedA");
        keyZ = _readyScreen.rootVisualElement.Q("notPressedZ");
        keyX = _readyScreen.rootVisualElement.Q("notPressedX");
        keyC = _readyScreen.rootVisualElement.Q("notPressedC");
        keyB = _readyScreen.rootVisualElement.Q("notPressedB");
        keyN = _readyScreen.rootVisualElement.Q("notPressedN");
        keyM = _readyScreen.rootVisualElement.Q("notPressedM");
        keyK = _readyScreen.rootVisualElement.Q("notPressedK");
        keyO = _readyScreen.rootVisualElement.Q("notPressedO");
        key0 = _readyScreen.rootVisualElement.Q("notPressed0");
        //Keyboard.current.aKey.IsPressed();

        keyMap = new KeyMapEntry[]
        {
            new KeyMapEntry(Keyboard.current.digit1Key, key1, buttonVisuals[0], buttonVisuals[1]),
            new KeyMapEntry(Keyboard.current.qKey,      keyQ, buttonVisuals[0], buttonVisuals[1]),
            new KeyMapEntry(Keyboard.current.aKey,      keyA, buttonVisuals[0], buttonVisuals[1]),

            new KeyMapEntry(Keyboard.current.zKey,      keyZ, buttonVisuals[2], buttonVisuals[3]),
            new KeyMapEntry(Keyboard.current.xKey,      keyX, buttonVisuals[2], buttonVisuals[3]),
            new KeyMapEntry(Keyboard.current.cKey,      keyC, buttonVisuals[2], buttonVisuals[3]),

            new KeyMapEntry(Keyboard.current.bKey,      keyB, buttonVisuals[4], buttonVisuals[5]),
            new KeyMapEntry(Keyboard.current.nKey,      keyN, buttonVisuals[4], buttonVisuals[5]),
            new KeyMapEntry(Keyboard.current.mKey,      keyM, buttonVisuals[4], buttonVisuals[5]),

            new KeyMapEntry(Keyboard.current.kKey,      keyK, buttonVisuals[6], buttonVisuals[7]),
            new KeyMapEntry(Keyboard.current.oKey,      keyO, buttonVisuals[6], buttonVisuals[7]),
            new KeyMapEntry(Keyboard.current.digit0Key, key0, buttonVisuals[6], buttonVisuals[7]),
        };
    }

    void Update()
    {
        checkPressedKeys();
    }
    void OnDestroy()
    {
        
    }



    private void checkPressedKeys()
{
    for (int i = 0; i < keyMap.Length; i++)
    {
        bool currentlyPressed = keyMap[i].key.isPressed;

        // Update the visual every frame
        keyMap[i].element.style.backgroundImage =
            new StyleBackground(currentlyPressed ? keyMap[i].pressedTex : keyMap[i].notPressedTex);

        // If the key is pressed this frame, permanently record it
        if (currentlyPressed)
            keyMap[i].hasEverBeenPressed = true;
    }

    // Check if all keys have been pressed at least once
    bool allPressedOnce = true;

    foreach (var entry in keyMap)
    {
        if (!entry.hasEverBeenPressed)
        {
            allPressedOnce = false;
            break;
        }
    }

    if (allPressedOnce)
        SceneManager.LoadScene("GameSceneRace");
}





}


