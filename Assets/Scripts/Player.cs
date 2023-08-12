using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;


    private float initHealth = 100;
    private float initMana = 50;

    [SerializeField]
    private GameObject[] spellPrefab;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2 ;

    public Transform MyTarget { get; set; }//玩家的目标

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);

        base.Start();
    }


    protected override void Update()
    {
        GetInput();
        base.Update();
    }

    private void GetInput()
    {
        //test
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }


        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }

    }

    private IEnumerator Attack(int spellIndex)
    {
        isAttacking = true;
        myAnimator.SetBool("attack", isAttacking);
        yield return new WaitForSeconds(1);
        //使用Instantiate函数创建一个新的游戏对象。spellPrefab[0]表示要实例化的预制体，exitPoints[exitIndex].position表示新游戏对象的位置，Quaternion.identity表示新游戏对象的旋转。
        //这意味着将在给定位置创建一个新的游戏对象，该对象是预制体spellPrefab[0]的一个实例，并且不旋转。
        Instantiate(spellPrefab[spellIndex], exitPoints[exitIndex].position, Quaternion.identity);
        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();

        if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    private bool InLineOfSight()
    {
        //它计算了两个变换（transform）之间的方向向量（targetDirec），并将其标准化（normalized）。
        //target.transform.position表示目标变换的位置，transform.position表示当前变换的位置，两者相减得到两个变换之间的方向向量。
        //.normalized表示将该向量标准化，使其长度为1。这意味着targetDirection是一个指向目标变换且长度为1的向量。
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        //使用Physics2D.Raycast函数来发射一条射线，检测沿着给定方向（targetDirection）的物体。
        //transform.position表示射线的起点，Vector2.Distance(transform.position,target.transform.position)表示射线的长度。
        //RaycastHit2D hit表示射线检测到的第一个物体的信息。如果射线没有检测到任何物体，hit.collider将为null。
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection,Vector2.Distance(transform.position, MyTarget.transform.position),64);

        if (hit.collider == null)
        {
            return true;
        }

        Debug.DrawRay(transform.position, targetDirection,Color.red);

        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}
