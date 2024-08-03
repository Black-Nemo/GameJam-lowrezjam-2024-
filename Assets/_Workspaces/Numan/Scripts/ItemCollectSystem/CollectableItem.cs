using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour, ICollectable
{
    public string Name;
    public int Count;

    public GameObject InfoCanvas;

    private bool canCollect;
    private CharacterInventory characterInventory;

    public void Collect()
    {
        characterInventory.AddItem(Name, Count);
        Destroy(gameObject);
    }

    public void TouchEnter(CharacterInventory _characterInventory)
    {
        characterInventory = _characterInventory;
        InfoCanvas.SetActive(true);
        canCollect = true;
    }

    public void TouchExit(CharacterInventory _characterInventory)
    {
        InfoCanvas.SetActive(false);
        canCollect = false;
    }

    private void Update() {
        if(canCollect){
            if(Input.GetKeyDown(KeyCode.E)){
                Collect();
            }
        }
    }
}
