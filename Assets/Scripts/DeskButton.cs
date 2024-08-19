using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DeskButton : MonoBehaviour
{
    public bool isHovering;
    public bool isPressed;
    public bool canPress = true;
    public GameObject pressablePart;
    private float initialOffset;
    public float hoverDownOffset;
    public float pressDownOffset;
    public StudioEventEmitter click;
    public StudioEventEmitter release;
    public Material activatedMaterial;
    public bool useCurrentMaterialAsActivatedMaterial = true;
    public Material deactivatedMaterial;
    public MeshRenderer buttonMesh;
    public bool hideLabelOnDeactivate;
    public bool showLabelOnActivate;
    public GameObject label;
    public UnityEvent onButtonPress;


    // Start is called before the first frame update
    void Start()
    {
        initialOffset = pressablePart.transform.localPosition.y;
        if (useCurrentMaterialAsActivatedMaterial)
        {
            activatedMaterial = buttonMesh.material;
        }
        SetButtonActive(canPress);
           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetButtonActive(!canPress);
        }
        if (canPress)
        {
            if (Input.GetMouseButtonDown(0) && isHovering)
            {
                onButtonPress.Invoke();
                click.Play();
                SetButtonPressedPostion(pressDownOffset);
                isPressed = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isHovering)
                {
                    if (isPressed)
                    {
                        release.Play();
                        isPressed = false;
                    }
                    SetButtonPressedPostion(hoverDownOffset);
                }

            }

        }
       
        
        if (Input.GetMouseButtonUp(0))
        {
            if (isHovering &&canPress)
            {
                if (isPressed)
                {
                    release.Play();
                    isPressed = false;
                }
                SetButtonPressedPostion(hoverDownOffset);
            }
            //else
            //{
            //    if (canPress)
            //    {
            //        SetButtonPressedPostion(initialOffset);
            //    }
                
                
            //}
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 7;
        // Perform the raycast

        //if (canPress)
        //{
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
        //}

    }
    void OnMouseHover()
    {
        if (isHovering == false)
        {
            if (canPress)
            {
                SetButtonPressedPostion(hoverDownOffset);
            }
            isHovering = true;

        }
    }

    void OnMouseExit()
    {
        if (isHovering == true)
        {

            if (isPressed)
            {
                release.Play();
                isPressed = false;
            }
            isHovering = false;
            if (canPress)
            {
                SetButtonPressedPostion(initialOffset);

            }


        }
    }


    void SetButtonPressedPostion(float offset)
    {
 
        pressablePart.transform.localPosition = new Vector3(pressablePart.transform.localPosition.x, offset, pressablePart.transform.localPosition.z);

    }
    public void SetButtonActive(bool allowPress)
    {
        canPress = allowPress;
        if (canPress)
        {
            buttonMesh.material = activatedMaterial;
            if (showLabelOnActivate)
            {
                label.SetActive(true);
            }
            if (isHovering)
            {
                SetButtonPressedPostion(hoverDownOffset);
            }
            else
            {
                SetButtonPressedPostion(initialOffset);
            }


        }
        else
        {
            buttonMesh.material = deactivatedMaterial;
            if (hideLabelOnDeactivate)
            {
                label.SetActive(false);
            }
            SetButtonPressedPostion(pressDownOffset);
        }

    }

}
