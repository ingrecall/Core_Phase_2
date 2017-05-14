using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneController : MonoBehaviour
{
    #region Variable
    public static PlaySceneController Instance;
    [SerializeField]
    float playerLife;
    float playerEnergy;
    [SerializeField]
    GameObject[] allBaseSelectorGroup;
    [SerializeField]
    GameObject[] allGameObject;
    [SerializeField]
    GameObject[] allBasePrefab;
    float saveY;
    int saveBaseType;
    int saveTowerLevel;
    bool isDirSelecting;
    float clickTimer;
    #endregion

    #region Get set
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

    public bool IsDirSelecting
    {
        get
        {
            return isDirSelecting;
        }
        set
        {
            isDirSelecting = value;
        }
    }
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (clickTimer > 0.0f)
            clickTimer -= Time.deltaTime;
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
                    isDirSelecting = true;
            }
        }
        if (Input.GetMouseButton(0) && isDirSelecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
                BaseSpaceChecker.Instance.RotateToTarget(hit.point);
        }
        if (Input.GetMouseButtonDown(1))
        {
            BaseSpaceChecker.Instance.CancelBuildBase();
            isDirSelecting = false;
        }
        if (clickTimer <= 0.0f && Input.GetMouseButtonUp(0) && BaseSpaceChecker.Instance.IsAvailableToBuild && BaseSpaceChecker.Instance.GetMeshRenderer.enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.collider.tag == "Ground")
                    CreateBase(saveBaseType);
            }
        }
        else if (clickTimer <= 0.0f && Input.GetMouseButtonUp(0) && BaseSpaceChecker.Instance.IsAvailableToBuild && !BaseSpaceChecker.Instance.GetMeshRenderer.enabled)
        {
            isDirSelecting = false;
            BaseSpaceChecker.Instance.IsAvailableToBuild = false;
        }
    }

    #region Function
    public void Init()
    {
        Debug.Log("Init from PlaySceneController.");
        playerLife = 30;
    }

    public void BaseSelected(int inputType)
    {
        if (BaseSpaceChecker.Instance.GetMeshRenderer.enabled)
            return;
        Debug.Log("Base Selected.");
        BaseSpaceChecker.Instance.ClearList();
        BaseSpaceChecker.Instance.IsAvailableToBuild = true;
        saveBaseType = inputType;
        clickTimer = 0.04f;
        for (int i = 0; i < allBaseSelectorGroup.Length; i++)
            allBaseSelectorGroup[i].SetActive(false);
    }

    public void CreateBase(int inputType)
    {
        if (playerEnergy >= 0)
        {
            isDirSelecting = false;
            BaseSpaceChecker.Instance.IsAvailableToBuild = false;
            BaseSpaceChecker.Instance.CreatedBaseState();
            playerEnergy -= 0;
            Debug.Log("Created base | Current Enerygy : " + playerEnergy);
            if (inputType == 1)
            {
                Debug.Log("Created Machine Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[0], BaseSpaceChecker.Instance.transform.position, BaseSpaceChecker.Instance.CheckDir()) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
                newBase.transform.GetChild(saveTowerLevel - 1).gameObject.SetActive(true);
            }
            else if (inputType == 2)
            {
                Debug.Log("Created Minigun Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[1], BaseSpaceChecker.Instance.transform.position, BaseSpaceChecker.Instance.CheckDir()) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
                newBase.transform.GetChild(saveTowerLevel - 1).gameObject.SetActive(true);
            }
            else if (inputType == 3)
            {
                Debug.Log("Created Anti Air Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[2], BaseSpaceChecker.Instance.transform.position, BaseSpaceChecker.Instance.CheckDir()) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
                newBase.transform.GetChild(saveTowerLevel - 1).gameObject.SetActive(true);
            }
            else if (inputType == 4)
            {
                Debug.Log("Created Missile Gun Tower base.");
                GameObject newBase = Instantiate(allBasePrefab[3], BaseSpaceChecker.Instance.transform.position, BaseSpaceChecker.Instance.CheckDir()) as GameObject;
                newBase.transform.position = new Vector3(newBase.transform.position.x, saveY, newBase.transform.position.z);
                newBase.transform.GetChild(saveTowerLevel - 1).gameObject.SetActive(true);
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

    public void OpenLevelGroup(GameObject inputGameObject)
    {
        inputGameObject.SetActive(!inputGameObject.activeSelf);
    }

    public void LevelSelector(int inputLevel)
    {
        saveTowerLevel = inputLevel;
    }
    #endregion
}
