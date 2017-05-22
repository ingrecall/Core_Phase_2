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
    float enemyArmorPoint;
    float enemyMaxArmorPoint;
    [SerializeField]
    float enemyEnergyArmorPoint;
    float enemyMaxEnergyArmorPoint;
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
        {
            getEnemyHealthPointBarController.MyHealthPointBarFill.fillAmount = enemyHealthPoint / enemyMaxHealthPoint;
            getEnemyHealthPointBarController.MyArmorPointBarFill.fillAmount = enemyArmorPoint / enemyMaxArmorPoint;
            getEnemyHealthPointBarController.MyEnergyArmorPointBarFill.fillAmount = enemyEnergyArmorPoint / enemyMaxEnergyArmorPoint;
        }
    }

    #region Function
    void Init()
    {
        enemyMaxHealthPoint = enemyHealthPoint;
        enemyMaxArmorPoint = enemyArmorPoint;
        enemyMaxEnergyArmorPoint = enemyEnergyArmorPoint;
    }

    public void SetGetEnemyHealthPointBarController(EnemyHealthPointBarController inputEnemyHealthPointBarController)
    {
        getEnemyHealthPointBarController = inputEnemyHealthPointBarController;
    }

    void TakeDamage(float inputDamage)
    {
        if (enemyEnergyArmorPoint > 0)
        {
            enemyEnergyArmorPoint -= inputDamage;
            if (enemyEnergyArmorPoint <= 0.0f)
                enemyEnergyArmorPoint = 0.0f;
        }
        else if (enemyArmorPoint > 0)
        {
            enemyArmorPoint -= inputDamage;
            if (enemyArmorPoint <= 0.0f)
                enemyArmorPoint = 0.0f;
        }
        else
        {
            if (enemyHealthPoint < 0)
                return;
            enemyHealthPoint -= inputDamage;
            if (enemyHealthPoint <= 0.0f)
            {
                enemyHealthPoint = 0.0f;
                Destroy(gameObject);
            }
        }
        getEnemyHealthPointBarController.MyHealthPointBarFill.fillAmount = enemyHealthPoint / enemyMaxHealthPoint;
        getEnemyHealthPointBarController.MyArmorPointBarFill.fillAmount = enemyArmorPoint / enemyMaxArmorPoint;
        getEnemyHealthPointBarController.MyEnergyArmorPointBarFill.fillAmount = enemyEnergyArmorPoint / enemyMaxEnergyArmorPoint;
    }
    #endregion
}
