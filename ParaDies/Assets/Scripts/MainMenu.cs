using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPointerEnter()
    {
        Debug.Log("AAAAA");
        if (GameObject.Find("Main Camera").GetComponent<Click>().buttonPressed == true)
        {
            SceneManager.LoadScene("1F");
        }
    }
}
