using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInteractPopup : MonoBehaviour
{
    public GameObject player;
    public GameObject interactPopup;
    public float popupDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < popupDistance)
        {
            interactPopup.SetActive(true);
        }
        else {
            interactPopup.SetActive(false);
        }
    }
}
