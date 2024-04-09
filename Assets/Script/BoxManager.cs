using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;

    [SerializeField] Slider X_Position;
    [SerializeField] Slider Y_Position;
    [SerializeField] Slider X_Size;
    [SerializeField] Slider Y_Size;

    public List<BoxData> boxes = new List<BoxData>();

    public List<RectTransform> objects = new List<RectTransform>();

    [SerializeField] GameObject p_box;
    [SerializeField] Transform t_boxes;

    Vector2 p1, p2, p3, p4;

    private void Start()
    {
        setDropDownOpthions();

        dropdown.onValueChanged.AddListener(delegate { Function_Dropdown(dropdown); });
    }

    private void Update()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].Name == dropdown.options[dropdown.value].text)
            {
                boxes[i].X_Position_Value = X_Position.value;
                boxes[i].Y_Position_Value = Y_Position.value;
                boxes[i].X_Size_Value = X_Size.value;
                boxes[i].Y_Size_Value = Y_Size.value;
            }
        }

        for (int i =0; i < boxes.Count; i++)
        {
            objects[i].transform.localPosition = new Vector3(boxes[i].X_Position_Value, boxes[i].Y_Position_Value, 0);
            objects[i].sizeDelta = new Vector2(boxes[i].X_Size_Value, boxes[i].Y_Size_Value);
        }
    }

    private void Function_Dropdown(Dropdown select)
    {
        for (int i = 0;  i < boxes.Count; i++)
        {
            if (boxes[i].Name == select.options[select.value].text)
            {
                X_Position.value = boxes[i].X_Position_Value;
                Y_Position.value = boxes[i].Y_Position_Value;
                X_Size.value = boxes[i].X_Size_Value;
                Y_Size.value = boxes[i].Y_Size_Value;
            }
        }
    }

    private void setDropDownOpthions()
    {
        dropdown.options.Clear();
    }

    public void addDropDownOpthions()
    {
        Dropdown.OptionData option = new Dropdown.OptionData(); 
        option.text = "Box" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value++;

        objects.Add(Instantiate(p_box, this.transform.position, this.transform.rotation, t_boxes.transform).GetComponent<RectTransform>());

        BoxData box = new BoxData();
        box.Name = option.text;
        boxes.Add(box);
        Function_Dropdown(dropdown);
    }

    public void removeDropDownOpthions()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].Name == dropdown.options[dropdown.value].text)
            {
                dropdown.options.RemoveAt(i);
                boxes.RemoveAt(i);
                dropdown.value--;

                Destroy(objects[i]);
                objects.RemoveAt(i);

                for(int j = i; j < boxes.Count; j++)
                {
                    Dropdown.OptionData option = new Dropdown.OptionData();
                    option.text = "Box" + j.ToString();
                    dropdown.options[j].text = option.text;
                    boxes[j].Name = option.text;
                }
            }
        }
    }

    public bool CheckPoint(Vector2 vector2)
    {
        if (p1.x < vector2.x && p2.x > vector2.x &&
            p1.y > vector2.y && p3.y < vector2.y)
            return true;
        return false;
    }
}
