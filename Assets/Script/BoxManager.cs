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

    List<BoxData> boxes = new List<BoxData>();

    Vector2 p1, p2, p3, p4;

    private void Start()
    {
        setDropDownOpthions();

        //p1 = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.z + transform.localScale.z / 2);
        //p2 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.z + transform.localScale.z / 2);
        //p3 = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2);
        //p4 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2);
        dropdown.onValueChanged.AddListener(delegate { Function_Dropdown(dropdown); });
    }

    string b;
    private void Function_Dropdown(Dropdown select)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].Name == b)
            {
                boxes[i].X_Position_Value = X_Position.value;
                boxes[i].Y_Position_Value = Y_Position.value;
                boxes[i].X_Size_Value = X_Size.value;
                boxes[i].Y_Size_Value = Y_Size.value;
            }
        }

        for (int i = 0;  i < boxes.Count; i++)
        {
            if (boxes[i].Name == select.options[select.value].text)
            {
                X_Position.value = boxes[i].X_Position_Value;
                Y_Position.value = boxes[i].Y_Position_Value;
                X_Size.value = boxes[i].X_Size_Value;
                Y_Size.value = boxes[i].Y_Size_Value;
                b = select.options[select.value].text;
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
