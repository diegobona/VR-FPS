using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using BNG; //VRIF

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    //public Camera cam;   //如果要用，把CenterEyeAnchor拖进去赋值给它
    public float giveDamage = 50f; //射中后的减血量；界面上设置的优先级高
    public float shootingRange = 100f;

    //Grabber类型：VRIF中的类型，抓取器，即左右手控制器。需要在inspector中拖CameraRig-...-LeftController下面的Grabber进去赋值。
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

    public float triggerThreshold = 0.5f;  // 手柄trigger键按压阈值。暂时未用到

    public Transform rayStartPoint;

//定义不同材质物体被射击后的效果(赋值：在界面inspector中把相应的prefab拖进去)：
    public GameObject woodEffect;
    public GameObject metalEffect;
    public GameObject humanEffect;
    public GameObject concreteEffect;




    void Start()
    {

    }

    
    void Update()
    {

      
        // 检测手正在握着的物体、以及Trigger按钮是否按下
        // HeldGrabbable是VRIF中定义的属性，指当前正被握着的物体

        if (lHandGrabber.HeldGrabbable != null)
        {
            Debug.Log("object currently held in lHand:" + lHandGrabber.HeldGrabbable);//返回的是VRIF的Grabbable类型，类似GameObject；但它更具体：可被抓取的。
            if (lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown)
            {
                if (checkBulletNum() > 0)
                {
                    Shoot();
                }
            }
        }

        if (rHandGrabber.HeldGrabbable != null)
        {
            Debug.Log("object currently held in rHand:" + rHandGrabber.HeldGrabbable);
            if (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown)
            {
                if (checkBulletNum() > 0)
                {
                    Shoot();
                }
            }
        }

        //if ((lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown) || (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown))
        //{
        //    if (checkBulletNum() > 0)
        //    {
        //        Shoot();
        //    }
        //}

    }


//检查枪里的剩余子弹数量:
    int checkBulletNum()
    {
        int bulletNum = gameObject.GetComponent<RaycastWeapon>().GetBulletCount();//调用VRIF的RaycastWeapon.cs脚本中的GetBulletCount方法；gameObject.是当前脚本挂载的物体。
        Debug.Log("剩余子弹数量" + bulletNum);
        return bulletNum;
    }



    void Shoot()
    {
        RaycastHit hitInfo;

        //if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hitInfo, shootingRange))
        //{
        //    Debug.Log(hitInfo.transform.name);
        //}

        if (Physics.Raycast(rayStartPoint.position, rayStartPoint.forward, out hitInfo, shootingRange))
        {
            Debug.Log("枪的raycast hit到的物体:"+hitInfo.transform.name);

            // 如果射线检测到碰撞，绘制一条红色的线
            Debug.DrawRay(rayStartPoint.position, rayStartPoint.forward* hitInfo.distance, Color.red);

            //射中不同材质的物体，产生不同的射中效果：
            if (hitInfo.transform.tag == "Metal")
            {
                Debug.Log("hit a Metal!!!!!!!");
                GameObject metalGo = Instantiate(metalEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(metalGo, 2f);
            }
            else if (hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("hit a enemy!!!!!!!");
                GameObject humanGo = Instantiate(humanEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(humanGo, 2f);
            }
            else if (hitInfo.transform.tag == "Wood")
            {
                Debug.Log("hit a wood!!!!!!!");
                GameObject woodGo = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 2f);
            }

            
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();//获取击中的enemy的Enemy组件（脚本也属于组件）

            if(enemy != null)
            {
                enemy.EnemyHitDamage(giveDamage);
            }


            // Objects objects = hitInfo.transform.GetComponent<Objects>();//通过获取组件的方式，创建一个Objects.cs脚本的实例对象（脚本也是组件）

            //if (objects != null)
            //{
            //    //被射中的object（主要是人）减少生命值
            //    objects.objectHitDamage(giveDamage);//调用Objects.cs脚本中的objectHitDamage方法
            //}

        }

       


    }




}
