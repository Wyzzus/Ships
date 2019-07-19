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
    public Vector3 LookPoint;
    public float LookRotatingT = 1f;
    public float ResetT = 1f;
    public bool Looking = true;

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

        if (!Looking)
            ResetCameraRotation();
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

    public void LookAtPoint(Vector3 point, bool OutsideCall = true)
    {
        LookPoint = point;
        Vector3 dir = (point - CameraComponent.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        CameraComponent.transform.rotation = Quaternion.Lerp(CameraComponent.transform.rotation, lookRot, LookRotatingT * Time.deltaTime);
        Looking = OutsideCall;
    }

    public void ResetLook()
    {
        Looking = false;
    }

    public void ResetCameraRotation()
    {
        if(CameraComponent.transform.localRotation != Quaternion.Euler(Vector3.zero))
            CameraComponent.transform.localRotation = Quaternion.Lerp(CameraComponent.transform.localRotation, Quaternion.Euler(Vector3.zero), ResetT * Time.deltaTime);
    }
}
