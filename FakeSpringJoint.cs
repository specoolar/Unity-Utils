using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSpringJoint : MonoBehaviour
{
    public Transform connectedObject;

    [Header("Position")]
    public bool springPosition = true;
    public float spring = 100;
    public float damp = 1;
    public Vector3 positionOffset;

    [Header("Rotation")]
    public bool springRotation = true;
    public float angularSpring = 100;
    public float angularDamp = 1;
    public Vector3 rotationOffset;
    Quaternion rotationOffsetQ;

    Vector3 currentPos;
    Vector3 velocity;
    Vector3 targetPos;
    Quaternion currentRot = Quaternion.identity;
    Vector3 angularVelocity = Vector3.zero;
    Quaternion targetRot = Quaternion.identity;
    Quaternion zeroRot = Quaternion.identity;

    [ContextMenu("Apply offset")]
    public void ApplyOffset(){
        positionOffset = connectedObject.InverseTransformPoint(transform.position);
        rotationOffset = (Quaternion.Inverse(connectedObject.rotation)*transform.rotation).eulerAngles;
    }
    void Awake()
    {
        zeroRot = Quaternion.identity;
        rotationOffsetQ = Quaternion.Euler(rotationOffset);
    }

    private void Start() {
        currentPos = transform.position;
        currentRot = transform.rotation;
    }

    void FixedUpdate()
    {
        targetPos = connectedObject.TransformPoint(positionOffset);
        targetRot = connectedObject.rotation*rotationOffsetQ;

        if(springPosition){
            velocity += (targetPos - currentPos) * (spring * Time.fixedDeltaTime);
            currentPos += velocity * Time.fixedDeltaTime;
            velocity /= 1 + damp * Time.fixedDeltaTime;
        }else{
            currentPos = targetPos;
        }

        if(springRotation){
            Quaternion rotationDifference = targetRot * Quaternion.Inverse(currentRot);
            rotationDifference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
            Vector3 rotDegree = angleInDegrees * angularSpring * rotationAxis;

            angularVelocity += rotDegree * Mathf.Deg2Rad;
            currentRot *= Quaternion.Euler(transform.InverseTransformVector(angularVelocity) * Time.fixedDeltaTime);
            angularVelocity /= 1 + Time.fixedDeltaTime * angularDamp;
        }else{
            currentRot = targetRot;
        }

        transform.SetPositionAndRotation(currentPos, currentRot);
    }
}
