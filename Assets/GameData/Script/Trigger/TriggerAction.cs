using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    #region Variables


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
        if (other.gameObject.layer == 9)
        {
            other.gameObject.layer = 8;
            other.gameObject.tag = "NewBall";

        }
        else if (other.gameObject.layer == 8)
        {
            other.gameObject.tag = "NewBall";
            other.gameObject.layer = 8;

        }
    }
    #endregion UnityFunction

    #region CustomFunction

    #endregion CustomFunction
    #region TrigerFunction

    #endregion TrigerFunction

}
