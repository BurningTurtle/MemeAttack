using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStopAnimDestroy : MonoBehaviour {

    [SerializeField] private GameObject timeToStopAnim;

    public void destroyTimeToStopAnim()
    {
        timeToStopAnim.SetActive(false);
    }
}
