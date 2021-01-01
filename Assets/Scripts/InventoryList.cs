using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Just some evidence that there's nothing magic about the letter T...
public class InventoryList<Tornado> where Tornado: class
{
    private Tornado _item;
    public Tornado item
    {
        get { return _item; }
    }

    public InventoryList()
    {
        Debug.Log("Generic list initialized...");
    }

    public void SetItem(Tornado newItem)
    {
        _item = newItem;
        Debug.Log("New item added...");
    }

}
