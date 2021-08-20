using UnityEngine;

public class UnKnownEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int multiplierNumber;
    [SerializeField] Animator textmeshBall;

    GameObject newBall;
    int tempRandom = 0;
    #endregion Variables


    #region UnityFunction
    void Start()
    {

    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("NewBall"))
        {

            GameManager.Instance.PlayBallSound();
            other.tag = "OldBall";

            for (int i = 0; i < multiplierNumber - 1; i++)
            {

                //   = Instantiate(other.gameObject, other.transform.position + new Vector3(Random.Range(0.15f, 0.25f), Random.Range(0.15f, 0.25f), Random.Range(0.15f, 0.25f)), Quaternion.identity);


                if (GameManager.Instance.deactiveBalls.childCount > 0)
                {
                    GameManager.Instance.totalBalls++;
                    newBall = GameManager.Instance.deactiveBalls.GetChild(0).gameObject;
                    newBall.SetActive(true);
                    newBall.tag = "OldBall";
                    newBall.layer = 9;

                    newBall.transform.position = other.transform.position + new Vector3(Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f), 0);
                    newBall.transform.parent = GameManager.Instance.activeBalls;

                    newBall.GetComponent<MeshRenderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
                    newBall.transform.GetChild(0).GetComponent<TrailRenderer>().material = other.transform.GetChild(0).GetComponent<TrailRenderer>().material;



                }

            }

            if (!textmeshBall.GetCurrentAnimatorStateInfo(0).IsName("TextScaleAnimation"))
                textmeshBall.Play("TextScaleAnimation");


        }
    }
    #endregion UnityFunction

}
