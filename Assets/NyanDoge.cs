using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanDoge : MonoBehaviour
{


    // For rotation around player
    private float rotateSpeed = 1f;
    private float radius = 2f;
    private float _angle;
    private Vector2 _centre;

    // Stuff for NyanDoge
    private int health = 500;
    private GameObject player;
    private bool alive = true;
    private int speed = 200;
    private SpriteRenderer sr;

    // Attacks
    private bool canShootNormal = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        _centre = player.transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            // Rotate around player at all times
            _centre = player.transform.position;
            _angle += rotateSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * radius;
            transform.position = _centre + offset;
            Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            playerVector.Normalize();

            if (playerVector != Vector2.zero)
            {
                float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            if(health > 300)
            {
                if (canShootNormal)
                {
                    StartCoroutine(shootNormal());
                }
            }

            if(health <= 300)
            {

            }
        }
    }

    IEnumerator shootNormal()
    {
        canShootNormal = false;
        FindObjectOfType<PopTartLauncher>().launchPopTart();
        FindObjectOfType<WoofSpawner>().spawnWoof();
        yield return new WaitForSeconds(1);
        canShootNormal = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            health -= player.GetComponent<Player>().damage;

            if(health <= 0)
            {
                //StartCoroutine(die());
            }
        }
    }
}
