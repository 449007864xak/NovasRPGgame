using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        //当鼠标点击左键以后,以及鼠标是否没有悬停在任何UI元素上
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //从摄像机将鼠标位置转换为世界坐标，然后从该点发射一条射线，方向为(0,0)，距离为无穷大，层掩码为128。
            //如果射线碰到了任何物体，它们的信息将被存储在hit变量中。
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,128);

            if (hit.collider != null)
            {
               if(hit.collider.tag == "Enemy")
                {
                    player.MyTarget = hit.transform;
                }
            }
            else
            {
                player.MyTarget = null;
            }
        }

    }
}
