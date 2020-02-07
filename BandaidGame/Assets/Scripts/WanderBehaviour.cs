using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WanderBehaviour : MonoBehaviour
{
    [Header("Wander Behaviour")]
    public float speed = 5;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    [Header("Whiskers/WallAvoidance")]
    public float whiskerRadius;
    public float whiskerRange;
    public float whiskerForwardSpawnModifier;
    public float wallAvoidanceTurnDegrees;

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
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * whiskerRange, Color.green);

        whiskerSpawnPoint = controller.transform.position + new Vector3(0, whiskerForwardSpawnModifier, 0);

        if (Physics.SphereCast(whiskerSpawnPoint, whiskerRadius, controller.transform.forward, out hit, whiskerRange))
        {
           // Debug.Log("Spherecast hit " + hit.transform.gameObject.name);
            AvoidWall();
        }
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    void AvoidWall()
    {
        // Debug.Log("avoiding wall");
        targetRotation = new Vector3(0, transform.eulerAngles.y + wallAvoidanceTurnDegrees, 0);
    }
}
