using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.Utils;

public class TestingRobot : MonoBehaviour
{
    public float speed = 2.5f;
    /// <summary>
    /// if set to true, the robot model will activate and send test information over Ros. 
    /// </summary>
    public bool useTestRobot = true;
 
    IPublisher<sensor_msgs.msg.BatteryState> batPub;
    IPublisher<sensor_msgs.msg.LaserScan> lidarPub;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;


    float batteryState = 100;
   

    // Start is called before the first frame update
    void Start()
    {
        //if the test robot is deactived, we wont create our node / pubs 
        if (!useTestRobot) return;

        lidarPub = ROS2Listener.instance.node.CreatePublisher<sensor_msgs.msg.LaserScan>(Utils.laserScanTopic, QosProfile.Profile.SensorData);
        batPub = ROS2Listener.instance.node.CreatePublisher<sensor_msgs.msg.BatteryState>(Utils.batteryStateTopic, QosProfile.Profile.SensorData);

    }

    // Update is called once per frame
    void Update()
    {
        RobotPose.SetTestRobotPosAndRot(transform.position, transform.rotation);
        if (!useTestRobot) return;

   


        timeElapsed += Time.deltaTime;
        
        //Every 3 seconds , publish artificial LaserScan Data and publish batterystate data. 
        if(timeElapsed > 3)
        {
            timeElapsed -= 3;
            PublishLaserScan();
            PublishBatteryState();

        }
        //Drive forward. 
        transform.position = new Vector3(transform.position.x + 0.1f * Time.deltaTime * speed, transform.position.y, transform.position.z);
        //Update global Robot Pose with our position. 
        

    }

    private void PublishLaserScan()
    {
        sensor_msgs.msg.LaserScan lsm = new sensor_msgs.msg.LaserScan();
        lsm.Ranges = new List<float>() { 1, 1, 2, 3, 4, 1, 1, 1 };
        lsm.Range_max = 5;
        lsm.Range_min = 0.1f;
        lsm.Angle_min = 0;
        lsm.Angle_increment = Mathf.PI / 6 ;
        lidarPub.Publish(lsm);
        if (Utils.debugMode) Debug.Log("TestingRobot: Published fake LaserScan Message.");
    }


    private void PublishBatteryState()
    {
        sensor_msgs.msg.BatteryState msg = new sensor_msgs.msg.BatteryState();
        msg.Percentage = batteryState;
        batteryState -= 10;

        batPub.Publish(msg);
        if (Utils.debugMode) Debug.Log("TestingRobot: Published fake BatteryState Message.");
    }

    //Method to return list of fibonacci numbers
    
}
