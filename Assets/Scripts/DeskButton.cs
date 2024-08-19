using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeskButton : MonoBehaviour
{
    public bool isHovering;
    public bool isPressed;
    public bool canPress = true;
    public GameObject pressablePart;
    private float initialOffset;
    public float hoverDownOffset;
    public float pressDownOffset;
    public UnityEvent onButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = pressablePart.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0) && isHovering)
        {
            onButtonPress.Invoke();
            SetButtonPressedPostion(pressDownOffset);
;
        }
        
        if (Input.GetMouseButtonUp(0))
        {

            if (isHovering)
            {
                SetButtonPressedPostion(hoverDownOffset);
            }
            else
            {
                SetButtonPressedPostion(initialOffset);
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 7;
        // Perform the raycast

        if (canPress)
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
        if (isHovering == false)
        {

            isHovering = true;
            SetButtonPressedPostion(hoverDownOffset);

        }
    }

    void OnMouseExit()
    {
        if (isHovering == true)
        {

            isHovering = false;
            SetButtonPressedPostion(initialOffset);


        }
    }


    void SetButtonPressedPostion(float offset)
    {
 
        pressablePart.transform.localPosition = new Vector3(pressablePart.transform.localPosition.x, offset, pressablePart.transform.localPosition.z);

    }
}
