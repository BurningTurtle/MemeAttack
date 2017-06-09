using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special2Slot : MonoBehaviour {

    public int slotNumber = 2;
    [SerializeField] private GameObject yen500Prefab;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(FindObjectOfType<Special2Controller>().GetComponent<Special2Controller>().solved);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            if(collision.gameObject.tag == "Zero" || collision.gameObject.tag == "One")
            {
                switch (collision.gameObject.tag)
                {
                    case "Zero":
                        slotNumber = 0;
                        break;
                    case "One":
                        slotNumber = 1;
                        break;
                }
                triggered = true;
                collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y);
                collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                FindObjectOfType<Special2Controller>().GetComponent<Special2Controller>().updateSolved();
                if (FindObjectOfType<Special2Controller>().GetComponent<Special2Controller>().solved)
                {
                    GameObject reward1 = Instantiate(yen500Prefab) as GameObject;
                    GameObject reward2 = Instantiate(yen500Prefab) as GameObject;
                    GameObject reward3 = Instantiate(yen500Prefab) as GameObject;
                    GameObject reward4 = Instantiate(yen500Prefab) as GameObject;
                    GameObject reward5 = Instantiate(yen500Prefab) as GameObject;

                    reward1.transform.position = new Vector2(-43.65f, -61);
                    reward2.transform.position = new Vector2(-40.15f, -61);
                    reward3.transform.position = new Vector2(-36.65f, -61);
                    reward4.transform.position = new Vector2(-33.15f, -61);
                    reward5.transform.position = new Vector2(-29.65f, -61);
                }
            }
        }
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
