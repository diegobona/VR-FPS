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


    public float triggerThreshold = 0.5f;  // 手柄按压阈值

    public Transform rayStartPoint;

    //Grabber：VRIF中的类型，抓取器，即左右手控制器。需要在inspector中拖CameraRig-...-LeftController下面的Grabber进去赋值。
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

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
        }

        Objects objects= hitInfo.transform.GetComponent<Objects>();

        if(objects != null)
        {
            objects.objectHitDamage(giveDamage);
        }


    }




}
