using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public static BuilderController Instance;
    public GameObject Base;
    public GameObject MachineGunTowerLv1;
    public GameObject MiniGunTowerLv1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Init()
    {
    }
}
