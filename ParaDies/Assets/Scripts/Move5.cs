using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move5 : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject enemy11;
    public GameObject enemy12;
    public GameObject enemy13;
    public GameObject enemy14;

    public bool AllEnemyDead5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy10.activeSelf && !enemy11.activeSelf && !enemy12.activeSelf && !enemy13.activeSelf && !enemy14.activeSelf && !enemy1.activeSelf && !enemy2.activeSelf && !enemy3.activeSelf && !enemy4.activeSelf && !enemy5.activeSelf && !enemy6.activeSelf && !enemy7.activeSelf && !enemy8.activeSelf && !enemy9.activeSelf && GameObject.Find("Player").GetComponent<Shoot>().Moved6 == false)
        {
            AllEnemyDead5 = true;
        }
        else
        {
            AllEnemyDead5 = false;
        }
    }
}
