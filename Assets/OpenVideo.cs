using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class OpenVideo : MonoBehaviour
{



    public string url;
    string urlPlaceholder = "https://www.youtube.com/watch?v=";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [DllImport("__Internal")]
    private static extern void OpenToBlankWindow(string _url);

    public void OpenURL() {
        OpenToBlankWindow(urlPlaceholder + url);
    }

}
