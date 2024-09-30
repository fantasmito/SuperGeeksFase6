using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;
 
[Serializable]
public class IntEvent : UnityEvent<int> { }
 
[Serializable]
public class InputEvent : UnityEvent<float, float> { }
 
public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 10;
    [SyncVar]
    public int coins;
    [SyncVar]
    public Color playerColor;
    public IntEvent OnCoinCollect;
    public InputEvent OnDirectionChanged;
 
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = playerColor;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddPlayerListener(this);
    }
 
    [ClientRpc]
    void TalkToAll()
    {
        Debug.Log("If this works, gimme 100 hundred dollars");
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Test Mensage (IM BECOMING CRAZYYYY HAHSHAFHAGHAHGA)");
            TalkToAll();
        }
 
 
        if (isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
 
            OnDirectionChanged.Invoke(inputX, inputY);
 
            if (inputX != 0 && inputY != 0)
            {
                speed = 5;
            }
            else
            {
                speed = 10;
            }
 
            rb.velocity = new Vector2(inputX, inputY) * speed;
        }
    }
 
    [Server]
    public void AddCoins()
    {
        coins += 1;
        OnCoinCollect.Invoke(coins);
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AddCoins();
            MyNetworkManager.spawnedCoins--;
            Destroy(other.gameObject);
        }
    }
}