using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Variable
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
        if (targetToShoot == null)
            return;
        if (isCanBeRotateWhenDetected && rotateGameObject != null && isOnlyGunRotate == false && isOnlyBarrelRotate == false)
        {
            var targetToRotation = Quaternion.LookRotation(-rotateGameObject.position - -targetToShoot[0].transform.position);
            rotateGameObject.rotation = Quaternion.Slerp(rotateGameObject.rotation, targetToRotation, rotationSpeed * Time.deltaTime);
        }
        else if (isCanBeRotateWhenDetected && gunGameObject != null && isOnlyGunRotate && isOnlyBarrelRotate == false)
        {
            var targetToRotation = Quaternion.LookRotation(-gunGameObject.position - -targetToShoot[0].transform.position);
            gunGameObject.rotation = Quaternion.Slerp(gunGameObject.rotation, targetToRotation, rotationSpeed * Time.deltaTime);
        }
        else if (isCanBeRotateWhenDetected && barrelGameObject != null && isOnlyGunRotate == false && isOnlyBarrelRotate)
        {
            var targetToRotation = Quaternion.LookRotation(-barrelGameObject.position - -targetToShoot[0].transform.position);
            barrelGameObject.rotation = Quaternion.Slerp(barrelGameObject.rotation, targetToRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, targetToShoot[0].transform.position) < shootAbleRange)
        {
            Debug.Log("Able to shoot.");
        }
        /*else
        {
            Debug.Log(Vector3.Distance(transform.position, targetToShoot.transform.position));
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
    }
    #endregion
}
