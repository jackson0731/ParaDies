using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move4 : MonoBehaviour
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

    public bool AllEnemyDead4;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy4.activeSelf && !enemy5.activeSelf && !enemy6.activeSelf && !enemy7.activeSelf && !enemy9.activeSelf && !enemy8.activeSelf && !enemy10.activeSelf && !enemy1.activeSelf && !enemy2.activeSelf && !enemy3.activeSelf && GameObject.Find("Player").GetComponent<Shoot>().Moved5 == false)
        {
            AllEnemyDead4 = true;
        }
        else
        {
            AllEnemyDead4 = false;
        }
    }
}
