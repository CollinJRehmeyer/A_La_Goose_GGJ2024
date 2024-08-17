using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeStack : MonoBehaviour
{
    public List<ResumePage> pages = new List<ResumePage>();
    public GameObject ResumePagePrefab;
    public float distancebetweenPages = .02f;
    public float randomRotation = 10f;
    public BoxCollider boxCollider;
    public float boxColliderBufferHeight = .1f;
    public bool isHoveringOverStack;
    public bool canHighlight = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            AddPageToStack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddPageToStack();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 7;
        // Perform the raycast
        if (canHighlight)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // Check if the object hit by the raycast is this GameObject
                if (hit.transform == transform)
                {
                    OnMouseHover();
                }
                else
                {
                    OnMouseExit();
                }
            }
            else
            {
                OnMouseExit();
            }
        }
    
    }

    void OnMouseHover()
    {
        if(isHoveringOverStack == false)
        {
            isHoveringOverStack = true;
            StartCoroutine(HighlightTopPage(pages[pages.Count-1]));
            print("hovering over stack");
        }
    }

    void OnMouseExit()
    {
        if (isHoveringOverStack == true)
        {
            isHoveringOverStack = false;
            StartCoroutine(UnhighlightTopPage(pages[pages.Count - 1]));
            print("end hovering over stack");
        }
    }

    public void AddPageToStack()
    {
        Vector3 spawnPosition = this.transform.position;
        Quaternion originalRotation = this.transform.rotation;
        float randomYRotation = Random.Range(-10f, 10f);
        // Create a Quaternion with this random Y rotation
        Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);
        // Combine with the original rotatio
        Quaternion finalRotation = originalRotation * randomRotation;
        GameObject spawnedResumeObject = Instantiate(ResumePagePrefab, spawnPosition, finalRotation);
        ResumePage spawnedPage = spawnedResumeObject.GetComponent<ResumePage>();
        if (spawnedPage != null)
        {
            spawnedPage.employee = new Employee();
            spawnedPage.PopulatePage();
            pages.Add(spawnedPage);
            MovePagesInStack(false);
        }
        else
        {
            print("Couldn't find ResumePage on spawned resume");
        }
        boxCollider.size = new Vector3(boxCollider.size.x, boxColliderBufferHeight + (distancebetweenPages * pages.Count), boxCollider.size.z);
        boxCollider.center = new Vector3(boxCollider.center.x, pages.Count * distancebetweenPages / 2, boxCollider.center.z);
    }

    public void MovePagesInStack(bool oldestOnTop)
    {
        int i = 0;
        foreach (ResumePage p in pages)
        {
            int numPages = pages.Count;
            int orderFromBottom = i;
            if (oldestOnTop)
            {
                orderFromBottom = numPages - i;
            }
            p.transform.position = this.transform.position + new Vector3(0, orderFromBottom * distancebetweenPages, 0);
            i++;
        }
    }
    private Vector3 StackPostionOfTopPage()
    {
       return this.transform.position + new Vector3(0, pages.Count * distancebetweenPages, 0);
    }

    IEnumerator HighlightTopPage(ResumePage pageToMove)
    {
        float timeToMove = .1f;
        float elapsedTime = 0;
        Vector3 startPos = pageToMove.transform.position;
        Vector3 endPos = pageToMove.transform.position + new Vector3(0, .2f, 0);
        Quaternion startRot = pageToMove.transform.rotation;
        Quaternion endRot = Quaternion.Euler(-35, -15, 0);


        while (elapsedTime < timeToMove)
        {
            canHighlight = false;
            pageToMove.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
            pageToMove.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the final position
        pageToMove.transform.position = endPos;
        pageToMove.transform.rotation = endRot;
        canHighlight = true;

    }
    IEnumerator UnhighlightTopPage(ResumePage pageToMove)
    {
        float timeToMove = .1f;
        float elapsedTime = 0;
        Vector3 startPos = pageToMove.transform.position;
        Vector3 endPos = StackPostionOfTopPage();
        Quaternion startRot = pageToMove.transform.rotation;
        Quaternion stackRotation = this.transform.rotation;
        float randomYRotation = Random.Range(-10f, 10f);
        // Create a Quaternion with this random Y rotation
        Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);
        // Combine with the original rotatio
        Quaternion endRot = stackRotation * randomRotation;


        while (elapsedTime < timeToMove)
        {
            canHighlight = false;
            pageToMove.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
            pageToMove.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the final position
        pageToMove.transform.position = endPos;
        pageToMove.transform.rotation = endRot;
        canHighlight = true;
    }
}
