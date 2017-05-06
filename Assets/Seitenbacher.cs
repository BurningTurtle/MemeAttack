using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seitenbacher : MonoBehaviour {

    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            // Function provides Player with temporary DMG-Up. Since Seitenbacher object gets destroyed after pickup, function has to be in another script (Player Script).
            player.GetComponent<Player>().Seitenbacher();
            Destroy(this.gameObject);
        }
    }
}
