using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereMouse : MonoBehaviour
{
    public Transform cursor;
    public Transform arrow;
    //public Transform playerCenter;
    public Transform shootThisWay;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 relative = transform.InverseTransformPoint(cursor.position);
        float angle = Mathf.Atan2(relative.x, relative.y);

        Debug.DrawLine(arrow.position, shootThisWay.position, Color.yellow);

        cursor.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));
        arrow.transform.eulerAngles = new Vector3(0, 0, (-angle * Mathf.Rad2Deg));
    }
}