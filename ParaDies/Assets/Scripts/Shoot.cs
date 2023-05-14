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

    public int HP = 3;

    public RectTransform uiElement;
    public LayerMask layerMask;

    private Animation anim;
    public bool Moved;
    public bool Moved2;
    public bool Moved3;

    public int Level = 1;

    public bool animEnd = false;
    private GameObject ShootedEnemy;

    public Text hptext;
    public GameObject Deadtext;

    public GameObject End;

    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;

        GameObject.Find("Level1").SetActive(true);
        GameObject.Find("Level2").SetActive(false);
        GameObject.Find("Level3").SetActive(false);
        End.SetActive(false);

        anim = gameObject.GetComponent<Animation>();
        foreach (AnimationClip clip in anim)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.time = clip.length;  // �b�ʵe������Ĳ�o�ƥ�
            animationEvent.functionName = "OnAnimationEnd";
            clip.AddEvent(animationEvent);
        }
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
                    hit.collider.gameObject.GetComponent<EnemyAct>().animator.SetBool("Walking", false);
                    ShootedEnemy = hit.collider.gameObject;
                    if(ShootedEnemy.GetComponent<EnemyAct>().animator.GetBool("Walking") == false)
                    {
                        hit.collider.gameObject.GetComponent<EnemyAct>().animator.SetBool("Death", true);
                    }
                }
            }
            if (animEnd == true)
            {
                ShootedEnemy.SetActive(false);
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
                
            }
        }
        if (Level == 2)
        {
            GameObject.Find("Level2m").transform.GetChild(0).gameObject.SetActive(true);
            if (GameObject.Find("Level2").GetComponent<Move2>().AllEnemyDead2 == true)
            {
                anim.Play("Move2");
            }
        }
        if (Level == 3)
        {
            GameObject.Find("Level3m").transform.GetChild(0).gameObject.SetActive(true);
            if (GameObject.Find("Level3").GetComponent<Move3>().AllEnemyDead3 == true)
            {
                anim.Play("Move3");
                Level++;
            }
        }
        if (Level == 4)
        {
            End.SetActive(true);
        }
        hptext.text = "" + HP;
        if(HP == 0)
        {
            Deadtext.SetActive(true);
        }
    }

    private void OnAnimationEnd()
    {
        if (Level == 1)
        {
            Moved = true;
            Level++;
        }else if (Level == 2)
        {
            Moved2 = true;
            Level++;
        }
        else if (Level == 3)
        {
            Moved3 = true;
        }
    }
}
