using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoggingAround : StateMachineBehaviour {

    private const float PathUpdateInterval = 0.25f; //Intervalle de rafraichissement

    private GameObject _aI;
    private RaycastHit _hit;
    private int speed;

    private float _lastUpdateTime = 0f; //Variable qui stocke quand la dernière update a été faite.

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
     //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aI.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Physics.Raycast(_aI.transform.position, _aI.transform.TransformDirection(Vector3.forward), out _hit, 5
            ))
        {
            if (Physics.Raycast(_aI.transform.position, _aI.transform.TransformDirection(Vector3.right), out _hit, 1)
                &&
                Physics.Raycast(_aI.transform.position, _aI.transform.TransformDirection(Vector3.left), out _hit, 1))
                _aI.transform.Rotate(Vector3.up * Random.Range(135, 180));

            else if (Physics.Raycast(_aI.transform.position, _aI.transform.TransformDirection(Vector3.left), out _hit, 1))
                _aI.transform.Rotate(Vector3.up * Random.Range(45, 90));

            else if (Physics.Raycast(_aI.transform.position, _aI.transform.TransformDirection(Vector3.right), out _hit, 1))
                _aI.transform.Rotate(Vector3.up * -Random.Range(45, 90));

            else
            {
                var tmp = true;
                if (tmp)
                {
                    _aI.transform.Rotate(Vector3.up * 90);
                    tmp = false;
                }
                else
                {
                    _aI.transform.Rotate(Vector3.up * -90);
                    tmp = true;
                }
            }
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
