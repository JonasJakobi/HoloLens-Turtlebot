using ROS2;
using ROS2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.Sensor;
using UnityEngine;

public class mrtkTest : MonoBehaviour
{
    INode node;
    ISubscription<sensor_msgs.msg.BatteryState>bat_sub;
    IPublisher<sensor_msgs.msg.BatteryState> bat_pub;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            RCLdotnet.Init();
        }
        catch (UnsatisfiedLinkError e)
        {
            Debug.Log(e.ToString());
        }

        node = RCLdotnet.CreateNode("mrtkTest");

        bat_sub = node.CreateSubscription<sensor_msgs.msg.BatteryState>(
            Utils.batteryStateTopic, CallBackMethod, QosProfile.Profile.Default);
       

    }

    // Update is called once per frame
    void Update()
    {
        RCLdotnet.SpinOnce(node, 0);
    }

    void CallBackMethod(sensor_msgs.msg.BatteryState msg)
    {
       if(Utils.debugMode) Debug.Log( "I heard with mrtk: " + msg.Percentage + "%!!!");
    }
}
