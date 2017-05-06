using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneController : MonoBehaviour
{
    public float playerLife;
    public float playerEnergy;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Init()
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
