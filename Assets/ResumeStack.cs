using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeStack : MonoBehaviour
{
    public List<ResumePage> pages = new List<ResumePage>();
    public GameObject ResumePagePrefab;
    public float distancebetweenPages = .02f;
    public float randomRotation = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            AddPageToStack();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
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

    void OnMouseHover()
    {
        // Code to run when the mouse is hovering over the GameObject
        Debug.Log("Mouse is over " + gameObject.name);
    }

    void OnMouseExit()
    {
        // Code to run when the mouse is no longer hovering over the GameObject
        Debug.Log("Mouse exited " + gameObject.name);
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
}
