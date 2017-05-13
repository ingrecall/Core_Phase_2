using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneController : MonoBehaviour
{
    public static PlaySceneController Instance;
    [SerializeField]
    float playerLife;
    float playerEnergy;
    [SerializeField]
    GameObject[] allGameObject;
    [SerializeField]
    GameObject[] allBasePrefab;
    float saveY;
    int saveBaseType;

    public GameObject[] AllGameObject
    {
        get
        {
            return allGameObject;
        }
        set
        {
            allGameObject = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (BaseSpaceChecker.Instance.IsAvailableToBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.collider.tag == "Ground")
                {
                    //Debug.Log("Mouse down on : " + hit.collider.name + " && Where? : " + hit.point);
                    BaseSpaceChecker.Instance.MoveToTarget(hit.point);
                    saveY = hit.point.y;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && BaseSpaceChecker.Instance.IsAvailableToBuild && BaseSpaceChecker.Instance.GetMeshRenderer.enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.collider.tag == "Ground")
                    CreateBase(saveBaseType);
            }
        }
    }

    public void Init()
    {
        Debug.Log("Init from PlaySceneController.");
        playerLife = 30;
    }

    public void BaseSelected(int inputType)
    {
        Debug.Log("Base Selected.");
        BaseSpaceChecker.Instance.ClearList();
        BaseSpaceChecker.Instance.IsAvailableToBuild = true;
        saveBaseType = inputType;
    }

    public void CreateBase(int inputType)
    {
        if (playerEnergy >= 0)
        {
            BaseSpaceChecker.Instance.IsAvailableToBuild = false;
            playerEnergy -= 0;
            Debug.Log("Created base | Current Enerygy : " + playerEnergy);
            BaseSpaceChecker.Instance.IsAvailableToBuild = false;
            if (inputType == 1)
            {
                Debug.Log("Created Machine Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[0], BaseSpaceChecker.Instance.transform.position, Quaternion.identity) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
            }
            else if (inputType == 2)
            {
                Debug.Log("Created Minigun Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[1], BaseSpaceChecker.Instance.transform.position, Quaternion.identity) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
            }
        }
        else
            Debug.Log("Not Enough Player Energy | Current Enerygy : " + playerEnergy);
    }

    public void PlayerLifeSet(int inputLife, bool isPlus)
    {
        if (isPlus == false)
            Debug.Log("Player Life : " + playerLife + " | Incoming Damage : " + inputLife);
        else
            Debug.Log("Player Life : " + playerLife + " | Incoming Heal : " + inputLife);
        if (isPlus)
            playerLife += inputLife;
        else
        {
            playerLife -= inputLife;
            if (playerLife <= 0)
            {
                Debug.Log("Lose.");
            }
        }

    }
}
