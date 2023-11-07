using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    enum MouseStatement
    {
        singleClick,
        holdMouse,
        doubleClick,
        pressUp,
        error
    };
    MouseStatement mouseStatement;

    private PlayerActions inputController;
    private Rigidbody2D rb;
    private SpriteRenderer footSprite;
    private Light2D playerLight;

    [Range(0, 1)]
    public float lowAlpha;                      // Control the image 
    [Range(0, 1)]
    public float fadeMult;                      // 淡出速率

    public float basicMoveSpeed;                // 基础移动速度
    public float holdSpeedMult;                 // Hold 移动增益
    public float tapSpeedMult;                  // Tap 移动增益
    public float waitSec;                       // 等待淡出的时间
    public float singleMoveExistTime;           // 单次步长时间
    public float mouseDistanceRange;            // 在距离内发射 ！
    public float fireLightExistTime;            // 发射时候存在照明时间

    private Vector3 mouseInWorldPos;
    private Vector3 mouseInScreenPos;
    private Vector3 moveDirection;
    private Vector3 lastMousePos;
    private float turnAngle;
    private float mouse2PlayerDistance;

    private bool isMouseEvent;
    private bool isSlowMoving;
    private bool isFiring;

    void Awake()
    {
        inputController = new PlayerActions();
        rb = GetComponent< Rigidbody2D >();
        footSprite = GetComponent< SpriteRenderer >();
        playerLight = transform.Find("PlayerLight").GetComponent< Light2D >();

        isSlowMoving = false;
        isMouseEvent = false;

        // 添加事件绑定
        inputController.PlayerGaming.MouseHold.performed += callback =>
        {
            lastMousePos = mouseInWorldPos;                 // 记录按下按键时的鼠标位置
            mouse2PlayerDistance = Vector3.Distance(lastMousePos, transform.position);
            // Debug.Log(mouse2PlayerDistance);

            isMouseEvent = true;

            // 距离足够大 => move
        
            if (callback.interaction is HoldInteraction)
            {
                // Debug.Log("Hold");
                mouseStatement = MouseStatement.holdMouse;
                if (mouse2PlayerDistance > mouseDistanceRange)
                    StartCoroutine(FastMove());
            }
            else
            {
                // Debug.Log("Click");
                mouseStatement = MouseStatement.singleClick;

                // 上一次走完之后才 adapt 新的输入
                if (isSlowMoving == false)
                {
                    if (mouse2PlayerDistance > mouseDistanceRange)
                        StartCoroutine(SlowMove());
                }
            }
        };

        inputController.PlayerGaming.MouseUp.performed += callback =>
        {
            // Debug.Log("Press UP");
            isMouseEvent = false;
            PressUp();

            mouseStatement = MouseStatement.pressUp;
        };
    }

    void Start()
    {
        // fade out the player
        StartCoroutine(FadeOutPlayer(waitSec + 0.3f, fadeMult));
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveParame();
    }

    void FireWave()
    {
        // Debug.Log("Fire!");
        var wave = WavePool.instance.GetObject();
        StartCoroutine(FireLight());                             
    }

    void PressUp()
    {
        // reset the velocity and animation
        if (mouseStatement == MouseStatement.holdMouse)
        {
            rb.velocity = Vector3.zero;
        }

        // 上一次长按 + 在触发距离内, 则发射射线
        // Debug.Log(mouseStatement);
        if (mouseStatement == MouseStatement.holdMouse && mouse2PlayerDistance < mouseDistanceRange)
        {
            isFiring = true;
            // 发射
            FireWave();
        }
            
        // 减小透明度 , 如果没在发射直接减少透明度
        if (isFiring != true)
            StartCoroutine(FadeOutPlayer(waitSec, fadeMult));

    }

    void GetMoveParame()
    {
        // Get the position of mouse
        mouseInScreenPos = Mouse.current.position.ReadValue();
        mouseInWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInScreenPos.x, mouseInScreenPos.y, 0));

        // Get the move direction and turn angle
        moveDirection = (mouseInWorldPos - transform.position).normalized;

        turnAngle = Vector2.Angle((Vector2)moveDirection, Vector3.up);
        turnAngle *= Mathf.Sign(transform.position.x - mouseInWorldPos.x);
    }

    //TODO 修复 Bug: 角色 连击 Tap => 多次 Press Up 发生的闪耀现象 ( 可能两个协程同时控制灯光 )
    IEnumerator FadeOutPlayer(float waitSec, float fadeMult)
    {
        // float alphaTemp = footSprite.color.a;
        float alphaTemp = playerLight.intensity;

        // 等待时刻, 不 fade out
        while (waitSec > 0f)
        {
            waitSec -= Time.deltaTime;
            // if isMouseEvent == true => 新的按键输入
            // 比如多次点击 tap, 就直接 break 不消失
            if (isMouseEvent == true)
                yield break;   
            yield return null;
        }

        while(alphaTemp > 0.01f)
        {
            alphaTemp -= alphaTemp * fadeMult;
            playerLight.intensity = alphaTemp;
            yield return null;
        }

        playerLight.intensity = 0f;
    }

    // 发射的时候先亮一会, 然后 fade out
    IEnumerator FireLight()
    {
        // float timer = fireLightExistTime;
        playerLight.intensity = 1f;

        yield return new WaitForSeconds(fireLightExistTime);

        // while (timer > 0f)
        // {
        //     timer -= Time.deltaTime;
        //     yield return null;
        // }

        StartCoroutine(FadeOutPlayer(waitSec, fadeMult));
    }

    IEnumerator FastMove()
    {
        while (isMouseEvent == true)
        {
            rb.velocity = moveDirection * holdSpeedMult;
            transform.eulerAngles = new Vector3(0, 0, turnAngle);
            // footSprite.color = new Color(1, 1, 1, 1);
            playerLight.intensity = 1f;

            yield return StartCoroutine(FlipFootSprite());
        }
    }
    
    IEnumerator SlowMove()
    {
        isSlowMoving = true;

        rb.velocity = moveDirection * tapSpeedMult;
        transform.eulerAngles = new Vector3(0, 0, turnAngle);
        // footSprite.color = new Color(1, 1, 1, lowAlpha);
        playerLight.intensity = lowAlpha;

        yield return StartCoroutine(FlipFootSprite());

        rb.velocity = Vector3.zero;
        isSlowMoving = false;
    }

    IEnumerator FlipFootSprite()
    {
        footSprite.flipX = !footSprite.flipX;
        yield return new WaitForSeconds(singleMoveExistTime);
    }

#region Enable
    void OnEnable()
    {
        inputController.Enable();
    }

    void OnDisable()
    {
        inputController.Disable();
    }
#endregion
}
