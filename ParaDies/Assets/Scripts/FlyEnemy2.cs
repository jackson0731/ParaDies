using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy2 : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;

    public float speed = 2f;
    private Vector3 direction;
    private bool playerdead = false;

    public float attackTimer = 5f;
    public bool canShoot = false;
    public Animator animator;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float distance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        int randomNumber = Random.Range(1, 10);
        if(randomNumber >= 4)
        {
            gameObject.SetActive(false);
        }
        animator.SetBool("Charge", false);
        Enemy = gameObject;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && animator.GetBool("Charge") == true)
        {
            Vector3 lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos*-1);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime);
        }

        if (GameObject.Find("Player").GetComponent<Shoot>().HP == 0)
        {
            playerdead = true;
        }

        if (playerdead == false)
        {
            if (attackTimer <= 0)
            {
                animator.SetBool("Fire", true);
                animator.SetBool("Charge", false);
                canShoot = true;
            }
            else
            {
                animator.SetBool("Charge", true);
                attackTimer -= Time.deltaTime;
            }
            if (canShoot == true)
            {
                // 创建子弹实例
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                // 设置子弹的速度和方向
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                Vector3 direction = (Player.transform.position - bulletSpawnPoint.position).normalized;
                bulletRb.velocity = direction * speed;
                // 重置蓄力计时和发射状态
                attackTimer = 5f;
                canShoot = false;
                animator.SetBool("Fire", false);
            }
        }
    }
}
