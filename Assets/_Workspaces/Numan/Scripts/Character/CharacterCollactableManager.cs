using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollactableManager : MonoBehaviour
{
    [SerializeField] private CharacterInventory characterInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.TouchEnter(characterInventory);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.TouchEnter(characterInventory);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.TouchExit(characterInventory);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.TouchExit(characterInventory);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }
    private void OnCollisionStay2D(Collision2D other)
    {
        
    }
}
