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
    public float basicMoveSpeed;                // 基础移动速度
    public float holdSpeedMult;                 // Hold 移动增益
    public float tapSpeedMult;                  // Tap 移动增益
    public float fadeMult;                      // 淡出速率
    public float waitSec;                       // 等待淡出的时间
    public float singleMoveExistTime;           // 单次步长时间
    public float mouseDistance;                 // 在距离内发射 ！

    private Vector3 mouseInWorldPos;
    private Vector3 mouseInScreenPos;
    private Vector3 moveDirection;
    private float turnAngle;

    private bool isMouseEvent;
    private bool isSlowMoving;

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
            isMouseEvent = true;

            if (callback.interaction is HoldInteraction)
            {
                Debug.Log("Hold");
                mouseStatement = MouseStatement.holdMouse;
                StartCoroutine(FastMove());
            }
            else
            {
                Debug.Log("Click");
                mouseStatement = MouseStatement.singleClick;

                // 上一次走完之后才 adapt 新的输入
                if (isSlowMoving == false)
                {
                    StartCoroutine(SlowMove());
                }
            }
        };

        inputController.PlayerGaming.MouseUp.performed += callback =>
        {
            Debug.Log("Press UP");
            isMouseEvent = false;
            PressUp();

            mouseStatement = MouseStatement.pressUp;
        };
    }

    void Start()
    {
        // 设置动画
        
        // fade out the player
        StartCoroutine(FadeOutPlayer(waitSec + 0.3f, fadeMult));
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveParame();
    }

    void FixedUpdate()
    {
        
    }

    void PressUp()
    {
        // reset the velocity and animation
        if (mouseStatement == MouseStatement.holdMouse)
        {
            rb.velocity = Vector3.zero;
            // playerAnimController.speed = 0f;
        }

        // 减小透明度
        StartCoroutine(FadeOutPlayer(waitSec, fadeMult));
        

        // 处于_ 的状态, 则发射射线
        if (mouseStatement == 0)
        {
            // Fire();
        }
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
            // footSprite.color = new Color(1, 1, 1, alphaTemp);
            playerLight.intensity = alphaTemp;
            yield return null;
        }

        // footSprite.color = new Color(1, 1, 1, 0);
        playerLight.intensity = 0f;
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
