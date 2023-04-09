using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{
    private GameObject enemy1;

    public bool AllEnemyDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy1 = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy1.activeSelf && GameObject.Find("Player").GetComponent<Shoot>().Moved == false)
        {
            AllEnemyDead = true;
        }
        else
        {
            AllEnemyDead = false;
        }
    }
}
