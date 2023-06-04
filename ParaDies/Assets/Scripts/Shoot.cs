using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using WiimoteApi;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    private GameObject[] GameObjects;

    private Wiimote wiimote;
    private bool buttonPressed = false;
    public Camera mainCamera;

    public int HP = 3;
    public int MAXHP = 3;
    public int MAXAmo = 15;
    public int Amo = 15;
    public int Money = 0;

    public RectTransform uiElement;
    public LayerMask layerMask;

    public Animation anim;
    public bool Moved0 = false;
    public bool Moved = false;
    public bool Moved2 = false;
    public bool Moved3 = false;

    public int Level = 0;

    public bool animEnd = false;
    private GameObject ShootedEnemy;

    public Text hptext;
    public Text Amotext;
    public Text Moneytext;
    public GameObject hpAmo;
    public GameObject Deadtext;
    public bool ChatEnd = false;

    public GameObject End;

    void Start()
    {
        GameObjects = GameObject.FindGameObjectsWithTag("enemy");

        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
        wiimote.SendPlayerLED(true, false, false, false);

        GameObject.Find("Dot 5").GetComponent<Image>().color = Color.green;

        GameObject.Find("Level1").SetActive(false);
        GameObject.Find("Level2").SetActive(false);
        GameObject.Find("Level3").SetActive(false);
        End.SetActive(false);
        hpAmo.SetActive(false);

        anim = gameObject.GetComponent<Animation>();
        foreach (AnimationClip clip in anim)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.time = clip.length;  // 在動畫結束時觸發事件
            animationEvent.functionName = "OnAnimationEnd";
            clip.AddEvent(animationEvent);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObjects[0].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[1].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[2].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[3].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[4].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[5].GetComponent<EnemyAct>().animator.SetBool("Death", true);
            GameObjects[6].GetComponent<EnemyAct>().animator.SetBool("Death", true);
        }

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
                    ShootedEnemy = hit.collider.gameObject;
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))
                    {
                        Money += 1;
                        hit.collider.gameObject.GetComponent<EnemyAct>().animator.SetBool("Walking", false);
                        if (ShootedEnemy.GetComponent<EnemyAct>().animator.GetBool("Walking") == false)
                        {
                            hit.collider.gameObject.GetComponent<EnemyAct>().animator.SetBool("Death", true);
                        }
                    }
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("FlyEnemy"))
                    {
                        Money += 1;
                        ShootedEnemy.SetActive(false);
                    }
                }
            }
            if (wiimote.Button.b && !buttonPressed)
            {
                buttonPressed = true;
                Amotext.text = "" + MAXAmo; 
            }
            if (animEnd == true)
            {
                ShootedEnemy.SetActive(false);
            }
            if (!wiimote.Button.a && !wiimote.Button.b && !wiimote.Button.plus && !wiimote.Button.minus)
            {
                buttonPressed = false;
            }

            if (gameObject.GetComponent<Shop>().shop.activeSelf)
            {
                Moneytext.text = "$：" + Money;
                if (wiimote.Button.b && !buttonPressed)
                {
                    buttonPressed = true;
                    gameObject.GetComponent<Shop>().shop.SetActive(false);
                }
                if (wiimote.Button.plus && !buttonPressed && Money >= 5)
                {
                    buttonPressed = true;
                    MAXHP++;
                    HP = MAXHP;
                    Money -= 5;
                }
                if (wiimote.Button.minus && !buttonPressed && Money >= 3)
                {
                    buttonPressed = true;
                    MAXAmo += 5;
                    Amo = MAXAmo;
                    Money -= 3;
                }
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

        if(Level == 0 && gameObject.GetComponent<IntroInA5>().endIntro == true)
        {
            anim.Play("Move0");
        }
        if (Level == 1)
        {
            GameObject.Find("Level1m").transform.GetChild(0).gameObject.SetActive(true);
            hpAmo.SetActive(true);
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
        if (Level == 0)
        {
            Moved0 = true;
            Level++;
        }
        else if (Level == 1)
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
            Level++;
        }
        else if (Level == 4)
        {
            SceneManager.LoadScene("2F");
        }
    }
}
