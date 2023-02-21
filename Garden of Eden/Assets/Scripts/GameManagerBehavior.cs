using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerBehavior : MonoBehaviour
{
    public string labelText = "Collect all the items and win your freedom!";
    public int maxItems = 3;
    public static GameManagerBehavior instance;

    public GameObject pausescreen;
    public float sensitivity = 1f;
    private bool showLossScreen = false;

    private int _itemsCollected = 0;
    private bool Blue = false;
    private bool Yellow = false;
    private bool Red = false;
    private bool win = false;
    private double dashtime = 0.0;
    private int bullets = 8;
    private double loadtime = 0.0;
    private bool pause = false;
    private float chargecd = 0f;
    private float cdmultiple = 1f;
    public float counterCD = 5f;
    public float countertime = 0.0f;

    private int _enemies = 0;

    public int EnemyCount 
    {
        get { return _enemies;}
        set { 
            _enemies = value;
            Debug.Log("Enemies:" + _enemies);
        }
    }

    public float cdMultiplier
    {
        get { return cdmultiple; }
        set { cdmultiple = value; }
    }

    public double reloadTime
    {
        get { return 2.5; }
    }
    public double reloadTimer
    {
        get { return loadtime; }
        set { loadtime = value; }
    }

    public float chargeTime
    {
        get { return 1f; }
    }
    public float chargeTimer
    {
        get { return chargecd; }
        set { chargecd = value; }
    }

    public double dashCooldown
    {
        get { return 3.0; }
    }

    public double dashTimer
    {
        get { return dashtime; }

        set { dashtime = value; }
    }

    public int Magazine
    {
        get { return bullets; }
        set
        {
            bullets = value;
            if (bullets == 0)
            {
                reloadTimer = Time.timeAsDouble;
            }
        }
    }

    public bool showWinScreen
    {
        get { return win; }
        set { win = value; }
    }
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the Artifacts! Return to the start to escape!";
            }
            else
            {
                labelText = "Artifacts Found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    public bool RedArtifact
    {
        get { return Red; }
        set
        {
            Red = value;
        }
    }

    public bool BlueArtifact
    {
        get { return Blue; }
        set
        {
            Blue = value;
        }
    }

    public bool YellowArtifact
    {
        get { return Yellow; }
        set
        {
            Yellow = value;
        }
    }
    private int _playerHP = 10;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);

            if(_playerHP <= 0) {
                showLossScreen = true;
                Time.timeScale = 0.0f;
            }
        }
    }




    void OnGUI()
    {
        if (pause == true)
        {
            /* GUI.Box(new Rect(0,0,Screen.width, Screen.height)," ");
            GUI.Label(new Rect(Screen.width / 2-25, Screen.height/2, 300, 500), "Game Paused"); */
        } 
        else
        {
            GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);

            GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);
            if (RedArtifact)
            {
                if (bullets == 0)
                {
                    GUI.Box(new Rect(20, 500, 250, 25), "Reloading...");
                }
                else { GUI.Box(new Rect(20, 500, 250, 25), "Left Click to Shoot! (" + bullets + "/8)"); }
            }
            if (BlueArtifact)
            {
                if (Time.time < countertime + counterCD)
                    GUI.Box(new Rect(20, 560, 250, 25), "Parry on cooldown for " + (System.Math.Round(counterCD + countertime - Time.time, 2)) + "s");
                else
                    GUI.Box(new Rect(20, 560, 250, 25), "Press E to Parry an Attack!");
            }
            if (YellowArtifact)
            {
                if (Time.time < chargeTimer + 3 + chargeTime * cdMultiplier)
                {
                    GUI.Box(new Rect(20, 530, 250, 25), "Bash on cooldown for " + (System.Math.Round(chargeTimer + chargeTime * cdMultiplier - Time.time, 2) + 3) + "s");
                }
                else { GUI.Box(new Rect(20, 530, 250, 25), "Hold RClick to charge a Bash!"); }
            }
            if (YellowArtifact && RedArtifact && BlueArtifact)
            {
                if (Time.time < dashtime + dashCooldown)
                {
                    GUI.Box(new Rect(20, 590, 250, 25), "Dash on cooldown for " + (System.Math.Round(dashtime + dashCooldown - Time.timeAsDouble, 2)) + "s");
                }
                else { GUI.Box(new Rect(20, 590, 250, 25), "Press LShift to dash through walls!"); }
            }

            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 500), labelText);

            if (showWinScreen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
                {
                    SceneManager.LoadScene(0);

                    Time.timeScale = 1.0f;
                }
            }

            if (showLossScreen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Kinda Suck at This"))
                {
                    SceneManager.LoadScene(0);

                    Time.timeScale = 1.0f;
                }
            }
        }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pausescreen = GameObject.Find("PauseMenu");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausescreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && showWinScreen == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            pause = true;
            pausescreen.SetActive(true);
        }
    }

    public void returnbutton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        pause = false;
        pausescreen.SetActive(false);
    }

    public void senschange(TMP_InputField playertextinput)
    {
        float i = 1;
        bool test = float.TryParse(playertextinput.text, out i);

        if (test)
        {
            sensitivity = i;
        }
        else
        {
            return;
        }

    }
}
