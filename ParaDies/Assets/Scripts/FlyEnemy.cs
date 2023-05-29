using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;

    public float speed = 0.5f;
    private Vector3 direction;
    private bool playerdead = false;

    public float attackTimer = 1.2f;
    public float attackCooldown = 1.0f;
    public Animator animator;

    public GameObject objectToSpawn;
    public float distance = 1f;
    private bool Spawned;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Charge", false);
        Enemy = gameObject;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && animator.GetBool("Charge") == true && Spawned == false)
        {
            Vector3 lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos*-1);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime);

            // 計算生成位置
            Vector3 spawnPosition = transform.position + transform.forward * distance;

            // 生成物件
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            Spawned = true;
        }

        if (GameObject.Find("Player").GetComponent<Shoot>().HP == 0)
        {
            playerdead = true;
        }

        if (playerdead == false)
        {
            if (attackTimer <= 0)
            {
                animator.SetBool("Charge", true);
                if(Spawned == true)
                {
                    if (GameObject.Find("Sphere(Clone)").GetComponent<EnemyAmo>().ChargeOK == true)
                    {
                        animator.SetBool("Fire", true);
                        attackTimer = attackCooldown;
                        animator.SetBool("Charge", false);
                        Spawned = false;
                    }
                }
                if (animator.GetBool("Fire") == true)
                {
                    direction = (Player.transform.position - GameObject.Find("Sphere(Clone)").transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
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
        direction = (Player.transform.position - GameObject.Find("Sphere").transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
