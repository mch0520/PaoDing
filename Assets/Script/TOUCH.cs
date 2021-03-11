using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCH : MonoBehaviour
{
    public void OnDrag()
    {
        //滑鼠
        transform.position = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,100)));
        //觸控
        if (Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        }

    }
}
