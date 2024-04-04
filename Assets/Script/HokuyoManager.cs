using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HokuyoManager : MonoBehaviour
{
    private URGSensorObjectDetector m_senserData;
    List<Vector3> vector3 = new List<Vector3>();
    List<Vector3> points = new List<Vector3>();

    [SerializeField][Header("¿¹¿Ü Á¸")]
    List<BoxSize> boxsize = new List<BoxSize>();

    [SerializeField]
    Slider Zoom;

    [SerializeField]
    GameObject gizmos_Image, Gizmos_Ob;
    List<GameObject> gizmos_Images =  new List<GameObject>();

    [SerializeField]
    RectTransform Map;

    // Start is called before the first frame update
    void Start()
    {
        m_senserData = GameObject.Find("Sensor Data").GetComponent<URGSensorObjectDetector>();
        for(int i = 0; i < 1081; i++)
        {
            gizmos_Images.Add(Instantiate(gizmos_Image, this.transform.position, this.transform.rotation, Gizmos_Ob.transform));
        }
        StartCoroutine(GetSendsorData());
    }

    IEnumerator GetSendsorData()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            vector3.Clear();

            points.Clear();

            for (int i = 0; i < m_senserData.DirectedDistances.Count; i++)
            {
                bool check = false;
                Vector3 vector = new Vector3(scale(-m_senserData.detectRectWidth / 2, m_senserData.detectRectWidth / 2, (-m_senserData.detectRectWidth / 200) * Zoom.value, (m_senserData.detectRectWidth / 200) * Zoom.value, m_senserData.DirectedDistances[i].x),
                                        scale(0, m_senserData.detectRectHeight, 0 * Zoom.value, m_senserData.detectRectHeight / 100 * Zoom.value, m_senserData.DirectedDistances[i].y),
                                        0);
                vector3.Add(vector);

                gizmos_Images[i].transform.localPosition = vector;

                if (gizmos_Images[i].transform.position.x < Map.rect.width / 2 + Map.transform.position.x &&
                    gizmos_Images[i].transform.position.x > -Map.rect.width / 2 + Map.transform.position.x &&
                    gizmos_Images[i].transform.position.y < Map.rect.height / 2 + Map.transform.position.y &&
                    gizmos_Images[i].transform.position.y > -Map.rect.height / 2 + Map.transform.position.y)
                {
                    gizmos_Images[i].SetActive(true);
                }
                else
                {
                    gizmos_Images[i].SetActive(false);
                }

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
