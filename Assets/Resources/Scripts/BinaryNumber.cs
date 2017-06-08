using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryNumber : MonoBehaviour {

    public int number;
    public bool collided = false;

	// Use this for initialization
	void Start () {
		if(this.tag == "Zero")
        {
            number = 0;
        }
        if(this.tag == "One")
        {
            number = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
