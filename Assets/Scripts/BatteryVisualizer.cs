using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ROS2;
using ROS2.Utils;


public class BatteryVisualizer : MonoBehaviour
{
    public TextMeshPro text;
    ISubscription<sensor_msgs.msg.BatteryState> bat_sub;

    // Start is called before the first frame update
    void Start()
    {
        bat_sub = ROS2Listener.instance.node.CreateSubscription<sensor_msgs.msg.BatteryState>(
            Utils.batteryStateTopic, ReceiveBatteryState, QosProfile.Profile.Default);
    }

    private void ReceiveBatteryState(sensor_msgs.msg.BatteryState msg)
    {
        if (Utils.debugMode) Debug.Log("BatteryVisualizer: Recieved battery message, soc: " + msg.Percentage + "%");
        text.text = msg.Percentage + "%";
    }

}
