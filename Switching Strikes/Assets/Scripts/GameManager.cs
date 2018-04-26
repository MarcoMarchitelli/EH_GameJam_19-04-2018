using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Transform attackLimitPrefab;

    PlayerController player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        //spawn limits
        Transform attackLimitLeft = Instantiate(attackLimitPrefab, new Vector3(-player.AttackRange, 0f, 0f), Quaternion.identity);
        Transform attackLimitRight = Instantiate(attackLimitPrefab, new Vector3(player.AttackRange, 0f, 0f), Quaternion.identity);
        Transform attackLimitTop = Instantiate(attackLimitPrefab, new Vector3(0f, 0f, player.AttackRange), Quaternion.Euler(Vector3.up * 90));
        Transform attackLimitBottom = Instantiate(attackLimitPrefab, new Vector3(0f, 0f, -player.AttackRange), Quaternion.Euler(Vector3.up * 90));

        Vector3 scale = new Vector3(attackLimitPrefab.localScale.x, attackLimitPrefab.localScale.y, player.AttackRange * 2);

        //set limits lenght
        attackLimitLeft.localScale = scale;
        attackLimitRight.localScale = scale;
        attackLimitTop.localScale = scale;
        attackLimitBottom.localScale = scale;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
