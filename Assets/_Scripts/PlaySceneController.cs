using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneController : MonoBehaviour
{
    float playerLife;
    float playerEnergy;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CreateBase()
    {
        if (playerEnergy >= 10)
        {
            playerEnergy -= 10;
            //Do create base here.
        }
    }

    public void PlayerLifeSet(int inputLife, bool isPlus)
    {
        if (isPlus)
            playerLife += inputLife;
        else
            playerLife -= inputLife;
    }
}
