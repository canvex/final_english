using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake(){

    CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

    float screenWidthScale = Screen.width / canvasScaler.referenceResolution.x;
    float screenHeightScale = Screen.height / canvasScaler.referenceResolution.y;
  
    canvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 1 : 0;
}
}
