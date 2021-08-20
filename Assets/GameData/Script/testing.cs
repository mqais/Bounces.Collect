using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour
{

    public Transform datasampel;

    public float delay = 0.1f;
    public int yourPositionInWorld = 10;

    public Text[] textRank;
    public Text[] textName;
    public Text textLevelNumber;
    public Text textScorePlayer;
    public Text textyou;
    public Text textRankPlayer;


    public Text textGameScore;
    public Text textBestScore;

    public Transform TransparentPanel;
    public Transform TransparentPanel2;
    public Transform YourPanel;
    public Transform BallsImageParent;
    public Transform YourPanelPositon;
    public Transform ParentPanel;
    public Transform[] star;
    public Transform StarsImageParent;
    public Transform StarsTarget;
    public Image[] imageFlag;


    public string[] names;
    public Sprite[] flages;

    public int currentRank
    {
        get
        {
            return PlayerPrefs.GetInt("currentRank", 10000);
        }
        set
        {
            PlayerPrefs.SetInt("currentRank", value);
        }

    }

    public int currentScore
    {
        get
        {
            return PlayerPrefs.GetInt("currentScore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("currentScore", value);
        }

    }

    public int bestScore
    {
        get
        {
            return PlayerPrefs.GetInt("bestScore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("bestScore", value);
        }

    }

    public int PlayerScore
    {
        get
        {
            return PlayerPrefs.GetInt("PlayerScore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("PlayerScore", value);
        }

    }
    public int totalStar
    {
        get
        {
            return PlayerPrefs.GetInt("totalStar", 0);
        }
        set
        {
            PlayerPrefs.SetInt("totalStar", value);
        }

    }
    int currentRanKinWorld = 0;

    int indexstar = 0;
    void ShowStar()
    {
        star[indexstar].gameObject.SetActive(true);
        StarsImageParent.GetChild(indexstar).GetComponent<StarMove>().startingPosition = star[indexstar].transform.position;
        StarsImageParent.GetChild(indexstar).GetComponent<StarMove>().EndingPosition = StarsTarget.transform.position;
        StarsImageParent.GetChild(indexstar).gameObject.SetActive(true);
        indexstar++;
        GameManager.Instance.PlaySound("Star1");
    }
    // Start is called before the first frame update
    void Start()
    {

        textLevelNumber.text = "Level  " + (GameManager.Instance.currentLevel + 1);
        yourPositionInWorld = (int)(Mathf.Clamp(GameManager.Instance.totalFinalStageBall * 70 / GameManager.Instance.MaxScoreInLevel, 5, 70));
        //yourPositionInWorld = 30;
        initialize();

        //GameManager.Instance.totalFinalStageBall = 250;


        textGameScore.text = GameManager.Instance.totalFinalStageBall.ToString();

        if (bestScore < GameManager.Instance.totalFinalStageBall)
        {
            bestScore = GameManager.Instance.totalFinalStageBall;
        }
        textBestScore.text = bestScore.ToString();


        int totalstar = (int)Mathf.Ceil(GameManager.Instance.totalStar);


        GameManager.Instance.uiManager.AddStar(totalstar);


        for (int i = 0; i < totalstar; i++)
        {
            Invoke("ShowStar", 0.4f * (i + 1));
        }


        Invoke("StartScoring", 0.35f * totalstar);


    }

    void StartScoring()
    {
        int positioninchildRank = 92 - yourPositionInWorld;
        RectTransform rect = gameObject.GetComponent<RectTransform>();


        YourPanel.parent = ParentPanel;
        YourPanel.localPosition = YourPanelPositon.localPosition;

        textyou.text = "You";

        YourPanel.DOScale(1.1f, 0.5f).OnComplete(() =>
        {
            TransparentPanel.gameObject.SetActive(true);
            int tempValue = currentRanKinWorld;
            DOTween.To(() => tempValue, x => tempValue = x, currentRanKinWorld - 3 - yourPositionInWorld, yourPositionInWorld * 0.1f).OnUpdate(() =>
             {

                 textRankPlayer.text = "  #" + tempValue;

             }).SetEase(Ease.Linear);


            ///////  playerscore to zero
            int temp2 = GameManager.Instance.totalFinalStageBall;
            DOTween.To(() => temp2, x => temp2 = x, 0, yourPositionInWorld * 0.1f).OnUpdate(() =>
             {

                 textGameScore.text = temp2.ToString();

             }).SetEase(Ease.Linear);
            //////////



            int tempValue1 = PlayerScore;
            textScorePlayer.text = PlayerScore.ToString();


            DOTween.To(() => tempValue1, x => tempValue1 = x, PlayerScore + GameManager.Instance.totalFinalStageBall, yourPositionInWorld * 0.1f).OnUpdate(() =>
            {

                textScorePlayer.text = tempValue1.ToString();

            }).OnComplete(() =>
            {

                CancelInvoke("StartMovingBalls");
                PlayerScore = PlayerScore + GameManager.Instance.totalFinalStageBall;

            }).SetEase(Ease.Linear).OnPlay(() =>
            {
                InvokeRepeating("StartMovingBalls", 0.01f, 0.1f);

            });


            float FinalPosition = (8832 - 96f * yourPositionInWorld);
            float positiony = 8832;
            DOTween.To(() => positiony, x => positiony = x, FinalPosition, yourPositionInWorld * 0.1f).OnUpdate(() =>
            {

                var pos = rect.localPosition;

                rect.localPosition = new Vector3(pos.x, positiony, pos.z);

            }).OnComplete(() =>
            {

                TransparentPanel.gameObject.SetActive(false);
                TransparentPanel2.gameObject.SetActive(true);
                TransparentPanel2.SetSiblingIndex(positioninchildRank);
                GameManager.Instance.PlaySound("HandEffect");
                // YourPanel.DOLocalMove(TransparentPanel2.position, 0.25f);
                YourPanel.DOScale(1f, 0.35f).OnComplete(() =>
                {
                    TransparentPanel2.gameObject.SetActive(false);
                    YourPanel.parent = transform;
                    YourPanel.SetSiblingIndex(positioninchildRank);
                    DOTween.Restart("NextButton");
                    setrandagain();


                    /// currentRank=final


                });
            });
        });
        currentRank = currentRank - yourPositionInWorld;


    }
    int ballIndex = 0;
    void StartMovingBalls()
    {
        ballIndex++;

        BallsImageParent.GetChild(ballIndex % BallsImageParent.childCount).GetComponent<CashMove>().startingPosition = startingBallPosition;
        BallsImageParent.GetChild(ballIndex % BallsImageParent.childCount).GetComponent<CashMove>().EndingPosition = textScorePlayer.transform.position;
        BallsImageParent.GetChild(ballIndex % BallsImageParent.childCount).gameObject.SetActive(true);
    }

    void setrandagain()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            textRank[i] = transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        }


        int indexarray = textRank.Length;
        for (int i = 0; i < textRank.Length; i++)
        {
            indexarray--;
            textRank[indexarray].text = "#" + (currentRank - i).ToString();
        }

    }
    Vector3 startingBallPosition = Vector3.zero;
    void initialize()
    {
        textRank = new Text[transform.childCount];
        textName = new Text[transform.childCount];
        imageFlag = new Image[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            textRank[i] = transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            textName[i] = transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
            imageFlag[i] = transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();



        }
        int randromNames = Random.Range(0, names.Length);
        int randromflag = Random.Range(0, flages.Length);
        int indexarray = textRank.Length;
        for (int i = 0; i < textRank.Length; i++)
        {
            indexarray--;
            textRank[indexarray].text = "#" + (currentRank - i).ToString();
            textName[indexarray].text = names[(randromNames + i) % names.Length];
            imageFlag[indexarray].sprite = flages[(randromflag + i) % flages.Length];


            if (textRank[indexarray] == textRankPlayer)
            {
                currentRanKinWorld = currentRank - i;

            }
        }

        startingBallPosition = textGameScore.transform.position;
    }

    int index = 0;

}
