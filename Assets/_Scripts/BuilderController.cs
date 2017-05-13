using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    #region Variable
    public static BuilderController Instance;
    public bool[] allUnlock;
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    #region Function
    public void Init()
    {
        Debug.Log("Builder Controller Init.");
    }
    #endregion
}
