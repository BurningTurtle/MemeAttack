using UnityEngine;
using System.Collections;

public class DatBoi : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    public float speed;
    public float shootPower;
    public float range;
    public int health = 10;

    // Keep track of which direction DatBoi is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    [SerializeField]private GameObject yen1, yen5, yen10, yen50;

    [SerializeField] private GameObject oshitwaddupPrefab;
    private GameObject oshitwaddup;
    public bool canShootNext;

    // Stuff for Spawning bois
    [SerializeField] GameObject datBoiPrefab;
    GameObject[] bois;
    GameObject boi1, boi2, boi3, boi4;
    bool canSpawnNext = true;

    private SpriteRenderer sr;
    private Animator anim;

    // For It's time to stop
    public bool stop = false;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;
    private SoundManager soundMan;

    private void Start()
    {
        player = GameObject.Find("Player");
        lastPosition = transform.position.x;
        canShootNext = true;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        statue = GameObject.Find("datboiStatue1");
        soundMan = GameObject.FindObjectOfType<SoundManager>();
    }

    private void FixedUpdate()
    {
        bois = GameObject.FindGameObjectsWithTag("DatBoi");

        if (alive && !stop)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            // Get deltaX for current position.
            deltaX = lastPosition - transform.position.x;

            // Last position for the next frame is the current position from now.
            lastPosition = transform.position.x;

            // If DatBoi is moving right
            if(deltaX < 0)
            {
                // Flip DatBoi
                sr.flipX = true;
            }
            else if (deltaX > 0)
            {
                // Flip DatBoi back to normal
                sr.flipX = false;
            }

            // Distance between DatBoi and player
            Vector2 trueDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float combinedDistance = Mathf.Abs(trueDistance.x) + Mathf.Abs(trueDistance.y);

            // If DatBoi's distance to the player is smaller than 7 [...] then shoot
            if (combinedDistance < 7 && canShootNext)
            {
                StartCoroutine(shoot(playerVector));
                soundMan.playAudioClip("MainEnemyProjectile");
            }

            // Spwan new Dat Bois if there is just one in scene (his special ability).
            if (bois.Length < 2 && canSpawnNext == true)
            {
                StartCoroutine(SpawnDemBois());
                canSpawnNext = false;
            }
        }

        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
        }

    }

    IEnumerator SpawnDemBois()
    {
        // Give Player time to kill single DatBoi. Prevents Endless Spawning.
        yield return new WaitForSeconds(1.5f);

        soundMan.playAudioClip("CryForHelp");

        // Cool animation here.
        Debug.Log("Here Come Dem Bois");

        // Instantiate them.
        boi1 = Instantiate(datBoiPrefab) as GameObject;
        boi2 = Instantiate(datBoiPrefab) as GameObject;
        boi3 = Instantiate(datBoiPrefab) as GameObject;
        boi4 = Instantiate(datBoiPrefab) as GameObject;

        // Later they're spawned at the Enemy's entrance.
        boi1.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        boi2.transform.position = new Vector2(transform.position.x - 1, transform.position.y + 1);
        boi3.transform.position = new Vector2(transform.position.x + 1, transform.position.y - 1);
        boi4.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 1);
    }

    IEnumerator shoot(Vector2 playerVector)
    {
        canShootNext = false;

        // Instantiate oshitwaddup on Datboi.
        oshitwaddup = Instantiate(oshitwaddupPrefab) as GameObject;
        oshitwaddup.transform.position = transform.position;

        // Move and rotate oshitwaddup to Player.
        oshitwaddup.GetComponent<Rigidbody2D>().AddForce(playerVector * shootPower);
        float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
        oshitwaddup.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        yield return new WaitForSeconds(2f);

        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Substract 1 health if hit by PlayerProjectile GameObject.
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health = health - FindObjectOfType<Player>().damage; ;

            if (health <= 0  && alive)
            {
                // Coroutine because Wait Time is necessary.
                StartCoroutine(die());
                soundMan.playAudioClip("MainEnemyDeath");
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        for(float f = 1f; f >= 0; f -= 0.1f)
        {
            // The colour of DatBoi's sprite.
            Color colour = sr.material.color;

            // Reduce colour's alpha by 0.1f for every f >= 0.
            colour.a = f;

            // Apply colour with new alpha value to DatBoi's SpriteRenderer Component.
            sr.material.color = colour;

            // Wait until next frame.
            yield return null;
        }

        // Kill DatBoi.
        alive = false;

        int ran = Random.Range(0, 100);
        if(ran < 50)
        {
            int ran1 = Random.Range(0, 100);

            if(ran1 < 10)
            {
                GameObject oneYen = Instantiate(yen1) as GameObject;
                oneYen.transform.position = transform.position;
            }
            else if(ran1 < 20)
            {
                GameObject fiveYen = Instantiate(yen5) as GameObject;
                fiveYen.transform.position = transform.position;
            }
            else if(ran1 < 80)
            {
                GameObject tenYen = Instantiate(yen10) as GameObject;
                tenYen.transform.position = transform.position;
            }
            else if(ran1 < 100)
            {
                GameObject fiftyYen = Instantiate(yen50) as GameObject;
                fiftyYen.transform.position = transform.position;
            }
        }
        statue.GetComponent<SpriteRenderer>().sprite = statueActivated;
        Destroy(this.gameObject);
    } 
}
