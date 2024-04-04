using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDetection : MonoBehaviour
{
    private HokuyoManager hokuyoManager;

    [SerializeField] Slider Point_Scale;
    [SerializeField] Slider Max_Scale;
    [SerializeField] Slider Min_Scale;

    List<GameObject> gizmos_Images = new List<GameObject>();

    private void Start()
    {
        hokuyoManager = GameObject.Find("HokuyoManager").GetComponent<HokuyoManager>();
        StartCoroutine("CheckPointDetect");
    }

    IEnumerator CheckPointDetect()
    {
        yield return new WaitUntil(() => hokuyoManager.gizmos_Images != null);
        gizmos_Images = hokuyoManager.gizmos_Images;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (gizmos_Images[0] != Point_Scale)
                for(int i = 0; i < gizmos_Images.Count; i++)
                {
                    gizmos_Images[i].GetComponent<RectTransform>().sizeDelta= new Vector2(Point_Scale.value, Point_Scale.value);
                }

            for(int i = 0; i < gizmos_Images.Count; i++)
            {
                if (gizmos_Images[i].active)
                {

                }
            }

        }
    }
}
