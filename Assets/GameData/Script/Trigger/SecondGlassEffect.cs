using DG.Tweening;
using UnityEngine;

public class SecondGlassEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int totalBalls;
    [SerializeField] Animator textmeshBall;
    [SerializeField] GameObject particleEffect;


    GameObject newBall;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        textmeshBall.GetComponent<TextMesh>().text = totalBalls.ToString();
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("NewBall") || other.CompareTag("OldBall"))
        {

            totalBalls++;
            textmeshBall.GetComponent<TextMesh>().text = totalBalls.ToString();
            GameManager.Instance.VibrationPhone();
            GameManager.Instance.PlayBallSound();
            DOTween.Restart("ShakeHand");

            newBall = other.gameObject;
            newBall.SetActive(false);
            newBall.transform.parent = GameManager.Instance.deactiveBalls;
            GameManager.Instance.totalBalls--;

            if (GameManager.Instance.firstHandControll.totalBalls <= 0)
            {
                if (GameManager.Instance.totalBalls <= 0)
                {
                    GameManager.Instance.DoTweenRestart("GoToPosition1");
                    GameManager.Instance.secondHandControll.SetTotalBalls(totalBalls);
                    GameManager.Instance.firstHandControll = null;
                    GameManager.Instance.PlaySound("HandEffect");

                }
            }



            particleEffect.SetActive(true);




            if (!textmeshBall.GetCurrentAnimatorStateInfo(0).IsName("TextScaleAnimation"))
                textmeshBall.Play("TextScaleAnimation");


        }
    }
    #endregion UnityFunction



}

