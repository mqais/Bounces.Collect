using UnityEngine;
public class ElasticEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int lockNumber;
    [SerializeField] TextMesh textmeshBall;
    [SerializeField] SkinnedMeshRenderer skinMeshRenderer;
    [SerializeField] GameObject cubeCollider;
    [SerializeField] Transform ColliderBox;


    GameObject newBall;
    Vector3 localpositon;
    float currentvalue = 0;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        textmeshBall.text = lockNumber.ToString();
        localpositon = cubeCollider.transform.localPosition;
    }
    void Update()
    {

        currentvalue = Mathf.Lerp(currentvalue, (10 - lockNumber) * 20, Time.deltaTime * 10);
        skinMeshRenderer.SetBlendShapeWeight(0, currentvalue);

        localpositon.y = -currentvalue * 0.000025f;
        cubeCollider.transform.localPosition = localpositon;
    }
    bool isReachLimit = false;
    bool checkGameOver = false;
    void GameOver()
    {
        if (lockNumber > 0)
            StartCoroutine(GameManager.Instance.uiManager.GameOver(0.25f));

    }
    private void OnTriggerEnter(Collider other)
    {
        if (isReachLimit)
            return;

        if (other.CompareTag("NewBall"))
        {
            other.tag = "OldBall";

            lockNumber--;

            if (!checkGameOver)
            {
                Invoke("GameOver", 2f);
            }
            checkGameOver = true;

            for (int i = 0; i < ColliderBox.childCount; i++)
            {
                ColliderBox.GetChild(i).gameObject.SetActive(false);
            }

            ColliderBox.GetChild(Mathf.Clamp((int)(10 - lockNumber), 0, ColliderBox.childCount - 1)).gameObject.SetActive(true);
            // int value = 0;

            //DOTween.To(() => value, x => value = x, (10 - lockNumber) * 10, 0.05f).OnUpdate(() =>
            //    {

            //        skinMeshRenderer.SetBlendShapeWeight(0, value);
            //    });



            // GameManager.Instance.totalBalls--;
            textmeshBall.text = lockNumber.ToString();
            Vector3 tempposition = textmeshBall.transform.position;
            tempposition.y = tempposition.y - 0.00025f;
            if (lockNumber > 0)
            {
            }
            else
            {
                isReachLimit = true;

                Invoke("disableWall", 1f);

            }




        }
    }

    private void disableWall()
    {
        gameObject.SetActive(false);
        textmeshBall.gameObject.SetActive(false);
    }
    #endregion UnityFunction
}
