using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    private float boundaryX = 32f; // 64/2
    private float boundaryY = 32f; // 64/2

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundaryX, boundaryX);
        pos.y = Mathf.Clamp(pos.y, -boundaryY, boundaryY);
        transform.position = pos;
    }
}
