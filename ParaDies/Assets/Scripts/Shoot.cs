using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using WiimoteApi;

public class Shoot : MonoBehaviour
{

    private Wiimote wiimote;
    private bool buttonPressed = false;
    public Camera mainCamera;

    public RectTransform uiElement;
    public LayerMask layerMask;

    private Animation anim;
    public bool Moved;
    public bool Moved2;
    public bool Moved3;

    public int Level = 1;

    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;

        anim = gameObject.GetComponent<Animation>();

        GameObject.Find("Level1").SetActive(true);
        GameObject.Find("Level2").SetActive(false);
        GameObject.Find("Level3").SetActive(false);
    }

    void Update()
    {
        if (wiimote != null)
        {
            wiimote.SendPlayerLED(true, false, false, false);

            if (wiimote.Button.a && !buttonPressed)
            {
                buttonPressed = true;
                // get the screen position of the UI element
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(uiElement.position.x, uiElement.position.y, 10));

                // create a ray from the screen position
                Ray ray = Camera.main.ScreenPointToRay(uiElement.position);

                // perform a raycast
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    // do something with the hit information
                    hit.collider.gameObject.SetActive(false);
                }
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

        if (Level == 1)
        {
            GameObject.Find("Level1").SetActive(true);
            if (GameObject.Find("Level1").GetComponent<Move1>().AllEnemyDead == true)
            {
                anim.Play("Move1");
                Moved = true;
                Level++;
            }
        }
        if (Level == 2)
        {
            GameObject.Find("Level2m").transform.GetChild(0).gameObject.SetActive(true);
            if (GameObject.Find("Level2").GetComponent<Move2>().AllEnemyDead2 == true)
            {
                anim.Play("Move2");
                Moved2 = true;
                Level++;
            }
        }
        if (Level == 3)
        {
            GameObject.Find("Level3m").transform.GetChild(0).gameObject.SetActive(true);
            if (GameObject.Find("Level3").GetComponent<Move3>().AllEnemyDead3 == true)
            {
                anim.Play("Move3");
                Moved3 = true;
            }
        }
    }
}
