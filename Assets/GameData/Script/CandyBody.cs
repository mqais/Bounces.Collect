using DG.Tweening;
using UnityEngine;

public class CandyBody : MonoBehaviour
{

    [SerializeField] SkinnedMeshRenderer boyRenderer;
    [SerializeField] GameObject textCandies;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EyeBlink", 2, 2f);

    }


    void EyeBlink()
    {
        int value = 0;
        DOTween.To(() => value, x => value = x, 100, 0.15f).OnUpdate(() =>
        {
            boyRenderer.SetBlendShapeWeight(22, value);
            boyRenderer.SetBlendShapeWeight(24, value);
        }).OnComplete(() =>
        {

            value = 100;
            DOTween.To(() => value, x => value = x, 0, 0.1f).OnUpdate(() =>
            {
                boyRenderer.SetBlendShapeWeight(22, value);
                boyRenderer.SetBlendShapeWeight(24, value);
            });

        });
    }

    bool ischewingComplete = true;
    public void Chewing()
    {

        //if (GameManager.Instance.isGameOver)
        //     return;
        int value1 = 100;

        if (ischewingComplete)
        {
            ischewingComplete = false;
            DOTween.To(() => value1, x => value1 = x, 50, 0.1f).OnUpdate(() =>
            {
                boyRenderer.SetBlendShapeWeight(10, value1);
            }).OnComplete(() =>
            {

                value1 = 50;
                DOTween.To(() => value1, x => value1 = x, 100, 0.1f).OnUpdate(() =>
                {
                    boyRenderer.SetBlendShapeWeight(10, value1);
                }).OnComplete(() =>
                {
                    ischewingComplete = true;
                });
            }).SetId("Chewing");

        }

    }

    void ChewingAfterGameOver()
    {
        int value1 = 100;



        if (ischewingComplete)
        {
            ischewingComplete = false;
            DOTween.To(() => value1, x => value1 = x, 30, 0.2f).OnUpdate(() =>
            {
                boyRenderer.SetBlendShapeWeight(11, value1);
            }).OnComplete(() =>
            {

                value1 = 30;
                DOTween.To(() => value1, x => value1 = x, 100, 0.2f).OnUpdate(() =>
                {
                    boyRenderer.SetBlendShapeWeight(11, value1);
                }).OnComplete(() =>
                {
                    ischewingComplete = true;

                });
            });
        }
    }

    public void NormalFace()
    {
        int value = 100;
        DOTween.To(() => value, x => value = x, 0, 0.25f).OnUpdate(() =>
        {
            boyRenderer.SetBlendShapeWeight(10, value);
        });
    }
    // Update is called once per frame
    public void Reaction()
    {
        textCandies.SetActive(false);
        int value = 100;
        if (GameManager.Instance.isGameWin)
        {
            DOTween.Pause("Chewing");
            DOTween.To(() => value, x => value = x, 0, 0.15f).OnUpdate(() =>
            {


                boyRenderer.SetBlendShapeWeight(8, value);
            }).OnComplete(() =>
            {
                anim.enabled = true;
                anim.Play("Happy");
                InvokeRepeating("ChewingAfterGameOver", 0.01f, 0.5f);

            });
        }
        else
        {
            DOTween.To(() => value, x => value = x, 100, 0.5f).OnUpdate(() =>
            {
                anim.enabled = true;
                anim.Play("Cry");
                boyRenderer.SetBlendShapeWeight(9, value);
            });
        }
    }
    public void Angry()
    {

    }
}
