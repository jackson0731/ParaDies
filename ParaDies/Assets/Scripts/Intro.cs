using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class Intro : MonoBehaviour
{
    private Wiimote wiimote;
    private bool buttonPressed = false;

    public GameObject FadeIn;
    public GameObject[] Intro1;
    public GameObject[] Intro2;
    public GameObject[] Intro3;
    private bool Intro1End = false;
    private bool Intro2End = false;
    private bool Intro3End = false;
    public int m = 1;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        FadeIn.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);

            if (wiimote.Button.a && buttonPressed == false)
            {
                buttonPressed = true;
                if (Intro1End == false && m <= Intro1.Length)
                {
                    Intro1[m].SetActive(true);
                    m++;
                }
                if (m >= Intro1.Length)
                {
                    Intro1End = true;
                    m = 0;
                }
                if (Intro2End == false && Intro1End == true && m <= Intro2.Length)
                {
                    Intro2[m].SetActive(true);
                    m++;
                }
                if (m >= Intro2.Length && Intro1End == true)
                {
                    Intro2End = true;
                    m = 0;
                }
                if (Intro3End == false && Intro2End == true && m <= Intro3.Length)
                {
                    Intro3[m].SetActive(true);
                    m++;
                }
                if (m >= Intro3.Length && Intro2End == true)
                {
                    Intro3End = true;
                }
            }

            if (!wiimote.Button.a && !wiimote.Button.plus && !wiimote.Button.minus)
            {
                buttonPressed = false;
            }
        }


        if (Intro1End == true)
        {
            for (int i = 0; i < Intro1.Length; i++)
            {
                Intro1[i].SetActive(false);
            }
        }
        if (Intro2End == true)
        {
            for (int i = 0; i < Intro2.Length; i++)
            {
                Intro2[i].SetActive(false);
            }
        }
        if (Intro3End == true)
        {
            SceneManager.LoadScene("Intro 2");
        }
    }
}
