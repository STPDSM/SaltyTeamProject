using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string Player_Tag = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            //We hit something
            if(_hit.collider.tag == Player_Tag)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _playerId, int _damage)
    {
        Debug.Log(_playerId+"Player has been shot");

        Player _player =gameManager.GetPlayer(_playerId);
        _player.RpcTakeDamage(_damage);
    }

}
