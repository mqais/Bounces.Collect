using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] Status status;
    [SerializeField] Transform[] ballNewPosition;
    int index = 0;
    enum Status
    {
        VacumeTagChange,
        VacumePositionChange
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.PlayBallSound();
        switch (status)
        {
            case Status.VacumeTagChange:
                Debug.Log(other.gameObject.name);
                if (other.CompareTag("NewBall"))
                {
                    other.tag = "ChangePosition";
                }

                break;
            case Status.VacumePositionChange:
                if (other.CompareTag("ChangePosition"))
                {

                    other.tag = "NewBall";
                    other.gameObject.SetActive(false);
                    other.transform.parent = GameManager.Instance.deactiveBalls;
                    NewBallSett();
                }
                break;
            default:
                break;
        }
    }

    GameObject newBall;
    void NewBallSett()
    {
        newBall = GameManager.Instance.deactiveBalls.GetChild(0).gameObject;
        newBall.layer = 9;
        newBall.tag = "NewBall";
        newBall.SetActive(true);
        newBall.transform.position = ballNewPosition[index % ballNewPosition.Length].position;
        newBall.transform.parent = GameManager.Instance.activeBalls;

        index++;


    }
}
