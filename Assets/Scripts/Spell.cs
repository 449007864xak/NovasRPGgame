using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed;

    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //只是硬编码，作为测试
        target = GameObject.Find("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //得到一个法术释放的方向
        Vector2 direction = target.position - transform.position;

        //将一个刚体的速度（velocity）设置为一个标准化的方向向量（direction.normalized）乘以一个速度值（speed）。
        //这意味着刚体将沿着给定的方向以给定的速度移动。
        myRigidBody.velocity = direction.normalized * speed;

        //使用Mathf.Atan2函数来计算一个二维向量（direction）与x轴之间的夹角（angle），并将其转换为角度制（* Mathf.Rad2Deg）。
        //Mathf.Atan2函数返回的是以弧度为单位的角度，因此需要乘以Mathf.Rad2Deg来将其转换为角度制。
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //使用Quaternion.AngleAxis函数来创建一个绕着给定轴（Vector3.forward）旋转给定角度（angle）的旋转四元数，并将其赋值给变换（transform）的旋转属性（rotation）。
        //这意味着变换将绕着给定的轴旋转给定的角度。
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
