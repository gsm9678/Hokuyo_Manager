using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    private URGSensorObjectDetector m_senserData;
    List<Vector3> vector3 = new List<Vector3>();
    List<Vector3> points = new List<Vector3>();

    [SerializeField][Header("예외 존")]
    List<BoxSize> boxsize = new List<BoxSize>();

    [SerializeField][Header("위치 적용 값")]
    float Left, Top, Right, Bottom;

    void Start()
    {
        m_senserData = GameObject.Find("Sensor Data").GetComponent<URGSensorObjectDetector>();
        StartCoroutine(GetSendsorData());
    }

    IEnumerator GetSendsorData()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            vector3.Clear();

            points.Clear();

            for (int i = 0; i < m_senserData.detectedObjects.Count; i++)
            {
                bool check = false;

                vector3.Add(new Vector3(scale(-m_senserData.detectRectWidth / 2, m_senserData.detectRectWidth / 2, Left, Right, m_senserData.detectedObjects[i].position.x),
                                        0,
                                        scale(0, m_senserData.detectRectHeight, Bottom, Top, m_senserData.detectedObjects[i].position.y)));

                foreach (BoxSize _b in boxsize)
                {
                    if (_b.CheckPoint(new Vector2(vector3[i].x, vector3[i].z)))
                    {
                        Debug.Log("A");
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    points.Add(vector3[i]);
                }
            }
        }
    }

    public List<Vector3> getSensorVector()
    {
        return points;
    }

    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
