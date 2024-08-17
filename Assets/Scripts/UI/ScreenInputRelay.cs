using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ScreenInputRelay : MonoBehaviour
{
    public LayerMask inputLayerMask = ~0;
    public int mouseRayDistance;

    public UnityEvent<Vector2> onCursorInput = new UnityEvent<Vector2>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(cursorRay, out hit, mouseRayDistance, inputLayerMask))
        {
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name + ": " + hit.textureCoord);
                onCursorInput.Invoke(hit.textureCoord);
            }


        }
    }
}
