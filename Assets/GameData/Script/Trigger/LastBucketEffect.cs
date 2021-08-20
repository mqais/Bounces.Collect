using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LastBucketEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int ballDropInBucket;
    [SerializeField] Animator textmeshBall;
    [SerializeField] GameObject particleEffect;
    [SerializeField] Transform basket;
    [SerializeField] GameObject[] activeObjects;
    [SerializeField] GameObject[] deactiveObjects;
    [SerializeField] GameObject confetti;
    [SerializeField] CandyBody candyBoy;


    [Header("Star")]
    [SerializeField] Transform starPanel;
    Image[] whiteStar;
    GameObject[] goldStar;
    bool[] isStarActive;


    GameObject newBall;
    Vector3 cameraposition;
    Vector3 newposition;
    bool isOrdertoDropAllSecondHandBall = false;
    float percentage;
    float y;
    float divsionValueforStar;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        whiteStar = new Image[3];
        goldStar = new GameObject[3];
        isStarActive = new bool[3];
        textmeshBall.GetComponent<TextMesh>().text = ballDropInBucket.ToString();

        divsionValueforStar = GameManager.Instance.MaxScoreInLevel * 0.6f / 3f;

        if (starPanel != null)
        {
            for (int i = 0; i < starPanel.childCount; i++)
            {

                whiteStar[i] = starPanel.GetChild(i).GetChild(0).GetComponent<Image>();
                goldStar[i] = starPanel.GetChild(i).GetChild(0).GetChild(0).gameObject;
            }
        }
    }
    //void Update()
    //{

    //    if (GameManager.Instance.isGameOver)
    //        return;

    //    if (GameManager.Instance.secondHandControll.totalBalls <= 0)
    //    {
    //        if (GameManager.Instance.totalBalls <= 0)
    //        {

    //            if (ballDropInBucket < 50)
    //            {
    //                GameManager.Instance.DoTweenRestart("FaceToCamera");
    //                GameManager.Instance.GameOver();
    //                Debug.Log("GameOver");
    //            }
    //            else
    //            {
    //                GameManager.Instance.DoTweenRestart("FaceToCamera");
    //                GameManager.Instance.GameComplete();
    //                Debug.Log("gameComplete");
    //                confetti.SetActive(true);
    //            }


    //        }
    //    }
    //}
    bool isConfettiPlay = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("NewBall") || other.CompareTag("OldBall"))
        {

            ballDropInBucket++;
            GameManager.Instance.totalBalls--;

            Star(ballDropInBucket);
            GameManager.Instance.VibrationPhone();


            if (candyBoy != null)
                candyBoy.Chewing();
            newBall = other.gameObject;
            newBall.tag = "Untagged";
            if (Camera.main.transform.parent != basket.parent)
            {
                cameraposition = Camera.main.transform.localPosition;
                cameraposition.y = cameraposition.y - 4.25f;
                Camera.main.transform.DOMove(cameraposition, 3f);
                Camera.main.transform.parent = basket.parent;

            }


            percentage = ballDropInBucket / 50f;
            percentage = Mathf.Clamp01(percentage);
            y = 1.5f - 1.5f * percentage;
            newposition = new Vector3(0, y, 0);
            basket.parent.localPosition = newposition;



            textmeshBall.GetComponent<TextMesh>().text = ballDropInBucket + "/50";
            GameManager.Instance.totalFinalStageBall = ballDropInBucket;
            if (ballDropInBucket < 200)
            {

                if (candyBoy != null)
                    candyBoy.Chewing();
                StartCoroutine(DeactiveBall(newBall));

                GameManager.Instance.PlayBallSound();


            }
            else if (ballDropInBucket >= 50 && ballDropInBucket < 70 && !isOrdertoDropAllSecondHandBall)
            {

                isOrdertoDropAllSecondHandBall = true;
                GameManager.Instance.secondHandControll.DropAllSecondHandBall();
                GameManager.Instance.textPerfect.transform.GetChild(Random.Range(0, GameManager.Instance.textPerfect.transform.childCount)).gameObject.SetActive(true);
                DOTween.Restart("ShakeHead");
                GameManager.Instance.PlayBallSound();
            }
            else if (ballDropInBucket >= 70)
            {
                // if (!DOTween.IsTweening("ShakeHead"))
                DOTween.Restart("ShakeHead");
                if (basket.GetComponent<MeshRenderer>().enabled)
                    basket.GetComponent<MeshRenderer>().enabled = false;

                for (int i = 0; i < activeObjects.Length; i++)
                    activeObjects[i].SetActive(true);
                for (int i = 0; i < deactiveObjects.Length; i++)
                    deactiveObjects[i].SetActive(false);
            }



            if (GameManager.Instance.isGameOver)
                return;


            if (GameManager.Instance.secondHandControll.totalBalls <= 0)
            {
                //  Debug.Log("total ball  0  = " + GameManager.Instance.secondHandControll.totalBalls);

                if (GameManager.Instance.totalBalls <= 40)
                {



                    if (ballDropInBucket < 50)
                    {
                        Debug.Log("gameover = ");
                        GameManager.Instance.DoTweenRestart("FaceToCamera");
                        GameManager.Instance.GameOver();


                    }
                    else
                    {
                        Debug.Log("gamecompete = ");

                        if (!isConfettiPlay)
                            Invoke("playConfeetiEffect", 2f);
                        isConfettiPlay = true;
                        GameManager.Instance.DoTweenRestart("FaceToCamera");

                        GameManager.Instance.GameComplete();
                        //Destroy(starPanel, 2.5f);
                        // starPanel.transform.parent.gameObject.SetActive(false);


                        // 
                    }


                }
            }





            particleEffect.SetActive(true);




            if (!textmeshBall.GetCurrentAnimatorStateInfo(0).IsName("TextScaleAnimation"))
                textmeshBall.Play("TextScaleAnimation");


        }
    }
    void playConfeetiEffect()
    {
        confetti.SetActive(true);
    }

    void Star(int totalBall)
    {


        if (!isStarActive[0])
        {
            percentage = totalBall * 1f / 50;
            whiteStar[0].fillAmount = percentage;
            GameManager.Instance.totalStar = percentage;
            if (percentage >= 1)
            {
                isStarActive[0] = true;
                goldStar[0].SetActive(true);
                GameManager.Instance.PlaySound("Star");
                GameManager.Instance.totalStar = 1;
            }


        }

        if (!isStarActive[1])
        {

            if (totalBall >= 50)
            {
                percentage = (totalBall - 50 * 1f) / 80;


                whiteStar[1].fillAmount = percentage;

                GameManager.Instance.totalStar = 1 + percentage;
                if (percentage >= 1)
                {
                    isStarActive[1] = true;
                    goldStar[1].SetActive(true);
                    GameManager.Instance.PlaySound("Star");
                    GameManager.Instance.totalStar = 2;
                }
            }

        }

        if (!isStarActive[2])
        {

            if (totalBall >= 130 * 1f)
            {
                percentage = (totalBall - 130 * 1f) / 70;
                whiteStar[2].fillAmount = percentage;

                GameManager.Instance.totalStar = 2 + percentage;
                if (percentage >= 1)
                {
                    isStarActive[2] = true;
                    goldStar[2].SetActive(true);
                    GameManager.Instance.PlaySound("Star");
                    GameManager.Instance.totalStar = 3;


                }
            }

        }

        if (totalBall > 500)
            GameManager.Instance.uiManager.levelComplete.panelNeedle.SetActive(true);
    }

    #endregion UnityFunction

    #region CustomFunction

    IEnumerator DeactiveBall(GameObject ball)
    {

        yield return new WaitForSeconds(1.2f);
        ball.SetActive(false);
        ball.transform.parent = GameManager.Instance.deactiveBalls;

    }
    #endregion CustomFunction

}

