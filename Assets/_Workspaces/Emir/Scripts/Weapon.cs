using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Camera _camera;
    private void Rotate()
    {
        Vector2 direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = -direction;
    }
    
    void Update()
    {
        Rotate();
    }
}
