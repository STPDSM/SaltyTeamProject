using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdeplacement : MonoBehaviour {

    private RaycastHit Hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 5
            ))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Hit, 1) 
                && 
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Hit, 1))
                transform.Rotate(Vector3.up * Random.Range(135, 180));

            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Hit, 1))
                transform.Rotate(Vector3.up * Random.Range(45, 90));

            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Hit, 1))
                transform.Rotate(Vector3.up * - Random.Range(45,90));

            else
            {
                var tmp = true;
                if (tmp)
                {
                    transform.Rotate(Vector3.up * 90);
                    tmp = false;
                }
                else
                {
                    transform.Rotate(Vector3.up * -90);
                    tmp = true;
                }
            }
        }
	}
}
