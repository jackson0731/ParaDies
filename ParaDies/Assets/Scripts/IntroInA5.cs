using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class IntroInA5 : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;

    public GameObject FadeIn;
    public GameObject Message;
    public GameObject Text;
    public Text text;
    public GameObject CYF;

    private int i = 0;

    public bool endIntro = false;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        FadeIn.SetActive(true);
        Message.SetActive(true);
        Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);
            if (wiimote.Button.a && buttonPressed == false && i == 0)
            {
                buttonPressed = true;
                i++;
            }
            if (wiimote.Button.a && buttonPressed == false && i == 1)
            {
                buttonPressed = true;
                Message.SetActive(false);
                Text.SetActive(true);
                i++;
            }
            if(wiimote.Button.a && buttonPressed == false && i >= 1)
            {
                buttonPressed = true;
                i++;
            }
            if (!wiimote.Button.a)
            {
                buttonPressed = false;
            }
        }
        if(i == 3)
        {
            text.text = "NPC：這裏是裏世界，歡迎你迷途的羔羊。";
        }
        if (i == 4)
        {
            text.text = "我：XXX老師？你爲什麽會在這裏？";
        }
        if (i == 5)
        {
            text.text = "NPC：我不是你認識的那個人，我只是用你熟悉的形象出現而已，首先聲明我不會白白幫助你，也不會回答你的問題。";
        }
        if (i == 6)
        {
            text.text = "我：什麽？抱歉我現在有點混亂。";
        }
        if (i == 7)
        {
            text.text = "NPC：算了，你拿著這個到另一邊來找我，證明你不是廢物，我們再繼續進一步交流。";
        }
        if (i == 8)
        {
            text.text = "我：等一下！";
        }
        if (i == 9)
        {
            text.text = "NPC：......祝好運。";
            CYF.SetActive(false);
        }
        if (i == 10)
        {
            text.text = "我：......沒辦法了。";
        }
        if (i == 11)
        {
            Text.SetActive(true);
            endIntro = true;
        }
    }
}
