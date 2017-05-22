using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    #region Variable
    [SerializeField]
    Material[] allMaterials;
    MeshRenderer getMeshRenderer;
    #endregion

    void Start()
    {
        getMeshRenderer = GetComponent<MeshRenderer>();
    }

    #region Function
    public void ChangeRangeType(int inputType)
    {
        getMeshRenderer.material = allMaterials[inputType];
    }
    #endregion
}
