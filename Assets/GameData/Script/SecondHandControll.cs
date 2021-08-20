using UnityEngine;

public class SecondHandControll : MonoBehaviour
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

    bool isOrdertoDropAllSecondHandBall = false;
    public bool IsHandPositionSet
    {
        get { return isHandPositionSet; }
        set { isHandPositionSet = value; }
    }

    #endregion Variables
    #region LocalVariables
    float y;
    float z;
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



    private void Start()
    {

        isRotateAlready = false;



        z = -0.72f;
        GameManager.Instance.secondHandControll = this;
        this.enabled = false;
    }
    private void Update()
    {




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
            tempPosition.z = z;
            tempPosition.x = Mathf.Clamp(tempPosition.x, minX, maxX);
            transform.position = tempPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //  if (!isOrdertoDropAllSecondHandBall)
            //   CancelInvoke("InstantiateBalls");
        }
    }
    #endregion UnityFunction


    #region CustomFunction


    public void DropAllSecondHandBall()
    {
        isOrdertoDropAllSecondHandBall = true;
        CancelInvoke("InstantiateBalls");
        InvokeRepeating("InstantiateBalls", 0.01f, 0.2f);
    }
    public void SetTotalBalls(int totalBalls)
    {
        this.totalBalls = totalBalls;
        GameManager.Instance.totalBallInHand = totalBalls;
        GameManager.Instance.totalBalls = 0;
        textTotalBalls.text = totalBalls.ToString();
    }
    public void setYPosition(float value)
    {
        y = value;
    }
    void InstantiateBalls()
    {
        if (!IsHandPositionSet)
            IsHandPositionSet = true;
        if (totalBalls > 0)
        {
            totalBalls--;
            textTotalBalls.text = totalBalls.ToString();


            if (GameManager.Instance.deactiveBalls.childCount > 0)
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
        newBall.gameObject.name = "Ball";
        newBall.SetActive(true);
        newBall.transform.position = ballPosition.position;
        newBall.transform.parent = GameManager.Instance.activeBalls;
        // tempRandom = Random.Range(0, ballMaterial.Length);
        // newBall.GetComponent<MeshRenderer>().sharedMaterial = ballMaterial[0];
        // newBall.transform.GetChild(0).GetComponent<TrailRenderer>().sharedMaterial = tailMaterial[0];
        // newBall.transform.GetChild(0).GetComponent<TrailRenderer>().Clear();

        //  newBall.transform.GetChild(0).GetComponent<TrailRenderer>().emitting = true;

    }

    public void SetDrag(bool value)
    {
        isDrag = value;
    }
    #endregion CustomFunction

}
