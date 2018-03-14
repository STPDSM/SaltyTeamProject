using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Loot : StateMachineBehaviour {

    private const float PathUpdateInterval = 0.25f; //Intervalle de rafraichissement

    private GameObject _aI;
    private GameObject _loot; //GameObject correspondant au loot qui est dans le champ de vision de l'IA.

    private float _lastUpdateTime = 0f; //Variable qui stocke quand la dernière update a été faite.

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_loot == null)
        {
            _loot = GameObject.FindGameObjectWithTag("Loot");
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > _lastUpdateTime + PathUpdateInterval)
        {
            _lastUpdateTime = Time.time;
            _aI.GetComponent<Rigidbody>().AddForce(_loot.transform.position);
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
