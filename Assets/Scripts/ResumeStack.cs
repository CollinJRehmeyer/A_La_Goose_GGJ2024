using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeStack : MonoBehaviour
{
    public List<ResumePage> pages = new List<ResumePage>();
    public EmployeeManager employeeManager;
    public GameObject ResumePagePrefab;
    public float distancebetweenPages = .02f;
    public float randomRotation = 10f;
    public BoxCollider boxCollider;
    public float boxColliderBufferHeight = .1f;
    public bool isHoveringOverStack;
    public bool canHighlight = true;
    public bool canAdd = true;
    public int pagesInQueue;
    public int startingPages;
    public bool isDragging;
    public bool pageIsHighlight;
    public ResumePage selectedPage;
    public Quaternion pageHighlightRotation = Quaternion.Euler(-35, -15, 0);
    public Vector3 pageHighlightOffset = new Vector3(0, .2f, 0);
    public Quaternion pageAcceptRotation = Quaternion.Euler(-35, 15, 0);
    public Vector3 pageAcceptOffset = new Vector3(.3f, 0, 0);
    public Quaternion pageRejectRotation = Quaternion.Euler(-35, -30, 0);
    public Vector3 pageRejectOffset = new Vector3(-.3f, 0, 0);
    public float distanceForAcceptedSwipe;
    public float maxSwipeDistance;

    private Vector3 initialMousePosition;


    // Start is called before the first frame update
    void Start()
    {
        TryAddPagesToStack(startingPages);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TryAddPagesToStack(1);
        }
        if (canAdd)
        {
            AddPagesFromQueue();
        }
        if (Input.GetMouseButtonDown(0) && pageIsHighlight)
        {
            if (selectedPage != null)
            {
                initialMousePosition = Input.mousePosition;
                isDragging = true;
                StopCoroutine(HighlightTopPage(selectedPage));
                Vector3 endPos = StackPostionOfTopPage() + pageHighlightOffset;
                Quaternion endRot = pageHighlightRotation;
                selectedPage.transform.position = endPos;
                selectedPage.transform.rotation = endRot;
            }

        }
        if (isDragging)
        {
            if (selectedPage != null)
            {
                Vector3 startPos = StackPostionOfTopPage() + pageHighlightOffset;
                Quaternion startRot = pageHighlightRotation;
                Vector3 endPos = StackPostionOfTopPage() + pageHighlightOffset;
                Quaternion endRot = pageHighlightRotation;
                Vector3 currentMousePosition = Input.mousePosition;
                float distanceX = currentMousePosition.x - initialMousePosition.x;
                if (distanceX < 0)
                {
                    endPos = StackPostionOfTopPage() + pageRejectOffset + pageHighlightOffset;
                    endRot = pageRejectRotation;
                }
                else
                {
                    endPos = StackPostionOfTopPage() + pageAcceptOffset + pageHighlightOffset;
                    endRot = pageAcceptRotation;
                }
                float percentToEnd = Mathf.Abs(distanceX) / maxSwipeDistance;
                selectedPage.transform.position = Vector3.Lerp(startPos, endPos, percentToEnd);
                selectedPage.transform.rotation = Quaternion.Lerp(startRot, endRot, percentToEnd);
            }


        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging == true)
            {
                isDragging = false;
                if (selectedPage != null)
                {
                    CheckDragPositionForAccept();

                }
            }
            

        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 7;
        // Perform the raycast

        if (canHighlight && !isDragging)
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

    public void CheckDragPositionForAccept()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float distanceX = currentMousePosition.x - initialMousePosition.x;
        //print("distance swiped: " + distanceX);
        if(Mathf.Abs(distanceX)> distanceForAcceptedSwipe)
        {
            if (distanceX < 0)
            {
                //print("draged to reject");
                RejectResume();
            }
            else
            {
                //print("dragged to accept");
                AcceptResume();
            }
            canAdd = true;
        }
    }

    void RejectResume()
    {
        pages.Remove(selectedPage);
        Destroy(selectedPage.gameObject);
        //print("reject resume");
    }
    void AcceptResume()
    {
        //print("accept resume");
        employeeManager.AddEmployee(selectedPage.employee, employeeManager.employees.Count);
        pages.Remove(selectedPage);
        Destroy(selectedPage.gameObject);
    }

    void OnMouseHover()
    {
        if (isHoveringOverStack == false && pages.Count>0)
        {
            ResumePage pageToMove = pages[pages.Count - 1];
            selectedPage = pageToMove;
            pageIsHighlight = true;
            isHoveringOverStack = true;
            StartCoroutine(HighlightTopPage(pageToMove));
            //print("hovering over stack");
        }
    }

    void OnMouseExit()
    {
        if (isHoveringOverStack == true && !isDragging && pages.Count > 0)
        {
            pageIsHighlight = false;
            isHoveringOverStack = false;
            StartCoroutine(UnhighlightTopPage(pages[pages.Count - 1]));
            //print("end hovering over stack");
        }
    }
    public bool TryAddPagesToStack(int numPages)
    {
        if (canAdd)
        {
            for (int i = 0; i < numPages; i++)
            {
                AddPageToStack();
            }
            return true;

        }
        else
        {
            QueuePagesforNextTry(numPages);
            return false;
        }
    }

    public void QueuePagesforNextTry(int numPages)
    {
        pagesInQueue += numPages;
    }
    public void AddPagesFromQueue()
    {
        int pagesToAdd = pagesInQueue;
        pagesInQueue = 0;
        TryAddPagesToStack(pagesToAdd);
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
            Employee e = new Employee();
            e.employeeSprite = employeeManager.employeeSprites[Random.Range(0, employeeManager.employeeSprites.Length)];
            spawnedPage.employee = e;
            
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
        Vector3 endPos = pageToMove.transform.position + pageHighlightOffset;
        Quaternion startRot = pageToMove.transform.rotation;
        Quaternion endRot = pageHighlightRotation;
        canAdd = false;


        while (elapsedTime < timeToMove)
        {
            if (pageToMove != null)
            {
                canHighlight = false;
                pageToMove.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
                pageToMove.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                canHighlight = true;
            }
            yield return null;
        }

        // Ensure the object reaches the final position
        pageToMove.transform.position = endPos;
        pageToMove.transform.rotation = endRot;
        canHighlight = true;
        

    }
    IEnumerator UnhighlightTopPage(ResumePage pageToMove)
    {
        pageIsHighlight = false;
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
            if (pageToMove != null)
            {
                canHighlight = false;
                pageToMove.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / timeToMove);
                pageToMove.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeToMove);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                canHighlight = true;
            }
            yield return null;
        }

        // Ensure the object reaches the final position
        pageToMove.transform.position = endPos;
        pageToMove.transform.rotation = endRot;
        canAdd = true;
        canHighlight = true;
    }
}
