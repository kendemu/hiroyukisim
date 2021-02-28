using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public GameObject tutorialPanel;
    public GameObject questionPanel;
    public GameObject popupPanel;

    bool questionFlag = false;
    bool tutorialFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            tutorialFlag = !tutorialFlag;
            tutorialPanel.SetActive(!tutorialFlag);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            questionFlag = true;
            questionPanel.SetActive(questionFlag);
            popupPanel.SetActive(!questionFlag);
        }
    }
}
