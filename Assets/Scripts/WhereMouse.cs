using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereMouse : MonoBehaviour
{
    public Transform whereMouseTrans;

    void FixedUpdate()
    {



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

        whereMouseTrans.position = Input.mousePosition / 10;
    }
}
