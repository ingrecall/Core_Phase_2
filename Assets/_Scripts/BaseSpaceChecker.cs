using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceChecker : MonoBehaviour
{
    #region Variable
    public static BaseSpaceChecker Instance;
    MeshRenderer getMeshRenderer;
    bool isAvailableToBuild;
    bool isNoSpace;
    List<GameObject> allNoSpaceGameObject = new List<GameObject>();
    #endregion

    #region Get set
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
    #endregion

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
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

    #region Function
    void Init()
    {
        getMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void MoveToTarget(Vector3 inputTransform)
    {
        if (PlaySceneController.Instance.IsDirSelecting)
            return;
        transform.position = inputTransform;
        if (isNoSpace)
            return;
        getMeshRenderer.enabled = true;
    }

    public void RotateToTarget(Vector3 inputTransform)
    {
        transform.LookAt(inputTransform);
        Debug.Log(transform.rotation.y);
    }

    public void ClearList()
    {
        allNoSpaceGameObject.Clear();
        isNoSpace = false;
    }

    public Quaternion CheckDir()
    {
        if (transform.rotation.y >= -0.3f && transform.rotation.y <= 0.3f)
            return Quaternion.Euler(0.0f, 0.0f, 0.0f);
        if (transform.rotation.y > 0.3f && transform.rotation.y < 0.9f)
            return Quaternion.Euler(0.0f, 90.0f, 0.0f);
        if (transform.rotation.y >= 0.9f && transform.rotation.y <= 1.0f)
            return Quaternion.Euler(0.0f, 180.0f, 0.0f);
        if (transform.rotation.y > -0.9f && transform.rotation.y < -0.3f)
            return Quaternion.Euler(0.0f, 270.0f, 0.0f);
        return Quaternion.identity;
    }
    #endregion
}
