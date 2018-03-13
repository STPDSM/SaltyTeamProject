using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private Vector3 _IAposition; //Position actuelle de l'IA. 
    private Animator _animator; 
    private int _playerOnSightHash; //Hash du booléen PlayerOnSight.
    private int _lootOnSightHash; //Hash du booléen LootOnSight.
    private int _isJoggingAroundHash; //Hash du booléen isJoggingAround.

    public Vector3 IAposition { get { return _IAposition; } } //Getter de la position.

	// Use this for initialization
	void Start ()
    {

        _IAposition = transform.position; //Set IApositon à la position acutelle de l'IA
        _animator = GetComponent(typeof(Animator)) as Animator;

        _playerOnSightHash = Animator.StringToHash("PlayerOnSight"); //Set le Hash de playerOnSight
        _lootOnSightHash = Animator.StringToHash("LootOnSight"); //Set le hash de lootOnsight
        _isJoggingAroundHash = Animator.StringToHash("isJoggingAround");
	}

    private void OnTriggerEnter(Collider collider)
    {
        // Si un joueur est a portée.
        if (collider.CompareTag("Player"))
        {
            //playerOnSight est set a true et isJoggingAround a false
            _animator.SetBool(_playerOnSightHash, true);
            _animator.SetBool(_isJoggingAroundHash, false);
        }
        //Si un loot est a portée
        else if (collider.CompareTag("Loot"))
        {//lootOnSight est set a true et isJoggingAround a false
            _animator.SetBool(_lootOnSightHash, true);
            _animator.SetBool(_isJoggingAroundHash, false);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        //Si le joueur n'est plus a portée
        if (collider.CompareTag("Player"))
        {//playerOnSight est set a false et isJoggingAround a true
            _animator.SetBool(_playerOnSightHash, false);
            _animator.SetBool(_isJoggingAroundHash, true);
        }
        else if (collider.CompareTag("Loot"))
        {//lootOnSight est set a false et isJoggingAround a true
            _animator.SetBool(_lootOnSightHash, false);
            _animator.SetBool(_isJoggingAroundHash, true);
        }
    }
}