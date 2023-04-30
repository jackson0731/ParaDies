using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{

    private GameObject Player;
    private GameObject Enemy;

    public float speed = 0.5f;
    private Vector3 direction;
    private bool playerdead = false;

    private float attackTimer = 1.2f;
    public float attackCooldown = 2.0f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walking", true);
        Enemy = gameObject;
        Player = GameObject.Find("Player");
        direction = (Player.transform.position - Enemy.transform.position).normalized;
        direction.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        transform.position += direction * speed * Time.deltaTime;
        if (GameObject.Find("Player").GetComponent<Shoot>().HP == 0)
        {
            playerdead = true;
        }
        if (animator.GetBool("Walking") == false)
        {
            speed = 0f;
        }
        else
        {
            speed = 1.2f;
        }
        if (animator.GetBool("Death") == true)
        {
            StartCoroutine(WaitDead());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (animator.GetBool("Death") == false)
        {
            animator.SetBool("Walking", false);
            if (playerdead == false)
            {
                if (attackTimer <= 0)
                {
                    animator.SetBool("Attack", true);
                    if (animator.GetBool("Attack") == true)
                    {
                        StartCoroutine(WaitAttacked());
                    }
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }
            else
            {
                Debug.Log("Player Dead");
            }
            if (other.CompareTag("Player"))
            {
                speed = 0f;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    IEnumerator WaitAttacked()
    {
        animator = GetComponent<Animator>();
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        while (currentState.normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Attack Player");
        GameObject.Find("Player").GetComponent<Shoot>().HP--;
        attackTimer = attackCooldown;
        animator.SetBool("Attack", false);
        yield break;
    }
    IEnumerator WaitDead()
    {
        animator = GetComponent<Animator>();
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        while (currentState.normalizedTime < 1.0f)
        {
            yield return null;
        }
        GameObject.Find("Player").GetComponent<Shoot>().animEnd = true;
        yield break;
    }
}
