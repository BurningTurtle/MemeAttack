using UnityEngine;

public class OShitWaddup : MonoBehaviour {

    public int damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Substract 2 health from Player if hit by oshitwaddup
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
        }
    }
}
