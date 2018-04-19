using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

    #region public variables

    [Header("Statistics")]
    public float AttackRange;
    public float AttackSpeed;
    public float JumpSpeed;

    [Header("Inputs")]
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode colorChange;

    [Header("Colors")]
    public Color blueColor;
    public Color redColor;

    [Header("Canvases")]
    public GameObject GameOverScreen;
    public GameObject ScoreCanvas;

    [Header("UI Stuff")]
    public TextMeshProUGUI gameScore;
    public TextMeshProUGUI endgameScore;

    #endregion

    Vector3 playerStartPosition;
    Vector3 destination;

    bool hasFinishedAnimation = true;
    bool isAttacking = false;
    bool isFirst = true;

    int enemiesKilled = 0;

    MeshRenderer meshRenderer;

    ColorType color;

    IEnumerator attackAnimation;

    private void Start()
    {
        playerStartPosition = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        int randomColorIndex = Random.Range(0, 2);
        if (randomColorIndex == 0)
        {
            meshRenderer.material.color = blueColor;
            color = ColorType.blue;
        }
        else
        {
            meshRenderer.material.color = redColor;
            color = ColorType.red;
        }
        gameScore.text = enemiesKilled.ToString();
        endgameScore.text = enemiesKilled.ToString();
    }

    private void Update()
    {
        if (hasFinishedAnimation)
        {
            if (Input.GetKeyDown(Up))
            {
                destination = new Vector3(transform.position.x, transform.position.y, AttackRange);
                attackAnimation = AttackAnimation(destination, AttackSpeed);
                StartCoroutine(attackAnimation);
            }
            if (Input.GetKeyDown(Down))
            {
                destination = new Vector3(transform.position.x, transform.position.y, -AttackRange);
                attackAnimation = AttackAnimation(destination, AttackSpeed);
                StartCoroutine(attackAnimation);
            }
            if (Input.GetKeyDown(Right))
            {
                destination = new Vector3(AttackRange, transform.position.y, transform.position.z);
                attackAnimation = AttackAnimation(destination, AttackSpeed);
                StartCoroutine(attackAnimation);
            }
            if (Input.GetKeyDown(Left))
            {
                destination = new Vector3(-AttackRange, transform.position.y, transform.position.z);
                attackAnimation = AttackAnimation(destination, AttackSpeed);
                StartCoroutine(attackAnimation);
            }
        }

        if (Input.GetKeyDown(colorChange))
        {
            ColorChange();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        ScoreCanvas.SetActive(false);
        GameOverScreen.SetActive(true);
    }

    IEnumerator AttackAnimation(Vector3 destination, float speed)
    {
        hasFinishedAnimation = false;
        isAttacking = true;
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        isAttacking = false;
        StartCoroutine(ReturnAnimation(playerStartPosition, AttackSpeed));
    }

    IEnumerator ReturnAnimation(Vector3 destination, float speed)
    {
        while(transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        ColorChange();
        hasFinishedAnimation = true;
        isFirst = true;
    }

    IEnumerator ColorJumpAnimation(Color _color)
    {
        float startHeight = transform.position.y;
        float peakHeight = 2f;
        float percent = 0;

        while(percent <= 1)
        {
            percent += Time.deltaTime * JumpSpeed;
            float lerpValue = (-percent * percent + percent) * 4;
            float currentHeight = Mathf.Lerp(startHeight, peakHeight, lerpValue);
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, _color, lerpValue);

            yield return null;
        }

    }

    void ColorChange()
    {
        if(color == ColorType.blue)
        {
            StartCoroutine(ColorJumpAnimation(redColor));
            color = ColorType.red;
        }
        else
        {
            StartCoroutine(ColorJumpAnimation(blueColor));
            color = ColorType.blue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy && isFirst)
        {
            Fight(enemy);
        }
    }

    void Fight(EnemyController enemy)
    {
        if(color == enemy.color && isAttacking)
        {
            enemy.Die();
            enemiesKilled++;
            gameScore.text = enemiesKilled.ToString();
            endgameScore.text = enemiesKilled.ToString();
            if (attackAnimation != null)
                StopCoroutine(attackAnimation);
            StartCoroutine(ReturnAnimation(playerStartPosition, AttackSpeed));
        }
        else
        {
            Die();
        }
    }

}

public enum ColorType
{
    blue,red
}
