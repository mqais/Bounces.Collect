using DG.Tweening;
using UnityEngine;

public class StarMove : MonoBehaviour
{

    internal Vector3 startingPosition;
    internal Vector3 EndingPosition;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.position = startingPosition;
        float timer = 0.4f + Random.Range(0.2f, 0.4f);
        transform.DOMove(EndingPosition, 0.6f).OnComplete(AnimateCoins).SetEase(Ease.InSine);
        transform.DOScale(Vector3.one * 0.5f, 0.6f);
    }

    void AnimateCoins()
    {
        gameObject.SetActive(false);
    }
}
