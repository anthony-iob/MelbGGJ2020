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

    Vector3 whiskerSpawnPoint;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
        StartCoroutine(AvoidWalls());
    }

    IEnumerator AvoidWalls() {
        while(true) {
            RaycastHit hit;

            Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * whiskerRange, Color.green);

            whiskerSpawnPoint = controller.transform.position + new Vector3(0, whiskerForwardSpawnModifier, 0);

            if (Physics.SphereCast(whiskerSpawnPoint, whiskerRadius, controller.transform.forward, out hit, whiskerRange))
            {
                //Debug.Log("Spherecast hit " + hit.transform.gameObject.name);

                AvoidWall();
                yield return new WaitForSeconds(wallAvoidanceInterval);
            }

            yield return null;
        }
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine(maxHeadingChange);
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine(float headingChange)
    {
        var floor = Mathf.Clamp(transform.eulerAngles.y - headingChange, 0, 360);
        var ceil = Mathf.Clamp(transform.eulerAngles.y + headingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
        //Debug.Log("Target Rotation changed to: " + targetRotation);
    }

    void AvoidWall()
    {
        NewHeadingRoutine(wallAvoidanceTurnDegrees);
    }
}
