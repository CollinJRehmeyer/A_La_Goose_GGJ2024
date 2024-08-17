using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenInputReceiver : MonoBehaviour
{
    public RectTransform canvasTransform;
    public ScreenInputRelay myScreen;
    private GraphicRaycaster uiRaycaster;

    private void Awake()
    {
        myScreen.onCursorInput.AddListener(MirrorScreenRelayInput);
        uiRaycaster = GetComponentInChildren<GraphicRaycaster>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void MirrorScreenRelayInput(Vector2 hitTexCoord)
    {
        //Debug.Log("Hello from Receiver - TextureCoord: " + hitTexCoord);
        Vector3 mousePos = new Vector3(canvasTransform.sizeDelta.x * hitTexCoord.x,
            canvasTransform.sizeDelta.y * hitTexCoord.y, 0f);

        PointerEventData receiverMouseEvent = new PointerEventData(EventSystem.current);
        receiverMouseEvent.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(receiverMouseEvent, results);

        bool mouseDown = Input.GetMouseButtonDown(0);
        bool mouseUp = Input.GetMouseButtonUp(0);

        foreach (RaycastResult result in results)
        {

            //Debug.Log(result.gameObject.name);
            if (mouseDown)
            {
                //Debug.Log("Pointer down on: " + result.gameObject.name);
                ExecuteEvents.Execute(result.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(result.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
            else if (mouseUp)
            {
                //Debug.Log("Pointer up on: " + result.gameObject.name);
                ExecuteEvents.Execute(result.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }

        }
    }
}
