using UnityEngine;

public class LockEffect : MonoBehaviour
{
    #region Variables
    [Header("Main")]
    [SerializeField] int lockNumber;
    [SerializeField] TextMesh textmeshBall;
    [SerializeField] GameObject brokenParts;



    GameObject newBall;
    #endregion Variables


    #region UnityFunction
    void Start()
    {
        textmeshBall.text = lockNumber.ToString();
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewBall"))
        {
            other.tag = "OldBall";

            lockNumber--;



            // GameManager.Instance.totalBalls--;
            textmeshBall.text = lockNumber.ToString();
            if (lockNumber > 0)
            {
            }
            else
            {

                //Destroy(other.gameObject);
                gameObject.SetActive(false);
                if (brokenParts != null)
                    brokenParts.SetActive(true);
                textmeshBall.gameObject.SetActive(false);
            }




        }
    }
    #endregion UnityFunction

}
