using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private GameObject foundPlayerIconObject;

    Rigidbody2D rb2d;

    private GameObject player;
    NavMeshAgent agent;

    public List<MoveNode> moveNodes;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        _moveNode = moveNodes[0];
        StartCoroutine(enumeratorMove());
    }

    private MoveNode _moveNode;
    int _counter = 0;
    float _waitTimer = 0;
    IEnumerator enumeratorMove()
    {
        Rotate(_moveNode.transform.position);
        while (true)
        {
            //Vector2 direction = (fov.player.position - transform.position).normalized;
            //rb2d.velocity = direction * moveSpeed;
            agent.SetDestination(_moveNode.transform.position);
            if (fov.player != null) { break; }
            if (Vector2.Distance(transform.position, _moveNode.transform.position) < 0.25f)
            {
                //agent.isStopped = true;
                if (_waitTimer >= _moveNode.waitTime)
                {
                    _waitTimer = 0;

                    if (moveNodes[moveNodes.Count - 1].transform == _moveNode.transform)
                    {
                        _moveNode = moveNodes[0];
                        _counter = 0;
                    }
                    else
                    {
                        _counter++;
                        _moveNode = moveNodes[_counter];
                    }
                    Rotate(_moveNode.transform.position);
                }
                else
                {
                    _waitTimer += 0.1f;
                }

            }
            else
            {
                //agent.isStopped = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
    void Rotate(Vector3 targetPos)
    {
        Vector2 direction = (transform.position - targetPos).normalized;
        if (direction.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            fov.transform.localRotation = Quaternion.Euler(0, 0, -90);
            fov.factor = 0;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            fov.transform.localRotation = Quaternion.Euler(0, 0, 270);
            fov.factor = 180;
        }

    }

    void Update()
    {
        if (fov.player != null)
        {
            foundPlayerIconObject.SetActive(true);
            agent.SetDestination(player.transform.position);
            Rotate(player.transform.position);

            //Vector2.MoveTowards(transform.position, fov.player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            foundPlayerIconObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class MoveNode
{
    public Transform transform;
    public float waitTime;
}
