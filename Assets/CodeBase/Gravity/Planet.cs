using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Planet : MonoBehaviour
{
    private Rigidbody2D _rb;

    public Rigidbody2D Rb
    {
        get { return _rb; }
    }

    public delegate void PlanetCreated(Planet planet);

    public static event PlanetCreated PlanetCreatedAction;

    public delegate void PlanetDestroyed(Planet planet);

    public static event PlanetDestroyed PlanetDestroyedAction;

    public delegate void PlanetTriggerEnter(Planet planet, IGravitable gravitableObject);

    public static event PlanetTriggerEnter PlanetTriggerEnterAction;

    public delegate void PlanetTriggerExit(Planet planet, IGravitable gravitableObject);

    public static event PlanetTriggerExit PlanetTriggerExitAction;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() =>
        PlanetCreatedAction?.Invoke(this);


    private void OnDisable() =>
        PlanetDestroyedAction?.Invoke(this);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (collision.TryGetComponent(out IGravitable gravitableObj))
            {
                PlanetTriggerEnterAction?.Invoke(this, gravitableObj);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
            if (collision.TryGetComponent(out IGravitable gravitableObj))
            {
                PlanetTriggerExitAction?.Invoke(this, gravitableObj);
            }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}