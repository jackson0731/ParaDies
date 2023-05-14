using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class Pause : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;
    public Camera mainCamera;

    public bool isPause = false;
    public GameObject PauseWindow;

    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        PauseWindow.SetActive(false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote.Button.plus && !buttonPressed && !PauseWindow.activeSelf)
        {
            buttonPressed = true;
            Time.timeScale = 0f;
            PauseWindow.SetActive(true);
        }
        if(wiimote.Button.a && !buttonPressed && PauseWindow.activeSelf)
        {
            buttonPressed = true;
            Time.timeScale = 1f;
            PauseWindow.SetActive(false);
        }
        if (wiimote.Button.plus && !buttonPressed && PauseWindow.activeSelf)
        {
            buttonPressed = true;
            Debug.Log("Setting");
        }
        if (wiimote.Button.minus && !buttonPressed && PauseWindow.activeSelf)
        {
            buttonPressed = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
        if(!wiimote.Button.plus && !wiimote.Button.a && !wiimote.Button.minus)
        {
            buttonPressed = false;
        }
    }
}
