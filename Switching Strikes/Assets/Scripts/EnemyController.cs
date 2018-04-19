using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Color")]
    public ColorType color;

    float speed;

    private void Start()
    {
        speed = SpawnManager.instance.enemiesSpeed;
        StartCoroutine(AttackAnimation(new Vector3(0, transform.position.y, 0), speed));
    }

    IEnumerator AttackAnimation(Vector3 destination, float speed)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void Die()
    {
        if(SpawnManager.instance.enemiesAlive > 0)
            SpawnManager.instance.enemiesAlive--;
        Destroy(gameObject);
    }

}
