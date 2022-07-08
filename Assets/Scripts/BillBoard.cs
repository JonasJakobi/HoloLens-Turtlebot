using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROS2;
/// <summary>
/// Add this script to a Gameobject to have the Gameobject always look at the Main Camera. 
/// </summary>
public class BillBoard : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
