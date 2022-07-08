using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPose : MonoBehaviour
{
    /// <summary>
    /// The current position of the robot.
    /// </summary>
    static Vector3 robotPosition;
    /// <summary>
    /// The current rotation of the robot
    /// </summary>
    static Quaternion robotRotation;

    public static Vector3 GetPosition()
    {
        return robotPosition;
    }
    public static Quaternion getRotation()
    {
        return robotRotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        robotPosition = new Vector3(0, 0, 0);
        robotRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetTestRobotPosAndRot(Vector3 pos, Quaternion rot)
    {
        robotPosition = pos;
        robotRotation = rot;
    }
}
