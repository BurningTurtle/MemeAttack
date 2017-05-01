using UnityEngine;

public class Knife : MonoBehaviour {

    public bool back;

    private void Update()
    {
        if (back)
        {
            transform.Rotate(0, 0, 720 * Time.deltaTime);
        }
    }

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
