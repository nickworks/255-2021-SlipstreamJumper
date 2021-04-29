using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        if (cam.position.x - transform.position.x >= 30) transform.position += transform.right * 60;
    }
}
