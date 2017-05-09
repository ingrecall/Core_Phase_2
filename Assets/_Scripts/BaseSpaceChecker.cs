using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpaceChecker : MonoBehaviour
{
    public static BaseSpaceChecker Instance;
    MeshRenderer getMeshRenderer;
    bool isAvailableToBuild;

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
        getMeshRenderer.enabled = false;
        transform.position = inputTransform;
        isAvailableToBuild = true;
        StartCoroutine(WaitThenShowBaseSelector());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Base")
        {
            Debug.Log("No space.");
            transform.position = new Vector3(0, 9999.0f, 0);
            isAvailableToBuild = false;
            return;
        }
    }

    public IEnumerator WaitThenShowBaseSelector()
    {
        yield return new WaitForSeconds(0.04f);
        if (isAvailableToBuild == false)
            yield break;
        Debug.Log("Show base selector ui.");
        PlaySceneController.Instance.AllGameObject[0].SetActive(true);
        getMeshRenderer.enabled = true;
    }
}
