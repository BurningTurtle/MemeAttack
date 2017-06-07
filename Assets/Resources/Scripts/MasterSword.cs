using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSword : MonoBehaviour {

    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(!player.GetComponent<Player>().isDarkLink)
        {
            Destroy(gameObject);
        }
        rotateToMouse();
    }

    private void rotateToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 270));
    }
}
