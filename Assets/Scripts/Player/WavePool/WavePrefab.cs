using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WavePrefab : MonoBehaviour
{
    // 发射脚步声的基本特性

    public float fireSpeed;
    public float subCount;
    private LineRenderer lrd;

    // 目标地点
    private Vector3 originPos;                                          // 起始点                   
    private Vector3 mouseInWorldPos;
    private Vector3 mouseInScreenPos;
    private Vector3 moveDirection;

    void Awake()
    {
        lrd = GetComponent< LineRenderer >();
    }

    // 启动的时候 射！ 回收
    void OnEnable()
    {
        originPos = GetComponent< Transform >().position;
        mouseInScreenPos = Mouse.current.position.ReadValue();
        mouseInWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInScreenPos.x, mouseInScreenPos.y, 0));

        Vector2 dir = mouseInWorldPos - originPos;

        Ray2D ray = new Ray2D(originPos, dir);

        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction);

        if(info)
        {
            Debug.Log(info.point);
            Debug.DrawLine(originPos, info.point, Color.red);
        }

        // 回收
        WavePool.instance.PushinPool(gameObject);
    }

    // void OnDisable()
    // {

    // }

}
