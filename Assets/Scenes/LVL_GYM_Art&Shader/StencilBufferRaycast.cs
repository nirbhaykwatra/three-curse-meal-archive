using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilBufferRaycast : MonoBehaviour
{
    [SerializeField] public GameObject target;
    public LayerMask mylayermask;
    [SerializeField] private float _radius = 2f;

    private void Start()
    {
        target.GetComponent<MeshRenderer>().enabled = true;
    }
    private void Update()
    {
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, target.transform.position, out hit, Mathf.Infinity, mylayermask))
        //{
        //    if (hit.collider.gameObject.tag == "Mask")
        //    {
        //        Debug.DrawRay(transform.position, target.transform.position * hit.distance, Color.yellow);
        //        Debug.Log("hit mask");
        //        target.transform.localScale = new Vector3(0, 0, 0);
        //    }
        //    else
        //    {
        //        Debug.DrawRay(transform.position, target.transform.position * hit.distance, Color.red);
        //        Debug.Log("hit wall");
        //        Debug.Log(hit.collider.gameObject.tag);
        //        target.transform.localScale = new Vector3(3f, 3f, 3f);
               
        //    }
        //}
    }



    private void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, _radius, target.transform.position - transform.position, out RaycastHit hit, Mathf.Infinity, mylayermask))
        {
            //Debug.Log($"{hit.collider.gameObject.name} was hit!");
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Mask"))
            {
                //Debug.DrawRay(transform.position, target.transform.position * hit.distance, Color.yellow);
                Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
                //Debug.Log("hit mask");
                target.GetComponent<MeshRenderer>().enabled = false; // please cache this on Start
            }
            else
            {
                //Debug.DrawRay(transform.position, target.transform.position * hit.distance, Color.red);
                Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
                //Debug.Log("hit wall");
                //Debug.Log(hit.collider.gameObject.tag);
                target.GetComponent<MeshRenderer>().enabled = true; // please cache this on Start

            }
        }
    }

        //RaycastHit[] hits;
        //hits= Physics.RaycastAll(transform.position, target.transform.position - transform.position, Mathf.Infinity, mylayermask);

        
    //    if (Physics.SphereCast(transform.position, _radius, target.transform.position - transform.position, out RaycastHit hit, Mathf.Infinity, mylayermask))
    //    {
            
    //        GameObject obs = hit.rigidbody.gameObject;
    //        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
    //        {
    //            Debug.Log($"{hit.collider.gameObject.name} was hit!");
    //            Debug.DrawRay(transform.position, target.transform.position * hit.distance, Color.yellow);
    //            obs = hit.rigidbody.gameObject;
    //            changeObjectColor(obs,0.3f);
    //            Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
    //            Debug.Log("hit mask");
    //            //target.GetComponent<MeshRenderer>().enabled = false; // please cache this on Start
    //        }
    //        else
    //        {
    //            changeObjectColor(obs, 1f);
    //        }
    //    }
    //}

    //private void changeObjectColor(GameObject obs, float alpha)
    //{
    //    Renderer renderer = obs.GetComponent<Renderer>();
    //    Color c = renderer.material.color;
    //    renderer.material.color = new Color(c.r, c.g, c.b, alpha);
    //    Debug.Log("changing opacity");
    //}
}