using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    #region Variable
    [SerializeField]
    Transform parentTo;
    [SerializeField]
    GameObject[] allEnemy;
    [SerializeField]
    GameObject enemyHealthPointBar;
    [SerializeField]
    Transform[] allSpawnPosition;
    #endregion

    void Start()
    {
        StartCoroutine(StartWave01());
    }

    #region Function
    void Spawn(int inputEnemy, int inputSpawnPosition)
    {
        GameObject newEnemy = Instantiate(allEnemy[inputEnemy], allSpawnPosition[inputSpawnPosition].position, Quaternion.identity) as GameObject;
        GameObject newEnemyHealthPointBar = Instantiate(enemyHealthPointBar, allSpawnPosition[inputSpawnPosition].position, Quaternion.identity) as GameObject;
        newEnemyHealthPointBar.transform.SetParent(parentTo);
        newEnemyHealthPointBar.GetComponent<EnemyHealthPointBarController>().FollowToSet(newEnemy.transform);
        newEnemyHealthPointBar.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 0.04f, 1.0f);
        newEnemy.GetComponent<EnemyController>().SetGetEnemyHealthPointBarController(newEnemyHealthPointBar.GetComponent<EnemyHealthPointBarController>());
    }
    #endregion

    #region Enum
    IEnumerator StartWave01()
    {
        yield return new WaitForSeconds(2.0f);
        Spawn(0, 0);
    }
    #endregion
}
