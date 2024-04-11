using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public OSC _isOSC;

    public void StartMessage(Vector2 vector2)
    {
        OscMessage message = new OscMessage();
        message.address = "/Sensor/Start";
        message.values.Add(vector2.x);
        message.values.Add(vector2.y);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void SensorMessage(Vector3 vector3)
    {
        OscMessage message = new OscMessage();
        message.address = "/Sensor/Data";
        message.values.Add(vector3.x);
        message.values.Add(vector3.y);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void StopMessage()
    {
        OscMessage message = new OscMessage();
        message.address = "/Sensor/End";
        message.values.Add(1);
        message.values.Add("");
        _isOSC.Send(message);
    }
}
