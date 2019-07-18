using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEntity : Entity
{
    // Start is called before the first frame update
    public Camera CameraComponent;
    [Header ("Following")]
    public Transform Target;
    public float FollowingT = 1f;
    public float RotatingT = 1f;

    [Header("Camera Settings")]
    public float CameraDistance = 10f;
    public Transform Shoulder;
    public Vector3 ShoulderOffset;
    public Vector3 ShoulderRotation;

    public override void Start()
    {
        base.Start();
        SetupCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target)
            FollowTarget();

        if (CameraComponent && Shoulder)
            AdjustCamera();
    }

    public void SetupTarget(Transform Target)
    {
        this.Target = Target;
    }

    public void SetupCamera()
    {
        CameraComponent = GetComponentInChildren<Camera>();
    }

    public void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, FollowingT * Time.deltaTime);
        Vector3 rotVector = new Vector3(0, Target.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotVector), RotatingT * Time.deltaTime);
    }

    public void AdjustCamera()
    {
        CameraComponent.transform.localPosition = new Vector3(0, 0, -CameraDistance);
        Shoulder.transform.localPosition = ShoulderOffset;
        Shoulder.transform.localRotation = Quaternion.Euler(ShoulderRotation);
    }
}
