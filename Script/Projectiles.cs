﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public LayerMask collisionMask;
    public Color trailColour;
    float speed = 10;
    float damage = 1;

    float lifetime = 3;
    float skinWidth = .1f;

    void Start()
    {
        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }

        GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColour);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }

    //void OnHitObject(RaycastHit hit)
    //{
    //    IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
    //    if (damageableObject != null)
    //    {
    //        damageableObject.TakeHit(damage, hit);
    //    }
    //    //print(hit.collider.gameObject.name);
    //    GameObject.Destroy(gameObject);
    //}

    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        //print(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
}