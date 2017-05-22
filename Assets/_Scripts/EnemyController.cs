using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    #region Variable
    [SerializeField]
    bool isTestHp;
    [SerializeField]
    string enemyName;
    [SerializeField]
    float enemyHealthPoint;
    float enemyMaxHealthPoint;
    [SerializeField]
    float enemyShield;
    [SerializeField]
    float enemyMovementSpeed;
    EnemyHealthPointBarController getEnemyHealthPointBarController;
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (isTestHp)
            getEnemyHealthPointBarController.MyHealthPointBarFill.fillAmount = enemyHealthPoint / enemyMaxHealthPoint;
    }

    #region Function
    void Init()
    {
        enemyMaxHealthPoint = enemyHealthPoint;
    }

    public void SetGetEnemyHealthPointBarController(EnemyHealthPointBarController inputEnemyHealthPointBarController)
    {
        getEnemyHealthPointBarController = inputEnemyHealthPointBarController;
    }

    void TakeDamage(float inputDamage)
    {
        if (enemyHealthPoint < 0)
            return;
        enemyHealthPoint -= inputDamage;
        if (enemyHealthPoint <= 0.0f)
        {
            enemyHealthPoint = 0.0f;
            Destroy(gameObject);
        }
        getEnemyHealthPointBarController.MyHealthPointBarFill.fillAmount = enemyHealthPoint / enemyMaxHealthPoint;
    }
    #endregion
}
