using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public enum MaterialType
{
    Male,
    Female,
    Enemy
}

public enum ShapeType
{
    Sphere,
    Cube
}

public class FaceSwitch : MonoBehaviour
{
    public CharacterMaterials sphereMaterials, cubeMaterials;


    public void SwitchFace(GameObject go, ShapeType shapeType, MaterialType materialType, int faceIndex)
    {
        //default values
        var shapeTypeValue = sphereMaterials;
        var materialTypeValue = shapeTypeValue.maleMaterials;

        Debug.Log("Goes In!");


        switch (shapeType)
        {
            case ShapeType.Sphere:
                shapeTypeValue = sphereMaterials;
                break;

            case ShapeType.Cube:
                shapeTypeValue = cubeMaterials;
                break;

            default: break;
        }

        Debug.Log("Goes In! " + shapeTypeValue);

        switch (materialType)
        {
            case MaterialType.Male:
                materialTypeValue = shapeTypeValue.maleMaterials;
                break;

            case MaterialType.Female:
                materialTypeValue = shapeTypeValue.femaleMaterials;
                break;

            case MaterialType.Enemy:
                materialTypeValue = shapeTypeValue.enemyMaterials;
                break;

            default: break;
        }

         
        if (go != null)
        { 
            Debug.Log("Changing Face");
            Material[] existingMaterial = go.GetComponent<MeshRenderer>().materials;
            if (existingMaterial != null)
            {
                existingMaterial[0] = materialTypeValue[faceIndex];

                go.GetComponent<MeshRenderer>().materials = existingMaterial;
            } 
        }

    }


}
