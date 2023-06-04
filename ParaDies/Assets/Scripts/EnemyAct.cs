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
    private bool walk = true;

    private float attackTimer = 1.2f;
    public float attackCooldown = 2.0f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Attack", false);
        Enemy = gameObject;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Death") == true)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attack", false);
        }

        if(gameObject.activeSelf && walk == true && animator.GetBool("Death") == false)
        {
            animator.SetBool("Walking", true);
            direction = (Player.transform.position - Enemy.transform.position).normalized;
            direction.y = 0f;

            Vector3 lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime);
        }
        
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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            walk = false;
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
    private void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    private void Drop()
    {
        animator.SetBool("DropEnd", true);
    }
    private void Intro()
    {
        animator.SetBool("IntroEnd", true);
    }
}
