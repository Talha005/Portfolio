using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopSystemManager : MonoBehaviour
{
    [SerializeField]
    public PlayerCostumes[] PlayerCostumes;

    public ShopMenu[] ShopMenu;
    public ShopItemButton ShopItemButton;
    public Button DeleteCostumesButton;

    [Header("Player Settings")]
    public GameObject playerGO;
    public Transform InstantiatedCostumesPlaceholder;

    [Header("Coins UI Settings")]
    public Text CoinsText;

    [Header("Menu Selection Buttons")]
    public GameObject[] MenuSelectionButtons;
    public Color SelectionColor;
    public Color DeselectionColor;

    [Header("VFX Settings")]
    public ParticleSystem VFX;

    [Header("Materials Settings")]
    public MeshRenderer PlayerMeshRenderer;

    [Header("Instantiated Hats Accessories")]
    public List<GameObject> InstantiatedHats;
    public List<GameObject> InstantiatedAccessories;

    [Header("Notification Panel")]
    public GameObject NotificationPanel;

    #region Singelton

    public static ShopSystemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    #region Default Callbacks

    private void Start()
    {
        //Initialize variables.
        InstantiatedHats = new List<GameObject>();
        InstantiatedAccessories = new List<GameObject>();

        //Populate all menus.
        PopulateAllMenus();

        //Select & Animate first menu.
        ShopMenu[0].AnimateMenuButtons();
        SelectButton(MenuSelectionButtons[0]);
        OnSelectMenu(0);

        //Update Coins Text.
        UpdateCoinsText(GetPlayerCoinsValue());

       // LoadingScreenUI.EndLoadingScreenEvent?.Invoke();
    }

    #endregion

    #region Menus Population

    private void PopulateMenu(int MenuIndex)
    {
        ShopItemButton shopItemButton;

        if (PlayerCostumes[MenuIndex].HasDeleteButton) 
        {
            InstantiateDeleteCostumesButton(MenuIndex);
        }

        for (int i = 0; i < PlayerCostumes[MenuIndex].PlayerCostumesList.Count; i++)
        {
            var button = Instantiate(ShopItemButton.gameObject, ShopMenu[MenuIndex].ButtonsParentObject);

            shopItemButton = button.GetComponent<ShopItemButton>();
            shopItemButton.SetID(i);
            shopItemButton.SetPlayerCostume(PlayerCostumes[MenuIndex].PlayerCostumesList[i]);
            shopItemButton.ClickShopItemButtonEvent.AddListener(PlayerCostumes[MenuIndex].PlayerCostumesList[i].OnclickCustomizationButton);
            shopItemButton.ClickShopTryButtonEvent.AddListener(PlayerCostumes[MenuIndex].PlayerCostumesList[i].OnclickTryButton);
            shopItemButton.UpdateCoinsText(PlayerCostumes[MenuIndex].PlayerCostumesList[i].Value);
            shopItemButton.SetItemImage(PlayerCostumes[MenuIndex].PlayerCostumesList[i].Sprite);
            shopItemButton.ShowProperButton(PlayerCostumes[MenuIndex].PlayerCostumesList[i].Bought);

            ShopMenu[MenuIndex].AddNewButton(button);
        }
    }

    private void PopulateAllMenus()
    {
        for (int i = 0; i < PlayerCostumes.Length; i++)
        {
            PopulateMenu(i);
        }
    }

    #endregion

    #region Player Customs Instantiation

    public void DestroyPreviousOwnedHats()
    {
        foreach (var item in InstantiatedHats)
        {
            Destroy(item);
        }

        InstantiatedHats.Clear();
        PlayerCustomizationManager.Instance.ClearAllHats();
    }

    public void DestroyPreviousOwnedAccessories()
    {
        foreach (var item in InstantiatedAccessories)
        {
            Destroy(item);
        }

        InstantiatedAccessories.Clear();
        PlayerCustomizationManager.Instance.ClearAllAccessories();
    }

    #endregion

    #region Menu Selection Buttons

    public void ResetAllSelectionButtons()
    {
        foreach (var button in MenuSelectionButtons)
        {
            LeanTween.scale(button.gameObject, Vector3.one, 0.3f);
            button.gameObject.GetComponent<Image>().color = DeselectionColor;
        }
    }

    public void SelectButton(GameObject buttonToSelect)
    {
        //LeanTween.scale(buttonToSelect, new Vector3(1.3f, 1.3f, 1.3f), 0.3f);
        buttonToSelect.GetComponent<Image>().color = SelectionColor;
    }

    private void ResetAll()
    {
        foreach (var item in ShopMenu)
        {
            item.ResetMenuButtons();
        }
    }

    public void OnSelectMenu(int menuID)
    {
        ResetAll();
        ShopMenu[menuID].MenuObject.SetActive(true);
        ShopMenu[menuID].AnimateMenuButtons();
    }

    public void RotatePlayer(float rotation)
    {
        playerGO.transform.Rotate(new Vector3(0, rotation, 0));
    }

    




    #endregion

    #region Player Coins

    public void UpdateCoinsText(int value)
    {
        CoinsText.text = value.ToString();
    }

    public void SetPlayerCoinsValue(int value)
    {
        PlayerDataManager.Instance.UpdatePlayerCoins(value);
    }

    public int GetPlayerCoinsValue()
    {
        return PlayerDataManager.Instance.GetPlayerCoinsValue();
    }

    #endregion

    #region Delete Costumes Button

    private void InstantiateDeleteCostumesButton(int ShopMenuIndex)
    {
        var button = Instantiate(DeleteCostumesButton.gameObject, ShopMenu[ShopMenuIndex].ButtonsParentObject);
        ShopMenu[ShopMenuIndex].AddNewButton(button);
        button.GetComponent<Button>().onClick.AddListener(delegate { PlayerCostumes[ShopMenuIndex].DeleteCostumes(); });
    }

    #endregion

    #region MyRegion

    public void ShowNotificationPanel()
    {
        if (!NotificationPanel.activeInHierarchy)
        {
            StartCoroutine(ShowNotificationPanelCorotinue());
        }
    }

    private IEnumerator ShowNotificationPanelCorotinue()
    {
        NotificationPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        NotificationPanel.SetActive(false);
    }

    #endregion
}

[System.Serializable]
public struct PlayerCostumes
{
    public string CostumesCategory;

    public List<PlayerCostume> PlayerCostumesList;

    public bool HasDeleteButton;

    public void DeleteCostumes()
    {
        //Destroy.
        foreach (Transform child in PlayerCustomizationManager.Instance.AccessoriesPlaceholder)
        {
            if (child.gameObject.tag.Equals(CostumesCategory))
            {
                Debug.Log("Destroy !!!");
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }

        //SaveData.
        PlayerDataManager.Instance.DeletePlayerCostume(CostumesCategory);
    }
}
