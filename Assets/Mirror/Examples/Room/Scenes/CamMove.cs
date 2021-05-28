using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

      [Range (1,5)] public float hassasiyet = 1f;
	[Range (0,2)] public float hiz = 1f;


		
	
	void Update () {

        if (Input.GetMouseButton(1))
            transform.Rotate(-Input.GetAxis("Mouse Y") * hassasiyet, Input.GetAxis("Mouse X") * hassasiyet, 0);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
        transform.Translate(Input.GetAxis("Horizontal") * hiz, 0, Input.GetAxis("Vertical")*hiz);

        if (Input.GetKey("e"))
        {
           transform.position += new Vector3(0, hiz , 0);
        }

        if (Input.GetKey("q"))
        {
            transform.position -= new Vector3(0, hiz , 0);
        }
    }
}
