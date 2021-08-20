using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Variables

    [SerializeField] internal Menu menu;
    [SerializeField] internal GamePlay gamePlay;
    [SerializeField] internal LevelComplete levelComplete;
    [SerializeField] internal LevelFailed levelFailed;

    #endregion Variables
    #region LocalVariables
    private int cashImageIndex = 0;
    #endregion LocalVariables
    #region UnityFunction
    void Start()
    {
        Initilize();
    }
    void Update()
    {

    }
    #endregion UnityFunction
    #region CustomFunction
    private void Initilize()
    {
        if (menu.panelOfflineReward != null)
            menu.panelOfflineReward.SetActive(true);
        else
            menu.panel.SetActive(true);
        //menu.music.isOn = !GameManager.Instance.music;
        menu.sound.isOn = GameManager.Instance.sound;
        menu.vibration.isOn = GameManager.Instance.vibration;
        gamePlay.textCurrentLevel.text = (GameManager.Instance.currentLevel + 1).ToString();
        gamePlay.textNextLevel.text = (GameManager.Instance.currentLevel + 2).ToString();
        if (gamePlay.levelBar != null)
            gamePlay.levelBar.value = 0;

        gamePlay.textStar.text = GameManager.Instance.totalStarInGame.ToString();
    }
    internal void TargetCounter()
    {

        float percentage = 0;
        gamePlay.levelBar.value = percentage;
        if (percentage >= 1)
        {
            gamePlay.confetti.SetActive(true);
            StartCoroutine(GameComplete(1f));
        }

    }
    internal void UpdateCashCountTxt(Vector3 cashposition, int Price)
    {
        GameManager.Instance.totalStarInGame = GameManager.Instance.totalStarInGame + Price;
        gamePlay.textStar.text = ((int)GameManager.Instance.totalStarInGame).ToString();
        gamePlay.cashParent.transform.GetChild(cashImageIndex).GetComponent<CashMove>().startingPosition = cashposition;
        gamePlay.cashParent.transform.GetChild(cashImageIndex).gameObject.SetActive(true);
        cashImageIndex++;
        cashImageIndex = (cashImageIndex >= gamePlay.cashParent.transform.childCount) ? 0 : cashImageIndex;
    }
    public IEnumerator startNeedle(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelComplete.panelNeedle.SetActive(true);
        int percentage = (int)(GameManager.Instance.totalFinalStageBall * 100f / GameManager.Instance.MaxScoreInLevel);
        float angle = 180f * (GameManager.Instance.totalFinalStageBall * 1f / GameManager.Instance.MaxScoreInLevel);
        angle = Mathf.Clamp(angle, 0, 180);
        percentage = Mathf.Clamp(percentage, 0, 100);
        int value = 0;

        levelComplete.needle.DORotate(new Vector3(0, 0, -angle), 2f);
        DOTween.To(() => value, x => value = x, percentage, 2f).OnUpdate(() =>
        {

            levelComplete.textbetterthan.text = "Better than " + value + "%";

        }).OnComplete(() =>
        {
            StartCoroutine(GameComplete(0.5f));
        }).SetEase(Ease.Linear);
    }
    public void AddStar(int value)
    {

        GameManager.Instance.totalStarInGame += value;
        gamePlay.textStar.text = GameManager.Instance.totalStarInGame.ToString();

    }
    public IEnumerator GameComplete(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelComplete.panel.SetActive(true);
        levelComplete.panelNeedle.SetActive(false);
    }


    public IEnumerator GameOver(float delay)
    {
        yield return new WaitForSeconds(delay);


        if (GameManager.Instance.totalFinalStageBall >= 50)
        {
            StartCoroutine(GameComplete(0.15f));
        }
        else
        {
            if (!GameManager.Instance.isGameOver)
            {
                GlobalAudioPlayer.Play("GameOver");
                GameManager.Instance.isGameOver = true;
                GameManager.Instance.isGameWin = false;
                levelFailed.panel.SetActive(true);
            }
        }




    }
    #endregion CustomFunction
    #region Classes
    [System.Serializable]
    public struct Menu
    {
        [SerializeField] internal GameObject panel;
        [SerializeField] internal GameObject panelOfflineReward;
        [SerializeField] internal Toggle music;
        [SerializeField] internal Toggle sound;
        [SerializeField] internal Toggle vibration;

    }
    [System.Serializable]
    public struct GamePlay
    {
        [SerializeField] internal GameObject panel;
        [SerializeField] internal GameObject cashParent;
        [SerializeField] internal GameObject confetti;
        [SerializeField] internal Transform cashTargetPositon;
        [SerializeField] internal Text textCurrentLevel;
        [SerializeField] internal Text textNextLevel;
        [SerializeField] internal Text textStar;
        [SerializeField] internal Slider levelBar;
    }
    [System.Serializable]
    public struct LevelComplete
    {
        [SerializeField] internal GameObject panel;
        [SerializeField] internal GameObject panelNeedle;
        [SerializeField] internal CashCollectionControll CashCollectionControll;
        [SerializeField] internal Text textCash;
        [SerializeField] internal Text textbetterthan;
        [SerializeField] internal Text textLeveComplete;
        [SerializeField] internal RectTransform needle;

        [SerializeField] internal Transform star;
    }
    [System.Serializable]
    public struct LevelFailed
    {
        [SerializeField] internal GameObject panel;

    }
    #endregion Classes
    #region ButtonActions



    #endregion ButtonActions
}
