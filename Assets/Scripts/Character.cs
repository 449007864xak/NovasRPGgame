using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    protected Vector2 direction;//定义一个方向

    protected Animator myAnimator;

    private Rigidbody2D myRigidbody;

    protected bool isAttacking = false;

    protected Coroutine attackRoutine;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidbody.velocity = direction.normalized * Speed;//normalized做归一化向量处理，对角线移动的话就不会有速度变化了~

      
    }


    //简单判断一下角色是否在移动
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public void HandleLayers()
    {
        //如果在移动
        if (IsMoving)
        {
            //激活跑步的动画，并设置动画中的x的值为direction.x，y值为direction.y这里涉及到角色移动上面其实最大值是1最小值是-1这样子
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if(isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    //批量设置动画的层级
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);//遍历所有的动画，设置为0，即不显示。
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);//传入一个layer的名字，就是动画的名字，然后将它权重设置为1，即显示。
    }

    public void StopAttack()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            myAnimator.SetBool("attack", isAttacking);
        }

    }
}
