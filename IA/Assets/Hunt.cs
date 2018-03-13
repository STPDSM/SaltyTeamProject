using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunt : StateMachineBehaviour {

    private const float PathUpdateInterval = 0.3f; //Intervalle de rafraichissement

    private GameObject _aI;
    private GameObject _player; //GameObject correspondant a un joueur
    private Vector3 _move;

    private float _lastUpdateTime = 0f; //Variable qui stocke le temps à la dernière update


	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
	}

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    if (Time.time > _lastUpdateTime + PathUpdateInterval)
        {
            _lastUpdateTime = Time.time;
            _aI.GetComponent<Rigidbody>().AddForce(_player.transform.position);
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
