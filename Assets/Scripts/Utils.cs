using RosMessageTypes.Geometry;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;
/// <summary>
/// UzL  - Collection of useful static methods and global variables
/// </summary>
public class Utils : MonoBehaviour
{
    public static bool debugMode = true;

    public static string batteryStateTopic = "battery_state";
    public static string laserScanTopic = "scan";

    #region Transformations ROS messages <-> Unity data types. 
    //FLU - (forward, left, up ) used by ROS. RUF (right, up, forward) used by Unity 
    public static PointMsg Vector3ToRos(Vector3 pos)
    {
        return pos.To<FLU>();
    }
    public static Vector3 PointMsgToUnity(PointMsg message)
    {
        return message.From<FLU>();
    }
    public static Quaternion QuaternionToUnity(QuaternionMsg message)
    {
        return message.From<FLU>();
    }
    public static QuaternionMsg QuaternionToRos(Quaternion rot)
    {
        return rot.To<FLU>();
    }
    #endregion
}
