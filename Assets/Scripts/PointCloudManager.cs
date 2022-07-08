using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ROS2;
using ROS2.Utils;


public class PointCloudManager : MonoBehaviour
{
    [SerializeField]
    GameObject pointPrefab;
    ISubscription<sensor_msgs.msg.LaserScan> laser_sub;
    bool currentstate = true;

    bool showCloud = true;
    public TextMeshPro text;
    int nr = 2;
    float time = 0;
    bool spawnFake = false;
    /// <summary>
    /// Shows or hides the PointCloud. Deactivates all gameobjects of the speicific points. 
    /// </summary>
    /// <param name="show">true - show, false - hide</param>
    public void ShowPointCloud(bool show)
    {
        showCloud = show;
        foreach (Transform c in transform)
        {
            if(c.tag == "PointCloud") // Only the actual points with the point tag
                {
                    c.gameObject.SetActive(show);
                }
        }
    }

    public void TogglePointCloud()
    {
        currentstate = !currentstate;
        ShowPointCloud(currentstate);
    }

    public void AddPoint(Vector3 position)
    {
        GameObject p = Instantiate(pointPrefab, position, Quaternion.identity);
        p.transform.parent = this.transform;
        p.SetActive(showCloud);
        nr++;
        text.text = "Nr of points: " + nr + "";

    }

    private void Start()
    {
        laser_sub = ROS2Listener.instance.node.CreateSubscription<sensor_msgs.msg.LaserScan>(
             Utils.laserScanTopic, RecieveLaserScanData, QosProfile.Profile.Default);
    }

    private void RecieveLaserScanData(sensor_msgs.msg.LaserScan msg)
    {
        int amount = msg.Ranges.Capacity;
        if (Utils.debugMode) Debug.Log("PointCloudManager: Recieved LaserScan data with " + amount + " entries.");
        for(int i = 0; i < amount; i++)
        {
            if (msg.Ranges[i] > msg.Range_max || msg.Ranges[i] < msg.Range_min) continue; // Discard any point that are too far or too close

            float angle = msg.Angle_min + (msg.Angle_increment * i);
            //Calculate x and y position of data point.
            float xpos = RobotPose.GetPosition().x + Mathf.Cos(angle) * msg.Ranges[i];
            float zpos = RobotPose.GetPosition().z + Mathf.Sin(angle) * msg.Ranges[i];//

            AddPoint(new Vector3(xpos, RobotPose.GetPosition().y, zpos));
        }
    }
    private void Update()
    {
        //Create fake sensor points every 3 seconds.
        time += Time.deltaTime;
        if(time > 3 & spawnFake)
        {
            time = 0;
            sensor_msgs.msg.LaserScan lsm = new sensor_msgs.msg.LaserScan();
            lsm.Ranges = new List<float>() { 0.2f, 0.2f, 1, 0.1f, 0.3f, 0.1f, 0.7f, 2 };
            lsm.Range_max = 5;
            lsm.Range_min = 0.1f;
            lsm.Angle_min = 0;
            lsm.Angle_increment = Mathf.PI / 6;
            RecieveLaserScanData(lsm);
        }
        
    }
    public void ToggleSpawnFake()
    {
        spawnFake = !spawnFake;
    }
}
