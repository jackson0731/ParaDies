using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class Click : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;

    public Button play;
    public Button set;
    public Button quit;

    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;
    }

    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);

            if (wiimote.Button.a && !buttonPressed)
            {
                buttonPressed = true;
                play.onClick.Invoke();
            }
            else if (wiimote.Button.plus && !buttonPressed)
            {
                buttonPressed = true;
                set.onClick.Invoke();
            }
            else if (wiimote.Button.minus && !buttonPressed)
            {
                buttonPressed = true;
                quit.onClick.Invoke();
            }

            if (!wiimote.Button.a && !wiimote.Button.plus && !wiimote.Button.minus)
            {
                buttonPressed = false;
            }
        }
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("1F");
    }

    public void OnSetClick()
    {
        Debug.Log("Set");
    }

    public void OnQuitClick()
    {
        Debug.Log("Quit");
    }
}
