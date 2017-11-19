using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Variable
    public enum AllTowerType { Energy, MachineGun };
    public AllTowerType towerType;
    [SerializeField]
    bool isTestRange;
    bool isShoot;
    [SerializeField]
    float reloadTimer;
    float savedReloadTimer;
    [SerializeField]
    [Range(0, 100)]
    float rotationSpeed;
    [SerializeField]
    [Range(0, 100)]
    float shootAbleRange;
    [SerializeField]
    [Range(0, 100)]
    float detectRange;
    [SerializeField]
    [Range(0, 0.4f)]
    float minRotationX;
    [SerializeField]
    [Range(-0.4f, 0)]
    float maxRotationX;
    [SerializeField]
    [Range(0, 100)]
    float maxHeightDetectRange;
    List<GameObject> targetToShoot = new List<GameObject>();
    [SerializeField]
    Transform rotateGameObject;
    [SerializeField]
    Transform gunGameObject;
    [SerializeField]
    Transform barrelGameObject;
    [SerializeField]
    bool isCanBeRotateWhenDetected;
    [SerializeField]
    bool isOnlyGunRotate;
    [SerializeField]
    bool isOnlyBarrelRotate;
    [SerializeField]
    GameObject AirRangeGameObject;
    [SerializeField]
    GameObject RadarRangeGameObject;
    [SerializeField]
    GameObject ShootRangeGameObject;
    [SerializeField]
    GameObject myBulletGameObject;
    [SerializeField]
    Transform[] firePoint;
    #endregion

    void Awake()
    {
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (isShoot)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0.0f)
                isShoot = false;
        }
        if (isTestRange)
        {
            RadarRangeGameObject.transform.localScale = new Vector3(detectRange * 2, detectRange * 2, detectRange * 2);
            ShootRangeGameObject.transform.localScale = new Vector3(shootAbleRange / 5.0f, shootAbleRange / 5.0f, shootAbleRange / 5.0f);
        }
        if (targetToShoot.Count <= 0)
            return;
        if (isCanBeRotateWhenDetected && rotateGameObject != null && isOnlyGunRotate == false && isOnlyBarrelRotate == false)
        {
            var targetToRotation = Quaternion.LookRotation(-rotateGameObject.position - -targetToShoot[0].transform.position);
            targetToRotation.x = 0;
            targetToRotation.z = 0;
            rotateGameObject.rotation = Quaternion.Slerp(rotateGameObject.rotation, targetToRotation, rotationSpeed * Time.deltaTime);
        }
        //Debug.Log(gunGameObject.localRotation.x);
        if (gunGameObject.localRotation.x < minRotationX && gunGameObject.localRotation.x > maxRotationX)
        {
            var gunTargetToRotation = Quaternion.LookRotation(-gunGameObject.position - -targetToShoot[0].transform.position);
            gunGameObject.rotation = Quaternion.Slerp(gunGameObject.rotation, gunTargetToRotation, rotationSpeed * Time.deltaTime);
        }
        /*if (Vector3.Distance(transform.position, targetToShoot[0].transform.position) < shootAbleRange && isShoot == false)
        {
            var bulletLookAt = Quaternion.LookRotation(barrelGameObject.position - -barrelGameObject.forward * 100.0f);
            GameObject newBullet = Instantiate(myBulletGameObject, firePoint[0].position, bulletLookAt) as GameObject;
            newBullet.GetComponent<BulletController>().FireBullet(targetToShoot[0].transform, 1.0f, 10.0f);
            reloadTimer = savedReloadTimer;
            isShoot = true;
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            targetToShoot.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (targetToShoot.Contains(other.gameObject))
            targetToShoot.Remove(other.gameObject);
    }

    #region Function
    public void Init()
    {
        Debug.Log("Tower Controller Init.");
        SphereCollider newCollider = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        newCollider.radius = detectRange;
        newCollider.isTrigger = true;
        shootAbleRange = shootAbleRange * 10;
        RadarRangeGameObject.SetActive(true);
        RadarRangeGameObject.transform.localScale = new Vector3(detectRange * 2, detectRange * 2, detectRange * 2);
        ShootRangeGameObject.SetActive(true);
        ShootRangeGameObject.transform.localScale = new Vector3(shootAbleRange / 5.0f, shootAbleRange / 5.0f, shootAbleRange / 5.0f);
        savedReloadTimer = reloadTimer;
        if (towerType == AllTowerType.Energy)
        {
            PlaySceneController.Instance.EnergyChange(5.0f);
        }
    }
    #endregion
}
