﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

public class Shop : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;

    public GameObject Text;
    public Text text;
    public GameObject shop;

    public GameObject CYF;
    public GameObject Next;

    private int i = 0;
    private bool endChat;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        Text.SetActive(false);
        Next.SetActive(false);
        shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);
            if (wiimote.Button.a && buttonPressed == false && !shop.activeSelf)
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
            text.text = "我：你該回答我的問題了！";
        }
        if (i == 2)
        {
            text.text = "NPC：不不不，你只是有資格而已，我不會免費爲你提供幫助。";
        }
        if (i == 3)
        {
            shop.SetActive(true);
            text.text = "NPC：看吧明碼標價了，相信這些付出你不會讓你後悔的。";
        }
        if (i == 4)
        {
            text.text = "NPC：很好，看來我們建立了基礎的信任，給你一個福利吧，來到二樓尋找我吧，去二樓的入口在另一邊，相信我，你不會想從這邊上去的。";
        }
        if (i == 5)
        {
            text.text = "我：爲什麽我要聼你的？";
        }
        if (i == 6)
        {
            text.text = "NPC：你可以不聽，但是你同伴的下落和回去的路我就會忘記了，比較沒用的東西我不會記得，祝好運。";
        }
        if (i == 7)
        {
            text.text = "我：你......！";
            CYF.SetActive(false);
        }
        if (i == 8)
        {
            text.text = "我：可惡！";
        }
        if (i == 9)
        {
            Text.SetActive(false);
            endChat = true;
        }

        if (gameObject.GetComponent<Shoot>().Moved3 == true)
        {
            Text.SetActive(true);
            Next.SetActive(true);
        }
    }
}
