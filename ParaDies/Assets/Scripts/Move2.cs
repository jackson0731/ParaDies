using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;

    public bool AllEnemyDead2;

    // Start is called before the first frame update
    void Start()
    {
        enemy1 = gameObject.transform.GetChild(0).gameObject;
        enemy2 = gameObject.transform.GetChild(1).gameObject;
        enemy3 = gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy1.activeSelf && !enemy2.activeSelf && !enemy3.activeSelf && GameObject.Find("Player").GetComponent<Shoot>().Moved2 == false)
        {
            AllEnemyDead2 = true;
        }
        else
        {
            AllEnemyDead2 = false;
        }
    }
}
