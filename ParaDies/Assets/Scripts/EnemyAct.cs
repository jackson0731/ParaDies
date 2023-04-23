using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{

    private GameObject Player;
    private GameObject Enemy;

    public float speed = 1f;
    private Vector3 direction;
    private bool playerdead = false;

    public int playerHP = 5;

    private float attackTimer = 0.0f;
    public float attackCooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject;
        Player = GameObject.Find("Player");
        direction = (Player.transform.position - Enemy.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (playerHP == 0)
        {
            playerdead = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playerdead == false)
        {
            if (attackTimer <= 0)
            {
                Debug.Log("Attack Player");
                playerHP--;
                attackTimer = attackCooldown; // 設定攻擊冷卻時間
            }
            else
            {
                attackTimer -= Time.deltaTime; // 減少計時器的值
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
