using UnityEngine;

public class Knife : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Give Player 2 Damage if hit, then destroy knife
            collision.GetComponent<Player>().health -= 2;
            Destroy(this.gameObject);
        }
    }
}
