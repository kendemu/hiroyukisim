using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSample : MonoBehaviour
{

    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float multiplier = 1f;



        if (Input.GetKey(KeyCode.LeftShift))
            multiplier = 4f;

        else if (Input.GetKey(KeyCode.RightShift))
            multiplier = 4f;


        this.transform.localPosition += this.transform.forward * -y * Time.deltaTime * multiplier + this.transform.right * -x * Time.deltaTime * multiplier;
        //this.transform.localRotation = Quaternion.Euler(gameObject.transform.localRotation.eulerAngles + new Vector3(0, x, 0) * 10f *  Time.deltaTime * multiplier);


        //float sensitivity = 5f;

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

        if (Input.GetKey(KeyCode.Space))
            this.GetComponent<Rigidbody>().AddForce(this.transform.up * 9.8f, ForceMode.Force);

        if (Input.GetKey(KeyCode.T))
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
}
