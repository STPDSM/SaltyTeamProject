using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasing : MonoBehaviour {

    public Transform _player;
    public Transform _loot;
    static Animator _anim;

	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Vector3.Distance(_player.position, this.transform.position) < 10)
        {
            Vector3 direction = _player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            _anim.SetBool("IsJoggingAround", false);
            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.1f);
                _anim.SetBool("PlayerOnSight", true);
                _anim.SetBool("IsInRange", false);
                _anim.SetBool("IsChasing", true);
            }
            else
            {
                _anim.SetBool("IsInRange", true);
                _anim.SetBool("IsChasing", false);
            }
        }
        else
        {
            _anim.SetBool("IsInRange", false);
            _anim.SetBool("IsChasing", false);
            _anim.SetBool("IsJoggingAround", true);
        }
    }
}
