using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool isInfected;

    public GameObject infectedSpriteObject;
    public GameObject notInfectedSpriteObject;
    public Slider slider;
    public Image sliderImage;

    public string tagName;


    public float moveSpeed;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private GameObject foundPlayerIconObject;

    Rigidbody2D rb2d;

    //private GameObject player;
    NavMeshAgent agent;


    public List<MoveNode> moveRandomNodes;

    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        rb2d = GetComponent<Rigidbody2D>();
        //player = GameObject.FindWithTag("Player");


        GameObject[] moveNodesObject = GameObject.FindGameObjectsWithTag(tagName);
        foreach (var item in moveNodesObject)
        {
            moveRandomNodes.Add(new MoveNode() { waitTime = 1, transform = item.transform });
        }
        StartCoroutine(enumeratorMoveRandom());
    }

    private MoveNode _moveNode;
    int _counter = 0;
    float _waitTimer = 0;
    IEnumerator enumeratorMoveRandom()
    {
        while (true)
        {
            if (_moveNode == null)
            {
                int rnd = Random.Range(0, moveRandomNodes.Count);
                _moveNode = moveRandomNodes[rnd];
                Rotate(_moveNode.transform.position);
            }
            //Vector2 direction = (fov.player.position - transform.position).normalized;
            //rb2d.velocity = direction * moveSpeed;
            agent.SetDestination(_moveNode.transform.position);
            if (fov.player != null) { _moveNode = null; break; }
            if (Vector2.Distance(transform.position, _moveNode.transform.position) < 0.25f)
            {
                //agent.isStopped = true;
                if (_waitTimer >= _moveNode.waitTime)
                {
                    _waitTimer = 0;

                    int rnd = Random.Range(0, moveRandomNodes.Count);
                    _moveNode = moveRandomNodes[rnd];
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
        if (isInfected)
        {
            
            //infectedSpriteObject.SetActive(true);
            //notInfectedSpriteObject.SetActive(false);
        }
        else
        {

            //infectedSpriteObject.SetActive(false);
            //notInfectedSpriteObject.SetActive(true);
        }

        if (fov.player != null)
        {
            foundPlayerIconObject.SetActive(true);
            agent.SetDestination(fov.player.transform.position);
            Rotate(fov.player.transform.position);

            //Vector2.MoveTowards(transform.position, fov.player.position, moveSpeed * Time.deltaTime);
        }
        else if (_moveNode == null)
        {
            StartCoroutine(enumeratorMoveRandom());
        }
        else
        {
            foundPlayerIconObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fov.player != null && fov.player.gameObject == other.gameObject)
        {
            other.gameObject.GetComponent<Enemy>().slider.gameObject.SetActive(true);
            other.gameObject.GetComponent<Enemy>().slider.value = 0;
            other.gameObject.GetComponent<Enemy>().slider.maxValue = infectionReverseTime;
            other.gameObject.GetComponent<Enemy>().sliderImage.color = Color.red;
            StartCoroutine(infectionReverseEnumator(other.gameObject));
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (fov.player != null && fov.player.gameObject == other.gameObject)
        {
            other.gameObject.GetComponent<Enemy>().slider.gameObject.SetActive(true);
            other.gameObject.GetComponent<Enemy>().slider.value = 0;
            other.gameObject.GetComponent<Enemy>().slider.maxValue = infectionReverseTime;
            other.gameObject.GetComponent<Enemy>().sliderImage.color = Color.red;
            StartCoroutine(infectionReverseEnumator(other.gameObject));
        }
    }

    public float infectionReverseTime;
    float _waitTimerInfectionReverse = 0;
    IEnumerator infectionReverseEnumator(GameObject other)
    {
        other.gameObject.GetComponent<Enemy>().animator.Play("ConvertNotInfected");
        while (true)
        {
            if (_waitTimerInfectionReverse >= infectionReverseTime)
            {
                other.gameObject.GetComponent<Enemy>().slider.gameObject.SetActive(false);
                other.gameObject.GetComponent<Enemy>().isInfected = false;
                fov.player = null;
                _moveNode = null;
                _waitTimerInfectionReverse = 0;
                break;
            }
            else if (isInfected)
            {
                other.gameObject.GetComponent<Enemy>().slider.gameObject.SetActive(false);
                other.gameObject.GetComponent<Enemy>().isInfected = true;
                fov.player = null;
                _moveNode = null;
                _waitTimerInfectionReverse = 0;
                break;
            }
            other.gameObject.GetComponent<Enemy>().slider.value = _waitTimerInfectionReverse;
            _waitTimerInfectionReverse += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

[System.Serializable]
public class MoveNode
{
    public Transform transform;
    public float waitTime;
}
