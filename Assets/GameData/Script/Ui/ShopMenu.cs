using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    #region Variables
    public Text txtPowerPrice;
    public Text txtPowerNmber;
    public Text txtOfflinePrice;
    public Text txtOfflineNumber;
    public Text txtCash;
    public Image imgPower;
    public Image imgOffline;
    public Color disableColor;
    #endregion Variables
    #region UnityFunction
    // Start is called before the first frame update
    void Start()
    {
        Initilize();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion UnityFunction
    #region ButtonsAction

    #endregion ButtonsAction
    #region CustomFuntion
    private void Initilize()
    {
        txtPowerNmber.text = GameManager.Instance.PowerPlayer.ToString();
        txtPowerPrice.text = (100 + 25 * GameManager.Instance.PowerPlayer).ToString();
        txtOfflineNumber.text = GameManager.Instance.offlineReward.ToString();
        txtOfflinePrice.text = (100 + 25 * GameManager.Instance.offlineReward).ToString();

        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.PowerPlayer))
        {

            imgPower.color = disableColor;
            imgPower.GetComponent<Button>().enabled = false;
        }
        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.offlineReward))
        {
            imgOffline.color = disableColor;
            imgOffline.GetComponent<Button>().enabled = false;
        }
    }
    public void UpgradePower()
    {
        if (GameManager.Instance.totalStarInGame >= (100 + 25 * GameManager.Instance.PowerPlayer))
        {
            Invoke("powereffectOff", 1.5f);
            //MMVibrationManager.Haptic(HapticTypes.Selection, true);
            GameManager.Instance.totalStarInGame = GameManager.Instance.totalStarInGame - (100 + 25 * GameManager.Instance.PowerPlayer);
            GameManager.Instance.totalStarInGame = Mathf.Clamp(GameManager.Instance.totalStarInGame, 0, GameManager.Instance.totalStarInGame);
            txtCash.text = "" + GameManager.Instance.totalStarInGame;
            GameManager.Instance.PowerPlayer = GameManager.Instance.PowerPlayer + 1;
            txtPowerNmber.text = GameManager.Instance.PowerPlayer.ToString();
            txtPowerPrice.text = (100 + 25 * GameManager.Instance.PowerPlayer).ToString();
        }
        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.PowerPlayer))
        {
            imgPower.color = disableColor;
            imgPower.GetComponent<Button>().enabled = false;
        }
        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.offlineReward))
        {
            imgOffline.color = disableColor;
            imgOffline.GetComponent<Button>().enabled = false;
        }
    }
    public void UpgradeOfflineReward()
    {
        if (GameManager.Instance.totalStarInGame >= (100 + 25 * GameManager.Instance.offlineReward))
        {
            Invoke("healtheffectOff", 1.5f);
            //MMVibrationManager.Haptic(HapticTypes.Selection, true);
            GameManager.Instance.totalStarInGame = GameManager.Instance.totalStarInGame - (100 + 25 * GameManager.Instance.offlineReward);
            GameManager.Instance.totalStarInGame = Mathf.Clamp(GameManager.Instance.totalStarInGame, 0, GameManager.Instance.totalStarInGame);
            txtCash.text = "" + GameManager.Instance.totalStarInGame;
            GameManager.Instance.offlineReward = GameManager.Instance.offlineReward + 1;
            txtOfflineNumber.text = GameManager.Instance.offlineReward.ToString();
            txtOfflinePrice.text = (100 + 25 * GameManager.Instance.offlineReward).ToString();
        }
        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.PowerPlayer))
        {
            imgPower.color = disableColor;
            imgPower.GetComponent<Button>().enabled = false;
        }
        if (GameManager.Instance.totalStarInGame < (100 + 25 * GameManager.Instance.offlineReward))
        {
            imgOffline.color = disableColor;
            imgOffline.GetComponent<Button>().enabled = false;
        }
    }
    #endregion CustomFuntion

}
