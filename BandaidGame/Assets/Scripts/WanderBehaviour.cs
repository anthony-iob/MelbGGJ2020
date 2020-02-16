using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WanderBehaviour : MonoBehaviour
{
    [Header("Wander Behaviour")]
    public float speed;
    public float directionChangeInterval;
    public float maxHeadingChange;

    [Header("Whiskers/WallAvoidance")]
    public float whiskerRadius;
    public float whiskerRange;
    public float whiskerForwardSpawnModifier;
    public float wallAvoidanceTurnDegrees;
    public float wallAvoidanceInterval;

    CharacterController controller;
    Vector3 targetRotation;
    Vector3 currentAngle;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        var heading = Random.Range(0, 360);
        currentAngle = new Vector3(0, heading, 0);
        transform.eulerAngles = currentAngle;

        StartCoroutine(NewHeading());
        StartCoroutine(AvoidWalls());
    }

    IEnumerator AvoidWalls() {
        while(true) {
            RaycastHit hit;

            Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * whiskerRange, Color.green);
            var direction = (controller.velocity.normalized * whiskerForwardSpawnModifier);
            var whiskerSpawnPoint = controller.transform.position + direction;

            if (Physics.SphereCast(whiskerSpawnPoint, whiskerRadius, direction, out hit, whiskerRange))
            {
                Debug.Log("Spherecast hit " + hit.transform.gameObject.name);

                AvoidWall();
                yield return new WaitForSeconds(wallAvoidanceInterval);
            }

            yield return null;
        }
    }

    void Update()
    {
        currentAngle = Vector3.Slerp(currentAngle, targetRotation, Time.deltaTime * directionChangeInterval);
        transform.eulerAngles = currentAngle;
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            NewRandomHeadingRoutine(maxHeadingChange);
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewRandomHeadingRoutine(float headingChange)
    {
        var floor = currentAngle.y - headingChange;
        var ceil = currentAngle.y + headingChange;
        var heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
        // Debug.Log("Target Rotation changed to: " + targetRotation);
    }

    void NewHeadingRoutine(float headingChange)
    {
        var heading = currentAngle.y + headingChange;
        targetRotation = new Vector3(0, heading, 0);
        Debug.Log("Avoiding, Target Rotation changed to: " + targetRotation);
    }

    void AvoidWall()
    {
        NewHeadingRoutine(wallAvoidanceTurnDegrees);
    }
}
