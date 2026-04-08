using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

public class SceneChanger : MonoBehaviour
{
    public void LoadHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadGame()
    {
        // 🔥 CHECK LIFE
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager NULL!");
            return;
        }

        Debug.Log("Lives hiện tại: " + GameManager.Instance.lives);

        if (GameManager.Instance.lives <= 0)
        {
            Debug.Log("Hết mạng → không cho vào");

            // 👉 test chắc chắn:
            return;
        }

        SceneManager.LoadScene("SampleScene");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}