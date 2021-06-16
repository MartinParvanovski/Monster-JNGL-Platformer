using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            Vector3 coordinates = follow.transform.position;
            this.transform.position = new Vector3(coordinates.x, coordinates.y, coordinates.z - 5);
        }

    }
}
