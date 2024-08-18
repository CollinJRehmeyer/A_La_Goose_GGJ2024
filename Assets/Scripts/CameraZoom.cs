using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera zoomDummyCamera;
    public Camera thisCamera;
    public Vector3 defaultPos;
    public Quaternion defaultRot;
    public Vector3 zoomPos;
    public Quaternion zoomRot;
    public float defaultFOV;
    public float zoomFOV;
    void Start()
    {
        if (GetComponent<Camera>() != null)
        {
            thisCamera = GetComponent<Camera>();
            defaultFOV = thisCamera.fieldOfView;
        }
        else
        {
            print("CameraZoom must be on a camera object");
        }
        if(zoomDummyCamera != null)
        {
            print("using dummy camera values for zoom");
            zoomPos = zoomDummyCamera.gameObject.transform.position;
            zoomRot = zoomDummyCamera.gameObject.transform.rotation;
            zoomFOV = zoomDummyCamera.fieldOfView;

        }
        else
        {
            print("using user specified values for zoom");
        }
        defaultPos = gameObject.gameObject.transform.position;
        defaultRot = gameObject.gameObject.transform.rotation;




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveToZoom(.1f));
        }
        if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(MoveToDefault(.1f));

        }
    }
    IEnumerator MoveToZoom(float timeToMove)
    {
        float elapsedTime = 0;
        Vector3 startPos = gameObject.transform.position;
        Vector3 endPos = zoomPos;
        Quaternion startRot = gameObject.transform.rotation;
        Quaternion endRot = zoomRot;
        float startFOV = thisCamera.fieldOfView;
        float endFOV = zoomFOV;


        while (elapsedTime < timeToMove)
        {

            gameObject.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
            gameObject.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
            thisCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the object reaches the final position
        gameObject.transform.position = endPos;
        gameObject.transform.rotation = endRot;
        thisCamera.fieldOfView = endFOV;


    }
    IEnumerator MoveToDefault(float timeToMove)
    {
        float elapsedTime = 0;
        Vector3 startPos = gameObject.transform.position;
        Vector3 endPos = defaultPos;
        Quaternion startRot = gameObject.transform.rotation;
        Quaternion endRot = defaultRot;
        float startFOV = thisCamera.fieldOfView;
        float endFOV = defaultFOV;


        while (elapsedTime < timeToMove)
        {

            gameObject.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
            gameObject.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
            thisCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the object reaches the final position
        gameObject.transform.position = endPos;
        gameObject.transform.rotation = endRot;
        thisCamera.fieldOfView = endFOV;
    }
}
