﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthPointBarController : MonoBehaviour
{
    #region Variable
    Image myHealthPointBarFill;
    [SerializeField]
    Transform followTo;
    RectTransform myRectTransform;
    #endregion

    #region Get set
    public Image MyHealthPointBarFill
    {
        get
        {
            return myHealthPointBarFill;
        }
        set
        {
            myHealthPointBarFill = value;
        }
    }
    #endregion

    void Awake()
    {
        myHealthPointBarFill = transform.GetChild(0).GetComponent<Image>();
        myRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (followTo != null)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(followTo.position);
            screenPoint.y += 15.0f;
            myRectTransform.position = screenPoint;
        }
    }

    #region Function
    public void FollowToSet(Transform inputTransform)
    {
        followTo = inputTransform;
    }
    #endregion
}
