using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceChecker : MonoBehaviour
{
    #region Variable
    public static BaseSpaceChecker Instance;
    MeshRenderer getMeshRenderer01;
    MeshRenderer getMeshRenderer02;
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
            return getMeshRenderer01;
        }
        set
        {
            getMeshRenderer01 = value;
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
            getMeshRenderer01.enabled = false;
            getMeshRenderer02.enabled = false;
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
                getMeshRenderer01.enabled = true;
                getMeshRenderer02.enabled = true;
                isAvailableToBuild = true;
            }
            Debug.Log(allNoSpaceGameObject.Count);
        }
    }

    #region Function
    void Init()
    {
        getMeshRenderer01 = GetComponent<MeshRenderer>();
        getMeshRenderer02 = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    public void MoveToTarget(Vector3 inputTransform)
    {
        if (PlaySceneController.Instance.IsDirSelecting)
            return;
        transform.position = inputTransform;
        if (isNoSpace)
            return;
        getMeshRenderer01.enabled = true;
        getMeshRenderer02.enabled = true;
    }

    public void RotateToTarget(Vector3 inputTransform)
    {
        transform.LookAt(inputTransform);
        if ((transform.eulerAngles.y >= 315.0f && transform.eulerAngles.y <= 360.0f) || (transform.eulerAngles.y >= 0.0f && transform.eulerAngles.y <= 45.0f))
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        if (transform.eulerAngles.y > 45.0f && transform.eulerAngles.y < 135.0f)
            transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        if (transform.eulerAngles.y >= 135.0f && transform.eulerAngles.y <= 215.0f)
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        if (transform.eulerAngles.y > 215.0f && transform.eulerAngles.y < 315.0f)
            transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
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

    public void CancelBuildBase()
    {
        allNoSpaceGameObject.Clear();
        isNoSpace = false;
        getMeshRenderer01.enabled = false;
        getMeshRenderer02.enabled = false;
        isAvailableToBuild = false;
        transform.position = new Vector3(0, 9999.0f, 0);
    }

    public void CreatedBaseState()
    {
        getMeshRenderer01.enabled = false;
        getMeshRenderer02.enabled = false;
    }
    #endregion
}
