using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceChecker : MonoBehaviour
{
    public static BaseSpaceChecker Instance;
    MeshRenderer getMeshRenderer;
    bool isAvailableToBuild;
    bool isNoSpace;
    List<GameObject> allNoSpaceGameObject = new List<GameObject>();

    public bool IsAvailableToBuild
    {
        get
        {
            return isAvailableToBuild;
        }
        set
        {
            isAvailableToBuild = value;
        }
    }

    public MeshRenderer GetMeshRenderer
    {
        get
        {
            return getMeshRenderer;
        }
        set
        {
            getMeshRenderer = value;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        getMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void MoveToTarget(Vector3 inputTransform)
    {
        transform.position = inputTransform;
        if (isNoSpace)
            return;
        getMeshRenderer.enabled = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Base")
        {
            Debug.Log("No space.");
            if (isAvailableToBuild == false)
                transform.position = new Vector3(0, 9999.0f, 0);
            getMeshRenderer.enabled = false;
            isNoSpace = true;
            if (!allNoSpaceGameObject.Contains(other.gameObject))
                allNoSpaceGameObject.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Base" && isAvailableToBuild)
        {
            Debug.Log("Move out from no space.");
            if (allNoSpaceGameObject.Contains(other.gameObject))
                allNoSpaceGameObject.Remove(other.gameObject);
            if (allNoSpaceGameObject.Count <= 0)
            {
                isNoSpace = false;
                getMeshRenderer.enabled = true;
                isAvailableToBuild = true;
            }
            Debug.Log(allNoSpaceGameObject.Count);
        }
    }

    public void ClearList()
    {
        allNoSpaceGameObject.Clear();
        isNoSpace = false;
    }
}
