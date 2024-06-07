using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizationManager : MonoBehaviour
{
    [Header("Materials Settings")]
    public MeshRenderer PlayerMeshRenderer;
    public Transform AccessoriesPlaceholder;
  
    #region Singelton Region

    public static PlayerCustomizationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    public void CustomizePlayer(PlayerData playerData)
    {
        if (playerData == null) return;

        Debug.Log("---CustomizePlayer---");

        CustomizePlayerExpression(PlayerMeshRenderer, playerData.PlayerExpression.ExpressionMaterial);
        CustomizePlayerSkin(PlayerMeshRenderer, playerData.PlayerSkin.SkinMaterial);
        CustomizePlayerHat(playerData.PlayerHat, AccessoriesPlaceholder);
        CustomizePlayerAccessory(playerData.PlayerAccessory, AccessoriesPlaceholder);
    }

    public void CustomizePlayer(PlayerData playerData, Transform CustomsPlaceholder, MeshRenderer playerMeshRenderer)
    {
        if (playerData == null) return;

        CustomizePlayerExpression(playerMeshRenderer, playerData.PlayerExpression.ExpressionMaterial);
        CustomizePlayerSkin(playerMeshRenderer, playerData.PlayerSkin.SkinMaterial);
        CustomizePlayerHat(playerData.PlayerHat, CustomsPlaceholder);
        CustomizePlayerAccessory(playerData.PlayerAccessory, CustomsPlaceholder);
    }

    #region Expression & Skin Customization

    public void CustomizePlayerMaterials(Material ExpressionMaterial, Material SkinMaterial)
    {
        if (ExpressionMaterial == null || SkinMaterial == null) return;

        Material[] newMaterials = new Material[] { ExpressionMaterial, SkinMaterial};
        PlayerMeshRenderer.materials = newMaterials;
    }

    public void CustomizePlayerExpression(MeshRenderer PlayerMeshRenderer, Material ExpressionMaterial)
    {
        if (PlayerMeshRenderer == null || ExpressionMaterial == null) return;

        Material[] oldMaterials = PlayerMeshRenderer.materials;
        Material[] newMaterials = new Material[] { ExpressionMaterial, oldMaterials[1] };
        PlayerMeshRenderer.materials = newMaterials;
    }

    public void CustomizePlayerSkin(MeshRenderer PlayerMeshRenderer, Material SkinMaterial)
    {
        if ((PlayerMeshRenderer == null) || (SkinMaterial == null)) return;

        Material[] oldMaterials =  PlayerMeshRenderer.materials;
        Material[] newMaterials = new Material[] { oldMaterials[0], SkinMaterial };
        PlayerMeshRenderer.materials = newMaterials;
    }

    #endregion

    #region Hats Customization

    public void CustomizePlayerHat(Hat hatItem, Transform parent)
    {
        if (hatItem == null) return;

        var newHat = Instantiate(hatItem.Model, parent);
        newHat.transform.localPosition = hatItem.RelativePos;
        newHat.transform.localEulerAngles = hatItem.RelativeRotation;
        LeanTween.scale(newHat, hatItem.RelativeScale, 0.5f);
    }

    public void CustomizePlayerHat(Hat hatItem, Transform parent, List<GameObject> list)
    {
        Debug.Log("CustomizePlayerHat");

        if (hatItem == null) return;

        var newHat = Instantiate(hatItem.Model, parent);
        list.Add(newHat);

        newHat.transform.localPosition = hatItem.RelativePos;
        newHat.transform.localEulerAngles = hatItem.RelativeRotation;

        LeanTween.scale(newHat, hatItem.RelativeScale, 0.5f);
    }

    #endregion

    #region Hats Customization

    public void CustomizePlayerAccessory(Accessory accessoryItem, Transform parent)
    {
        if (accessoryItem == null) return;

        var newHat = Instantiate(accessoryItem.Model, parent);
        newHat.transform.localPosition = accessoryItem.RelativePos;
        newHat.transform.localEulerAngles = accessoryItem.RelativeRotation;
        LeanTween.scale(newHat, accessoryItem.RelativeScale, 0.5f);
    }

    public void CustomizePlayerAccessory(Accessory accessoryItem, Transform parent, List<GameObject> list)
    {
        if (accessoryItem == null) return;

        var newHat = Instantiate(accessoryItem.Model, parent);
        list.Add(newHat);

        newHat.transform.localPosition = accessoryItem.RelativePos;
        newHat.transform.localEulerAngles = accessoryItem.RelativeRotation;

        LeanTween.scale(newHat, accessoryItem.RelativeScale, 0.5f);
    }

    #endregion

    public void ClearAllAccessories(Transform AccessoriesPlaceholder)
    {
        foreach (Transform child in AccessoriesPlaceholder)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearAllAccessories()
    {
        foreach (Transform child in AccessoriesPlaceholder)
        {
            if (child.gameObject.tag.Equals("Accessory"))
            {
                Destroy(child.gameObject);
            }         
        }
    }

    public void ClearAllHats()
    {
        foreach (Transform child in AccessoriesPlaceholder)
        {
            if (child.gameObject.tag.Equals("Hat"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ClearAllCustoms()
    {
        foreach (Transform child in AccessoriesPlaceholder)
        {
            Destroy(child.gameObject);
        }
    }

}
