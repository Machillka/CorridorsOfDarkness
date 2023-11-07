using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class WavePrefab : MonoBehaviour
{
    // 发射脚步声的基本特性
    [Range(0, 1)]
    public float alpha;

    public float fireSpeed;
    public float subCount;
    public float initWaveLength;
    public float maxMoveDistanve;                                       // 单次最远移动距离

    private LineRenderer lrd;

    // 目标地点
    private Vector3 originPos;                                          // 起始点                   
    private Vector3 mouseInWorldPos;
    private Vector3 mouseInScreenPos;
    private Vector3 moveDirection;
    private Transform playerTransform;

    private Vector3 moveStart;
    private Vector3 moveEnd;

    private RaycastHit2D rayInfo;
    private Ray2D waveRay;

    private float waveLength;
    private float moveDistance;

    void Awake()
    {
        lrd = GetComponent< LineRenderer >();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // 启用后执行
    // 发射一段距离之后取消 || 触碰到物体
    void Update()
    {
        lrd.SetPosition(0, moveStart);
        lrd.SetPosition(1, moveEnd);

        waveLength = Vector3.Distance(moveStart, moveEnd);
        moveDistance = Vector3.Distance(moveEnd, originPos);                    // 移动距离就是射线末端到发射位置的距离

        // 初始还没射太远, 只修改末位置
        if (waveLength <= initWaveLength)
        {
            moveEnd += fireSpeed * moveDirection;
        }
        else
        {
            moveStart += fireSpeed * moveDirection;
            moveEnd += fireSpeed * moveDirection;
        }        
        
        

        // 可以击打, 距离很近, 结束
        if (Vector3.Distance(moveEnd, rayInfo.point) < 0.03f)
        {
            // Start sub wave()
            gameObject.SetActive(false);
        }


    }

    // 启动的时候 射！ 回收
    void OnEnable()
    {
        Debug.Log("enable!");
        originPos = playerTransform.position;
        mouseInScreenPos = Mouse.current.position.ReadValue();
        mouseInWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInScreenPos.x, mouseInScreenPos.y, 0));

        moveDirection = (mouseInWorldPos - originPos).normalized;

        waveRay = new Ray2D(originPos, moveDirection);
        rayInfo= Physics2D.Raycast(waveRay.origin, waveRay.direction);

        moveStart = originPos;
        moveEnd = originPos;
    }
}
