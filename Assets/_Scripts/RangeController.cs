using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    [SerializeField]
    Material[] allMaterials;
    MeshRenderer getMeshRenderer;

    void Start()
    {
        getMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeRangeType(int inputType)
    {
        getMeshRenderer.material = allMaterials[inputType];
    }
}
