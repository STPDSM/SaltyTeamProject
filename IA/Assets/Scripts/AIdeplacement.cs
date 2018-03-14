using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdeplacement : MonoBehaviour {

    Animator anim;
    private RaycastHit Hit;
    private int _fighting; //Hash de l'animation Fighting
    private int _chasing; //Hash de l'animation PlayerOnSight.
    private int _looting; //Hash de l'animation LootOnSight.
    private int _JoggingAround; //Hash de l'animation isJoggingAround.

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 2
            ))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Hit, 3)
                &&
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Hit, 3))
                transform.Rotate(Vector3.up * Random.Range(135, 180));

            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Hit, 3))
                transform.Rotate(Vector3.up * Random.Range(45, 90));

            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Hit, 3))
                transform.Rotate(Vector3.up * -Random.Range(45, 90));

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
