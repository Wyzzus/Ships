using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsComponent : EntityComponent
{
    [Header("Aim")]
    public bool AimLeft = false;
    public bool AimRight = false;
    public GameObject LeftAimSight;
    public GameObject RightAimSight;
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
    public void ShowAimLeft(bool value)
    {
        AimLeft = value;
    }

    public void ShowAimRight(bool value)
    {
        AimRight = value;
    }

    public void ShowHideAimSight(bool value, GameObject AimSight)
    {
        if (value && !AimSight.activeSelf)
        {
            AimSight.SetActive(true);
        }
        else if (!value && AimSight.activeSelf)
        {
            AimSight.SetActive(false);
        }
    }

    public void ShowHideSights()
    {
        ShowHideAimSight(AimLeft, LeftAimSight);
        ShowHideAimSight(AimRight, RightAimSight);
    }
    #endregion
}
