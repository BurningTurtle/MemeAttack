using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleProjectile : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private int damage = 5;

    private VillainArenaController villainArenaCtrl;

    // Use this for initialization
    void Start()
    {
        villainArenaCtrl = FindObjectOfType<VillainArenaController>();
        player = GameObject.Find("Player");
        StartCoroutine(die());
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        playerVector.Normalize();

        if (playerVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    void Update()
    {
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(villainArenaCtrl.GetComponent<VillainArenaController>().bossBeaten == false)
        {
            if (collision.tag == "Player")
            {
                if (collision.GetComponent<Player>().readyForDamage)
                {
                    collision.GetComponent<Player>().health -= damage;
                    collision.GetComponent<Player>().GetReadyForDamage();
                }
            }
        }
        Destroy(this.gameObject);
    }
}
