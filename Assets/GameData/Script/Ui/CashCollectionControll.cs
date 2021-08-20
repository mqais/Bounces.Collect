using DG.Tweening;
using UnityEngine;

public class CashCollectionControll : MonoBehaviour
{
    private int index = 0;
    private int index1 = 0;
    private int index2 = 0;

    [SerializeField] internal float pricepercash = 0;
    [SerializeField] internal float totalCash = 0;
    [SerializeField] Transform TargetPosition;
    [SerializeField] Transform StartingPosition;
    public Vector3 Position2d;

    private void OnEnable()
    {
        index = 0;
        index1 = 0;
        index2 = 0;
        NextCashFloat();

        pricepercash = totalCash / (transform.childCount);
    }
    Sequence mySequence;
    void NextCashFloat()
    {
        Vector3 temppos;



        for (int i = 0; i < transform.childCount; i++)
        {
            mySequence = DOTween.Sequence();

            temppos = transform.GetChild(index).position;
            transform.GetChild(index).gameObject.SetActive(true);
            transform.GetChild(index).position = StartingPosition.position;

            mySequence.Append(transform.GetChild(index).DOMove(temppos, 0.35f).OnComplete(MoveToTarget).SetEase(Ease.InSine));
            mySequence.PrependInterval(i * 0.025f);
            index++;
        }
    }
    void MoveToTarget()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.GetChild(index1).DOMove(TargetPosition.position, 1f).OnComplete(AnimateCashs).SetEase(Ease.InSine));
        mySequence.Append(transform.GetChild(index1).DOScale(new Vector3(1, 1, 1), 1));
        mySequence.PrependInterval(index1 * 0.025f);
        index1++;
    }
    void AnimateCashs()
    {



        if (!GetComponent<AudioSource>().isPlaying && GameManager.Instance.sound)
            GetComponent<AudioSource>().Play();

        TargetPosition.parent.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        TargetPosition.parent.DOScale(Vector3.one, 0.02f).OnComplete(() =>
        {
            transform.GetChild(index2).gameObject.SetActive(false);
            index2++;
            if (index2 >= transform.childCount - 1)
            {
                gameObject.SetActive(false);
                GetComponent<AudioSource>().Pause();

            }

            GameManager.Instance.uiManager.AddStar(1);
        });




        //gameObject.SetActive(false);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
        GetComponent<AudioSource>().Pause();
    }
}
