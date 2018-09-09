using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Color")]
    public ColorType color;

    float speed;

    Animator animator;

    IEnumerator attackAnimation;

    Collider collider;

    private void Start()
    {
        speed = SpawnManager.instance.enemiesSpeed;
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
        attackAnimation = AttackAnimation(new Vector3(0, transform.position.y, 0), speed);
        StartCoroutine(attackAnimation);
    }

    IEnumerator AttackAnimation(Vector3 destination, float speed)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator DeathAnimation(Vector3 destination, float speed = 3f)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Die()
    {
        if(SpawnManager.instance.enemiesAlive > 0)
            SpawnManager.instance.enemiesAlive--;
        collider.enabled = false;
        animator.SetTrigger("Death");
        if(attackAnimation != null)
            StopCoroutine(attackAnimation);
        StartCoroutine(DeathAnimation(transform.position + Vector3.up * -7f));
    }

}
