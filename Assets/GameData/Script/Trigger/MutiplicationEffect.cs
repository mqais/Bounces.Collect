using DG.Tweening;
using UnityEngine;

public class MutiplicationEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int multiplierNumber;
    [SerializeField] Animator textmeshBall;
    [SerializeField] DOTweenAnimation movingPlateform;
    [SerializeField] SpriteRenderer sprtRenderer;
    [SerializeField] Sprite[] sprtNumbers;

    Renderer rendercube;
    GameObject newBall;
    int tempRandom = 0;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        sprtRenderer.sprite = sprtNumbers[multiplierNumber];
        rendercube = gameObject.GetComponent<MeshRenderer>();
        rendercube.material = GameManager.Instance.matrCubes[multiplierNumber];

    }
    void Update()
    {

    }
    string ballname = "";
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("NewBall"))
        {
            GameManager.Instance.PlayBallSound();
            if (movingPlateform != null)
            {
                movingPlateform.DOPause();
                movingPlateform = null;
            }
            ballname = other.gameObject.name;
            other.tag = "OldBall";
            GameManager.Instance.VibrationPhone();
            for (int i = 0; i < multiplierNumber; i++)
            {

                //   = Instantiate(other.gameObject, other.transform.position + new Vector3(Random.Range(0.15f, 0.25f), Random.Range(0.15f, 0.25f), Random.Range(0.15f, 0.25f)), Quaternion.identity);


                if (GameManager.Instance.deactiveBalls.childCount > 0)
                {


                    GameManager.Instance.totalBalls++;
                    newBall = GameManager.Instance.deactiveBalls.GetChild(0).gameObject;
                    newBall.SetActive(true);
                    newBall.tag = "OldBall";
                    if (ballname == "AlreadyJumpBall")
                        newBall.gameObject.name = "AlreadyJumpBall";
                    newBall.layer = 9;
                    //newBall.transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
                    newBall.transform.position = other.transform.position + new Vector3(Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f), 0);
                    newBall.transform.parent = GameManager.Instance.activeBalls;
                    //  newBall.GetComponent<MeshRenderer>().sharedMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
                    // newBall.transform.GetChild(0).GetComponent<TrailRenderer>().sharedMaterial = other.transform.GetChild(0).GetComponent<TrailRenderer>().material;
                }

            }

            if (!textmeshBall.GetCurrentAnimatorStateInfo(0).IsName("TextScaleAnimation"))
                textmeshBall.Play("TextScaleAnimation");


        }
    }
    #endregion UnityFunction

}
