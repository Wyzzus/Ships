using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsComponent : EntityComponent
{
    [Header("Aim")]
    public bool AimLeft = false;
    public bool AimRight = false;
    public GameObject LeftAimSight;
    public Vector3 LeftCenter;
    public Vector3 LeftScale;
    public GameObject RightAimSight;
    public Vector3 RightCenter;
    public Vector3 RightScale;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHideSights();
    }

    #region OnlyPlayerGraphics
    public void ShowAimLeft(bool value, Vector3 Center, Vector3 Scale)
    {
        AimLeft = value;
        LeftScale = Scale;
        LeftCenter = Center;
    }

    public void ShowAimRight(bool value, Vector3 Center, Vector3 Scale)
    {
        AimRight = value;
        RightScale = Scale;
        RightCenter = Center;
    }

    public void ShowHideAimSight(bool value, Vector3 Scale, Vector3 Center, GameObject AimSight)
    {
        if (value && !AimSight.activeSelf)
        {
            AimSight.SetActive(true);
            AimSight.transform.localPosition = Center;
            AimSight.transform.localScale = Scale;
        }
        else if (!value && AimSight.activeSelf)
        {
            AimSight.SetActive(false);
        }
    }

    public void ShowHideSights()
    {
        ShowHideAimSight(AimLeft, LeftScale, LeftCenter, LeftAimSight);
        ShowHideAimSight(AimRight, RightScale, RightCenter, RightAimSight);
    }
    #endregion
}
