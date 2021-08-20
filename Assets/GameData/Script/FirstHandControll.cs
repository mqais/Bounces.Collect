using DG.Tweening;
using UnityEngine;

public class FirstHandControll : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] string handName;
    [SerializeField] internal bool isDrag = false;
    [SerializeField] internal int totalBalls = 10;
    [SerializeField] TextMesh textTotalBalls;


    [SerializeField] float minX;
    [SerializeField] float maxX;

    [Header("Ball Data")]
    [SerializeField] Transform ballPosition;
    [SerializeField] Material[] ballMaterial;
    [SerializeField] Material[] tailMaterial;


    public bool IsHandPositionSet
    {
        get { return isHandPositionSet; }
        set { isHandPositionSet = value; }
    }

    #endregion Variables
    #region LocalVariables
    float y;
    float zCoordinate;
    Vector3 offset;
    Vector3 tempPosition;
    bool isRotateAlready = false;

    bool isHandPositionSet = false;
    bool isInstantiateBall = false;
    int tempRandom = 0;

    GameObject newBall;
    #endregion LocalVariables
    #region UnityFunction

    public void VideoReward()
    {
        totalBalls = totalBalls * 2;
        textTotalBalls.text = totalBalls.ToString();
        textTotalBalls.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        textTotalBalls.transform.DOScale(Vector3.one, 0.25f);
    }
    private void Start()
    {

        isRotateAlready = false;
        y = transform.position.y;
        textTotalBalls.text = totalBalls.ToString();

        GameManager.Instance.totalBallInHand = totalBalls;
        GameManager.Instance.firstHandControll = this;
    }
    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        if (!isDrag)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isRotateAlready == false)
            {
                isRotateAlready = true;
                DG.Tweening.DOTween.Restart(handName + "Rotate");
            }
            zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
            offset = transform.position - GetMouseWorldPos();

            // see first time ball instantiate
            if (!IsHandPositionSet)
                InvokeRepeating("InstantiateBalls", 0.25f, 0.2f);
        }

        if (Input.GetMouseButton(0))
        {
            tempPosition = GetMouseWorldPos() + offset;
            tempPosition.y = y;
            tempPosition.x = Mathf.Clamp(tempPosition.x, minX, maxX);
            transform.position = tempPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // CancelInvoke("InstantiateBalls");
        }
    }
    #endregion UnityFunction


    #region CustomFunction

    //public void FallingBall(Transform ball, Transform PathTransform)
    //{
    //    if (PathTransform.childCount >= 2)
    //    {
    //        Vector3[] vectorPath = new Vector3[PathTransform.childCount];
    //        for (int i = 0; i < PathTransform.childCount; i++)
    //        {
    //            vectorPath[i] = PathTransform.GetChild(i).position;

    //        }
    //        ball.DOPath(vectorPath, 0.25f, PathType.CatmullRom).SetEase(Ease.Linear);

    //    }
    //    else
    //    {
    //        Debug.Log("give transform More than One Child");
    //    }

    //}
    void InstantiateBalls()
    {
        if (!IsHandPositionSet)
            IsHandPositionSet = true;
        if (totalBalls > 0)
        {
            totalBalls--;
            textTotalBalls.text = totalBalls.ToString();

            if (GameManager.Instance.deactiveBalls.childCount > 2)
            {
                NewBallSett();
                GameManager.Instance.totalBalls++;
                GameManager.Instance.totalBallInHand--;
            }
        }
        else
        {
            CancelInvoke("InstantiateBalls");
        }



    }
    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of gameObject on screen
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void NewBallSett()
    {
        newBall = GameManager.Instance.deactiveBalls.GetChild(0).gameObject;
        newBall.layer = 9;
        newBall.tag = "NewBall";
        newBall.SetActive(true);
        newBall.transform.position = ballPosition.position;
        newBall.transform.parent = GameManager.Instance.activeBalls;
        tempRandom = Random.Range(0, ballMaterial.Length);
        //newBall.GetComponent<MeshRenderer>().sharedMaterial = ballMaterial[0];
        //newBall.transform.GetChild(0).GetComponent<TrailRenderer>().sharedMaterial = tailMaterial[0];

    }
    #endregion CustomFunction

}
