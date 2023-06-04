using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

public class EndChat : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;

    public GameObject Text;
    public Text text;
    public GameObject FadeOut;
    public GameObject TBC;

    public GameObject Next;

    public int i = 0;
    public bool endChat;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        gameObject.GetComponent<Shop>().enabled = false;
        Text.SetActive(false);
        Next.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);
            if (wiimote.Button.a && buttonPressed == false && gameObject.GetComponent<Shoot>().Moved7 == true)
            {
                buttonPressed = true;
                i++;
            }
            if (!wiimote.Button.a)
            {
                buttonPressed = false;
            }
        }
        if (i == 1)
        {
            text.text = "NPC：當然沒問題我的朋友，只要你證明自己的用處。";
        }
        if (i == 2)
        {
            endChat = true;
        }

        if (gameObject.GetComponent<Shoot>().Moved7 == true && i <= 3)
        {
            Text.SetActive(true);
            Next.SetActive(true);
            gameObject.GetComponent<Shoot>().hpAmo.SetActive(false);
        }else if(i >= 3)
        {
            Text.SetActive(false);
            Next.SetActive(false);
            gameObject.GetComponent<Shoot>().hpAmo.SetActive(true);
        }

        if(endChat == true)
        {
            FadeOut.SetActive(true);
            TBC.SetActive(true);
        }
    }
}
