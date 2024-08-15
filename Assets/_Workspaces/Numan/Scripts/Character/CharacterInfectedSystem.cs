using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterInfectedSystem : MonoBehaviour
{
    public Collider2D parentCollider;
    public float infectionTime;

    public bool isIntoTheHuman;
    public SpriteRenderer spriteRenderer;
    public Collider2D myCollider2D;

    public GameObject IntoTheHumanEnterCanvas;

    private GameObject _human;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isIntoTheHuman) { return; }
        if (other.tag == "Enemy")
        {
            if (other.TryGetComponent(out Enemy enemy) && !enemy.isInfected)
            {
                IntoTheHumanEnterCanvas.SetActive(true);
                _human = other.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isIntoTheHuman) { return; }
        if (other.tag == "Enemy")
        {
            if (_human == other.gameObject && !isIntoTheHuman)
            {
                IntoTheHumanEnterCanvas.SetActive(false);
                _human = null;
            }
        }
    }

    private void Update()
    {
        if (_human != null && Input.GetKeyDown(KeyCode.E) && !_human.GetComponent<Enemy>().isInfected)
        {
            if (!isIntoTheHuman)
            {
                IntoTheHuman();
            }
        }

        if (isIntoTheHuman)
        {
            transform.parent.transform.position = _human.transform.position;
            parentCollider.enabled = false;
        }else{
            parentCollider.enabled = true;
        }
    }
    private void IntoTheHuman()
    {
        IntoTheHumanEnterCanvas.SetActive(false);
        isIntoTheHuman = true;
        spriteRenderer.enabled = false;
        myCollider2D.enabled = false;
        _human.GetComponent<Enemy>().slider.gameObject.SetActive(true);
        _human.GetComponent<Enemy>().slider.value = 0;
        _human.GetComponent<Enemy>().sliderImage.color = Color.green;
        _human.GetComponent<Enemy>().slider.maxValue = infectionTime;
        StartCoroutine(infectionEnumator());
    }
    private void OutTheHuman()
    {
        isIntoTheHuman = false;
        spriteRenderer.enabled = true;
        myCollider2D.enabled = true;
        _human.GetComponent<Enemy>().slider.gameObject.SetActive(false);
    }

    float _waitTimer = 0;
    IEnumerator infectionEnumator()
    {
        _human.GetComponent<Enemy>().animator.Play("ConvertInfected");
        while (true)
        {
            if (_waitTimer >= infectionTime)
            {
                _human.GetComponent<Enemy>().isInfected = true;
                _waitTimer = 0;
                OutTheHuman();
                break;
            }
            _human.GetComponent<Enemy>().slider.value = _waitTimer;
            _waitTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
