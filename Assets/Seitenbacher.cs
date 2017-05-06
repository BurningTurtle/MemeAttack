using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seitenbacher : MonoBehaviour {

    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only do this if Seitenbacher was hit by player. Otherwise, a Projectile for example could trigger this.
        if (player != null && collision.gameObject == player)
        {
            // Function provides Player with temporary DMG-Up. Since Seitenbacher object gets destroyed after pickup, function has to be in another script (Player Script).
            player.GetComponent<Player>().Seitenbacher();

            Destroy(this.gameObject);
        }
    }
}
