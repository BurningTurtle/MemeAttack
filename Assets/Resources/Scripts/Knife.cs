using UnityEngine;

public class Knife : MonoBehaviour {

    public bool back;
    public int damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Rotate if the knife is on its way back to the player.
        if (back)
        {
            transform.Rotate(0, 0, 720 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Give Player 2 Damage on hit, then destroy knife.
            collision.GetComponent<Player>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
