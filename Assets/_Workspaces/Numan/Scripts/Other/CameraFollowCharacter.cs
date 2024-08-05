using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour
{
    [SerializeField] private float smoothValue;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        Vector3 vec = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, vec, smoothValue * Time.deltaTime);
    }
}
