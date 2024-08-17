using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInputReceiver : MonoBehaviour
{
    public RectTransform canvasTransform;
    public ScreenInputRelay myScreen;

    private void Awake()
    {
        myScreen.onCursorInput.AddListener(MirrorScreenRelayInput);
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void MirrorScreenRelayInput(Vector2 hitTexCoord)
    {
        Debug.Log("TextureCoord: " + hitTexCoord);
    }
}
