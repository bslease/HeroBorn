﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;

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
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    void OnGUI()
    {
        //GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        //GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);
        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        guiStyle.fontSize = GUIFontSize;
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP, guiStyle);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected, guiStyle);
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 50, 300, 50), labelText, guiStyle);
    }
}
