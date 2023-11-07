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
    public float hitOffset;
    public LayerMask detectiveLayer;


    private LineRenderer lrd;

    // 目标地点
    private Vector3 originPos;                                          // 起始点                   
    private Vector3 mouseInWorldPos;
    private Vector3 mouseInScreenPos;
    private Vector2 moveDirection;
    private Transform playerTransform;

    private Vector2 moveStart;
    private Vector2 moveEnd;

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
    //TODO 射线的渐变
    void Update()
    {
        lrd.SetPosition(0, moveStart);
        lrd.SetPosition(1, moveEnd);

        waveLength = Vector2.Distance(moveStart, moveEnd);
        moveDistance = Vector2.Distance(moveEnd, originPos);                    // 移动距离就是射线末端到发射位置的距离

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
        if (Vector3.Distance(moveEnd, rayInfo.point) < hitOffset)
        {
            // Start sub wave()
            gameObject.SetActive(false);
            Debug.Log("hit!!");
        }
        
        // 移动距离过
        if (moveDistance > maxMoveDistanve)
        {
            gameObject.SetActive(false);
            Debug.Log("move too many");
        }
        // Debug.Log("MoveEnd:" + moveEnd);
    }

    // 启动的时候 射！ 回收
    void OnEnable()
    {
        originPos = playerTransform.position;
        mouseInScreenPos = Mouse.current.position.ReadValue();
        mouseInWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInScreenPos.x, mouseInScreenPos.y, 0));

        moveDirection = (Vector2)(mouseInWorldPos - originPos).normalized;

        waveRay = new Ray2D(originPos, moveDirection);
        rayInfo= Physics2D.Raycast(waveRay.origin, waveRay.direction, Mathf.Infinity, detectiveLayer);

        moveStart = originPos;
        moveEnd = originPos;
        
        Debug.Log(rayInfo.point);
    }
}
