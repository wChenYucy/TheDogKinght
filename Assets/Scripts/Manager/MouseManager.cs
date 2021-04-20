// ****************************************************
//     文件：MouseManager.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/20 22:16:49
//     功能：鼠标控制类
// *****************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    public Texture2D Point, Doorway, Attack, Target, Arrow;
    
    public static MouseManager Instance;
    public event Action<Vector3> OnMouseClick;
    
    private RaycastHit hitInfo;

    public void Awake()
    {
        if ( Instance!= null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    private void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            //todo 切换鼠标显示
            switch (hitInfo.collider.tag)
            {
                case "Ground":
                    Cursor.SetCursor(Target,new Vector2(16,16),CursorMode.Auto);
                    break;
            }
        }
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClick?.Invoke(hitInfo.point);
            }
        }
    }
}
