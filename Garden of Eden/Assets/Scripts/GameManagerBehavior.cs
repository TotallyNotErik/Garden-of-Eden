using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerBehavior : MonoBehaviour
{

    // For UI ========================================================================================
    public GameObject holybeam;
    public GameObject pausescreen;
    public GameObject winscreen;
    public GameObject lossscreen;
    public GameObject winscreenclose;
    public GameObject winscreeneasy;
    public GameObject winscreennorm;
    public GameObject minimap;
    public GameObject fullmap;
    public GameObject magtext;
    public GameObject dashUI;
    public GameObject bashUi;
    public GameObject ParryUI;
    public TextMeshProUGUI BulletsText;
    public GameObject artifactDisplay;
    public GameObject hpUI;
    public Color colorRed = new Color(1, 1, 1, 1);
    public Color colorYellow;
    public Color colorBlue;

    private bool pause = false;
    private bool showLossScreen = false;
    public float sensitivity = 1f;
    public int artIndex = 3;

    public static GameManagerBehavior instance;

    private int maptog = 1;
    private int _itemsCollected = 0;
    public int maxItems = 3;
    private bool Blue = false;
    private bool Yellow = false;
    private bool Red = false;
    private bool win = false;
    private bool allitems = false;
    private double dashtime = 0.0;

    private int bullets = 8;
    private double loadtime = 0.0;


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
            if (_itemsCollected >= 3)
                allitems = true;
        }
    }

    public bool RedArtifact
    {
        get { return Red; }
        set
        {
            Red = value;
            magtext.SetActive(true);
            displayArt(colorRed);
        }
    }

    public bool BlueArtifact
    {
        get { return Blue; }
        set
        {
            Blue = value;
            ParryUI.SetActive(true);
            displayArt(colorBlue);
        }
    }

    public bool YellowArtifact
    {
        get { return Yellow; }
        set
        {
            Yellow = value;
            bashUi.SetActive(true);
            bashUi.transform.GetChild(2).gameObject.SetActive(false);
            displayArt(colorYellow);
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

           /* if (_playerHP >= 30)
                hpUI.transform.GetChild(3).gameObject.GetComponent<Image>().fillAmount = 1f;
            else if(_playerHP >= 20 && _playerHP < 30)*/
                hpUI.transform.GetChild(3).gameObject.GetComponent<Image>().fillAmount = ((float)_playerHP - 20f) / 10f;

           /* if (_playerHP >= 20)
                hpUI.transform.GetChild(2).gameObject.GetComponent<Image>().fillAmount = 1f;
            else if (_playerHP >= 10 && _playerHP < 20)*/
                hpUI.transform.GetChild(2).gameObject.GetComponent<Image>().fillAmount = ((float)_playerHP - 10f) / 10f;

           /* if (_playerHP >= 10)
                hpUI.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 1f;
            else if (_playerHP >= 0 && _playerHP < 10) */
                hpUI.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = (float)_playerHP / 10f;

            if (_playerHP < 5)
                hpUI.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(0.8980393f, 0.254902f, 0.145098f, 1);
            else
                hpUI.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(0, 1, 1, 1);

            if (_playerHP <= 0) {
                showLossScreen = true;
                Time.timeScale = 0.0f;
            }
        }
    }




    void OnGUI()
    {
        if (maptog%2 == 0 && !pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            minimap.SetActive(false);
            fullmap.SetActive(true);
        }
        else if (!pause)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            fullmap.SetActive(false);
            minimap.SetActive(true);
        }
        if (pause == true)
        {
            /* GUI.Box(new Rect(0,0,Screen.width, Screen.height)," ");
            GUI.Label(new Rect(Screen.width / 2-25, Screen.height/2, 300, 500), "Game Paused"); */
        } 
        else if (showWinScreen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

                if(_playerHP <=5)
                {
                    winscreenclose.SetActive(true);
                }
  
                if(_playerHP <= 14  && HP > 5)
                {
                winscreennorm.SetActive(true);
                }

                if (_playerHP >= 15)
                {
                winscreeneasy.SetActive(true);
                }

        }
        else if (showLossScreen)
        {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                lossscreen.SetActive(true);
        }
        else
        {

            if (RedArtifact)
            {
                if (bullets == 0)
                    BulletsText.text = "X";
                else
                    BulletsText.text = bullets.ToString();
            }
            if (BlueArtifact)
            {
                if (Time.time < countertime + counterCD)
                {
                    ParryUI.transform.GetChild(1).gameObject.SetActive(true);
                    ParryUI.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 1 - ((Time.time - countertime) / counterCD);
                }
                else
                    ParryUI.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (YellowArtifact)
            {
                if (Time.time < chargeTimer + 3 + chargeTime * cdMultiplier)
                {
                    bashUi.transform.GetChild(1).gameObject.SetActive(true);
                    bashUi.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 1 - ((Time.time - chargeTimer) / (3 + chargeTime * cdMultiplier));
                }
                else
                    bashUi.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (YellowArtifact && RedArtifact && BlueArtifact)
            {
                dashUI.SetActive(true);

                if (Time.time < dashtime + dashCooldown)
                {
                    dashUI.transform.GetChild(1).gameObject.SetActive(true);
                    dashUI.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 1 - ((Time.time - (float)dashtime) / (float)dashCooldown);
                }
                else { dashUI.transform.GetChild(1).gameObject.SetActive(false); }
            }

        }
    }

    public void restart() 
    {
        SceneManager.LoadScene(0);

        Time.timeScale = 1.0f;
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pausescreen = GameObject.Find("PauseMenu");
        holybeam = GameObject.Find("Holy Beam");
        lossscreen = GameObject.Find("LossScreen");
        winscreenclose = GameObject.Find("WinScreenClose");
        winscreennorm = GameObject.Find("WinScreenNorm");
        winscreeneasy = GameObject.Find("WinScreenEasy");
        minimap = GameObject.Find("MiniMap");
        fullmap = GameObject.Find("Full Map");
        magtext = GameObject.Find("MagText");
        dashUI = GameObject.Find("Blink");
        bashUi = GameObject.Find("Bash");
        ParryUI = GameObject.Find("Parry");
        hpUI = GameObject.Find("HealthBar");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausescreen.SetActive(false);
        holybeam.SetActive(false);
        winscreenclose.SetActive(false);
        winscreennorm.SetActive(false);
        winscreeneasy.SetActive(false);
        lossscreen.SetActive(false);
        fullmap.SetActive(false);
        magtext.SetActive(false);
        dashUI.SetActive(false);
        bashUi.SetActive(false);
        ParryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !showWinScreen && !showLossScreen)
            maptog++;

        if (Input.GetKey(KeyCode.Tab) && !showWinScreen && !showLossScreen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            pause = true;
            pausescreen.SetActive(true);
        }

        if(allitems && EnemyCount == 0)
            holybeam.SetActive(true);
            
    }

    public void displayArt(Color color)
    {
        Debug.Log("hi");
        artifactDisplay.transform.GetChild(artIndex).gameObject.GetComponent<Image>().color = color;
        artifactDisplay.transform.GetChild(artIndex).gameObject.transform.localScale += new Vector3(1, 1, 1);
        artIndex++;
    }

    public void exitmap()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fullmap.SetActive(false);
        minimap.SetActive(true);
        maptog++;
    }

    public void returnbutton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        pause = false;
        pausescreen.SetActive(false);
    }

    public void SleaseMode()
    {
         HP = 10000;
         RedArtifact = true;
         BlueArtifact = true;
        YellowArtifact = true;
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
