using DG.Tweening;
using UnityEngine;
public class SubstractionEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int substractionNumber;
    [SerializeField] Animator textmeshBall;
    [SerializeField] DOTweenAnimation movingPlateform;
    [SerializeField] Transform minus1Parent;

    int index = 0;

    GameObject newBall;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        textmeshBall.GetComponent<TextMesh>().text = "-" + substractionNumber.ToString();
    }

    bool checkGameOver = false;
    void GameOver()
    {
        if (substractionNumber > 0)
        {

            if (GameManager.Instance.secondHandControll.totalBalls <= 0 && GameManager.Instance.firstHandControll == null)
                StartCoroutine(GameManager.Instance.uiManager.GameOver(4f));


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("working");
        if (other.CompareTag("NewBall"))
        {
            other.tag = "OldBall";

            if (movingPlateform != null)
            {
                movingPlateform.DOPause();
                movingPlateform = null;
            }
            substractionNumber--;
            minus1Parent.GetChild(index % minus1Parent.childCount).gameObject.SetActive(true);
            index++;
            GameManager.Instance.VibrationPhone();
            GameManager.Instance.PlayBallSound();
            GameManager.Instance.totalBalls--;

            textmeshBall.GetComponent<TextMesh>().text = "-" + substractionNumber.ToString();


            if (!checkGameOver)
            {
                Invoke("GameOver", 2f);
            }
            checkGameOver = true;


            if (substractionNumber > 0)
            {


                newBall = other.gameObject;
                newBall.SetActive(false);
                newBall.transform.parent = GameManager.Instance.deactiveBalls;
                newBall.tag = "OldBall";

                // Debug.Log("totalball = " + GameManager.Instance.totalBalls);
            }
            else
            {
                // Debug.Log("totalball = " + GameManager.Instance.totalBalls);
                Destroy(other.gameObject);
                gameObject.SetActive(false);
                textmeshBall.gameObject.SetActive(false);
            }


            if (!textmeshBall.GetCurrentAnimatorStateInfo(0).IsName("TextScaleAnimation"))
                textmeshBall.Play("TextScaleAnimation");


        }
    }

    #endregion UnityFunction

}
