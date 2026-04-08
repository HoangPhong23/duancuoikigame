using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Core;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI lifeText;

    void Start()
    {
        UpdateUI();
    }

    public void Buy1Life()
    {
        if (GameManager.Instance.BuyLife(100, 1))
        {
            UpdateUI();
        }
    }

    public void Buy5Lives()
    {
        if (GameManager.Instance.BuyLife(400, 5))
        {
            UpdateUI();
        }
    }

    public void Buy10Lives()
    {
        if (GameManager.Instance.BuyLife(700, 10))
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = "$" + GameManager.Instance.money;
        lifeText.text = "Lives: " + GameManager.Instance.lives;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}