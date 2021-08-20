using DG.Tweening;
using UnityEngine;

public class CashMove : MonoBehaviour
{

    internal Vector3 startingPosition;
    internal Vector3 EndingPosition;
    // Start is called before the first frame update
    private void OnEnable()
    {

        GameManager.Instance.PlayBallSound();
        transform.position = startingPosition;
        transform.DOMove(EndingPosition, 0.15f + Random.Range(0.2f, 0.4f)).OnComplete(AnimateCoins).SetEase(Ease.InSine);

    }

    void AnimateCoins()
    {
        gameObject.SetActive(false);
    }
}
