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
    GameObject targetToShoot;
    [SerializeField]
    Transform rotateGameObject;
    [SerializeField]
    bool isCanBeRotateWhenDetected;
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
        if (isCanBeRotateWhenDetected && rotateGameObject != null)
        {
            var targetToRotation = Quaternion.LookRotation(-rotateGameObject.position - -targetToShoot.transform.position);
            //targetToRotation = Quaternion.Inverse(targetToRotation);
            //targetToRotation.x = -targetToRotation.x;
            //targetToRotation.y = -targetToRotation.y;
            //targetToRotation.z = -targetToRotation.z;
            rotateGameObject.rotation = Quaternion.Slerp(rotateGameObject.rotation, targetToRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, targetToShoot.transform.position) < shootAbleRange)
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
            targetToShoot = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetToShoot)
            targetToShoot = null;
    }

    #region Function
    public void Init()
    {
        Debug.Log("Tower Controller Init.");
        SphereCollider newCollider = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        newCollider.radius = detectRange;
        newCollider.isTrigger = true;
        shootAbleRange = shootAbleRange * 10;
    }
    #endregion
}
