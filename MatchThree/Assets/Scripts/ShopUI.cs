using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject shopOverlay;

    public void OpenShop()
    {
        shopOverlay.SetActive(true);
    }

    public void CloseShop()
    {
        shopOverlay.SetActive(false);
    }
}
