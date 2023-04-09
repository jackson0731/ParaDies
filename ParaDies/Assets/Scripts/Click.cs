using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class Click : MonoBehaviour
{
    private Wiimote wiimote;
    public bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);

            if (wiimote.Button.a && !buttonPressed)
            {
                buttonPressed = true;
            }
            if (!wiimote.Button.a)
            {
                buttonPressed = false;
            }
        }

        if (!buttonPressed)
        {
            GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;
        }
        else
        {
            GameObject.Find("Dot 5").GetComponent<Image>().color = Color.red;
        }
    }
}
