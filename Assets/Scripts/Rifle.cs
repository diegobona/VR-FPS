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
    //public Camera cam; //如果要用，把CenterEyeAnchor赋值给它
    public float giveDamage = 50f;
    public float shootingRange = 100f;

    //Grabber：VRIF中的类型，抓取器，即左右手控制器。需要在inspector中拖CameraRig-...-LeftController下面的Grabber进去赋值。
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

    public float triggerThreshold = 0.5f;  // 手柄按压阈值.暂时未用到

    public Transform rayStartPoint;

    public GameObject woodEffect;
    public GameObject metalEffect;
    public GameObject humanEffect;
    public GameObject concreteEffect;





    void Start()
    {

    }

    
    void Update()
    {
        Debug.Log("object currently held in lHand:" + lHandGrabber.HeldGrabbable);//是object类型
        Debug.Log("object currently held in rHand:" + rHandGrabber.HeldGrabbable);

        // 检测手grip的物体、以及Trigger按钮的按下
        //HeldGrabbable是VRIF中定义的属性，指当前正被抓着的物体
        if ((lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown) || (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown))
        {
            Debug.Log("Trigger is holddown");
            Shoot();
        } 

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
            Debug.Log(hitInfo.transform.name);
            // 如果射线检测到碰撞，绘制一条红色的线
            Debug.DrawRay(rayStartPoint.position, rayStartPoint.forward* hitInfo.distance, Color.red);


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

            Objects objects = hitInfo.transform.GetComponent<Objects>();

            if (objects != null)
            {
                objects.objectHitDamage(giveDamage);

               
            }
        }

       


    }




}
