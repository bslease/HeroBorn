using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public Stack<string> lootStack = new Stack<string>();

    public int GUIFontSize = 18;
    private GUIStyle guiStyle = new GUIStyle();

    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected;  }
        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if(_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's gotta hurt.";
            }
        }
    }

    void Start()
    {
        Initialize();    
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mytrhil Bracers");
    }

    // NOTE: OnGUI still gets called even when Time.timeScale is 0
    void OnGUI()
    {
        //GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        //GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);
        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        guiStyle = new GUIStyle(GUI.skin.box);
        guiStyle.fontSize = GUIFontSize;
        GUI.Box(new Rect(20, 20, 300, 50), "Player Health:" + _playerHP, guiStyle);
        GUI.Box(new Rect(20, 80, 300, 50), "Items Collected:" + _itemsCollected, guiStyle);

        guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = GUIFontSize;
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 50, 600, 50), labelText, guiStyle);

        if (showWinScreen)
        {
            guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.fontSize = GUIFontSize;
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!", guiStyle))
            {
                Utilities.RestartLevel(0);
            }
        }

        if (showLossScreen)
        {
            guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.fontSize = GUIFontSize;
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose...", guiStyle))
            {
                Utilities.RestartLevel();
            }
        }
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
        string s = lootStack.ToString();
        Debug.Log(s);
    }
}
