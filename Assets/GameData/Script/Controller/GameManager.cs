using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;


    [Header("Other")]
    [SerializeField] internal AudioManager audioManager;
    [SerializeField] internal UiManager uiManager;
    [SerializeField] internal Transform activeBalls;
    [SerializeField] internal Transform deactiveBalls;




    [Header("Effects")]
    [SerializeField] internal GameObject textPerfect;

    [Header("BallData")]
    [SerializeField] internal bool isCandy = false;



    [Header("LevelsSystem")]
    [SerializeField] internal LevelManager[] levelManamger;

    internal FirstHandControll firstHandControll;
    internal SecondHandControll secondHandControll;

    internal int totalBalls = 0;

    internal float totalStar = 0;
    internal int totalBallInHand = 0;
    internal int MaxScoreInLevel = 0;
    internal int totalFinalStageBall = 0;
    internal bool isGameOver = false;
    internal bool isGameStart = true;
    internal bool isGameWin = false;
    internal bool isVibrationPlay = false;
    public Material[] matrCubes;
    #endregion Variables
    #region ReturnVariables
    public int currentLevel
    {
        get { return PlayerPrefs.GetInt("currentLevel"); }
        set { PlayerPrefs.SetInt("currentLevel", value); }
    }
    public int subLevel
    {
        get { return PlayerPrefs.GetInt("subLevel", 0); }
        set { PlayerPrefs.SetInt("subLevel", value); }
    }
    public int chestPercentage
    {
        get { return PlayerPrefs.GetInt("chestPercentage"); }
        set { PlayerPrefs.SetInt("chestPercentage", value); }
    }
    public int totalStarInGame
    {
        get { return PlayerPrefs.GetInt("totalStarInGame"); }
        set { PlayerPrefs.SetInt("totalStarInGame", value); }
    }
    public bool sound
    {
        get { return (PlayerPrefs.GetString("Sound", "On") == "On") ? true : false; }
        set { if (value) PlayerPrefs.SetString("Sound", "On"); else PlayerPrefs.SetString("Sound", "Off"); }
    }
    public bool vibration
    {
        get { return (PlayerPrefs.GetString("vibration", "On") == "On") ? true : false; }
        set { if (value) PlayerPrefs.SetString("vibration", "On"); else PlayerPrefs.SetString("vibration", "Off"); }
    }
    public int offlineReward
    {
        get
        {
            return PlayerPrefs.GetInt("HealthPlayer");
        }
        set
        {
            PlayerPrefs.SetInt("HealthPlayer", value);
        }

    }
    public int PowerPlayer
    {
        get
        {
            return PlayerPrefs.GetInt("PowerPlayer");
        }
        set
        {
            PlayerPrefs.SetInt("PowerPlayer", value);
        }

    }



    #endregion ReturnVariables
    #region UnityFunction
    private void Awake()
    {
        Instance = this;
        Initilize();

    }
    private void Start()
    {
        GlobalAudioPlayer.PlayMusic("Music");


    }
    #endregion UnityFunction
    #region CustomFunction
    private void Update()
    {

        //if (firstHandControll != null)
        //    Debug.Log("FirstHand");
        //if (secondHandControll != null)
        //    Debug.Log("SecondHand");
        if (Input.GetKeyDown(KeyCode.A))
            Time.timeScale = 0f;
    }
    private void Initilize()
    {

        totalBalls = 0;
        // MaxScoreInLevel = levelManamger[currentLevel % levelManamger.Length].MaxScoreInLevel[subLevel % 3];
        MaxScoreInLevel = 10000;
        Instantiate(levelManamger[currentLevel % levelManamger.Length].levelPreb[subLevel % 3]);

        Camera.main.orthographicSize = 3.7f / Screen.width * Screen.height;
    }
    public void GameComplete()
    {
        if (isGameOver)
            return;
        //GlobalAudioPlayer.Play("GameComplete");
        isGameWin = true;
        isGameOver = true;


        StartCoroutine(uiManager.GameComplete(4f));
    }
    public void GameOver()
    {
        if (isGameOver)
            return;
        StartCoroutine(uiManager.GameOver(6f));
    }
    public void VibrationPhone()
    {
        if (!vibration)
            return;


        if (isVibrationPlay == false)
        {
            //  Debug.Log("Vibration");
            isVibrationPlay = true;
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            Invoke("resetVibration", 0.2f);
        }

        //#if UNITY_ANDROID
        //        and
        //#endif

        //#if UNITY_IPHONE
        //       //  Vibration.VibratePop();
        //#endif
    }

    void resetVibration()
    {
        isVibrationPlay = false;
    }

    public void VideoReward()
    {
        firstHandControll.VideoReward();
        PlaySound("HandEffect");
    }
    public void SetLevelAndReplay(int levelnumber)
    {
        currentLevel = levelnumber;
        subLevel = 0;
        SceneManager.LoadScene(0);
    }

    public void PlayBallSound()
    {
        if (isplaysoundBounce)
            return;
        isplaysoundBounce = true;
        PlaySound("Bounce");
        Invoke("resetSound", 0.1f);
    }
    bool isplaysoundBounce = false;

    void resetSound()
    {
        isplaysoundBounce = false;
    }
    #endregion CustomFunction
    #region ButtonsEvents
    public void PlaySound(string name)
    {
        GlobalAudioPlayer.Play(name);
    }
    public void Replay()
    {
        subLevel++;
        SceneManager.LoadScene(0);
    }
    public void Next()
    {
        currentLevel++;
        subLevel = 0;
        SceneManager.LoadScene(0);
    }
    public void DoTweenRestart(string value)
    {
        DOTween.Restart(value);
    }

    public void TaptoPlay()
    {
        firstHandControll.isDrag = true;
        isGameStart = true;
    }
    #endregion ButtonsEvents

}
