using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currenHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for(int i = 0; i < wasEnabled.Length ;i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }
    /*void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(999);
        }
    }*/

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead)
            return;
        currenHealth -= _amount;

        Debug.Log(transform.name + " now has " + currenHealth + " health");
        if(currenHealth<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        //Disable Components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>(); ;
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is dead.");

        //Call respawn method
        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(gameManager.instance.MatchSettings.respawnTime);
        SetDefaults();
        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;

        Debug.Log(transform.name + " respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        currenHealth = maxHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>(); ;
        if (_col != null)
            _col.enabled = true;
    }

}
