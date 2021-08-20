using UnityEngine;
using UnityEngine.UI;

public class RotationControl : MonoBehaviour
{
    [SerializeField] float minAnlge = 0F;
    [SerializeField] float maxAngle = 180F;
    static RotationControl thisSpeedo;
    [SerializeField] Transform needle;
    [SerializeField] float angle;
    [SerializeField] Text textbetterthan;

    // [Range(0, 1f)]
    public float percentage = 0f;
    float finalPercentage = 0f;
    // Use this for initialization
    void Start()
    {
        thisSpeedo = this;
    }
    void Update()
    {

        percentage = (GameManager.Instance.totalFinalStageBall * 1f / GameManager.Instance.MaxScoreInLevel);

        finalPercentage = Mathf.Lerp(finalPercentage, percentage, Time.deltaTime);

        textbetterthan.text = Mathf.Clamp((int)(finalPercentage * 100) + 30, 0, 100) + "%";
        float ang = Mathf.LerpAngle(minAnlge, maxAngle, 1 - (finalPercentage + 0.3f));
        needle.transform.eulerAngles = new Vector3(0, 0, ang);

    }
}