using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CodeBase;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


namespace CodeBase
{
    public class GravityController : MonoBehaviour
    {
        private Dictionary<IGravitable, HashSet<Planet>>
            _affectedBodiesDict = new Dictionary<IGravitable, HashSet<Planet>>();

        public Transform nearestPlanet;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Planet.PlanetTriggerEnterAction -= PlanetTriggerEntered;
            Planet.PlanetTriggerExitAction -= PlanetTriggerExited;
        }

        private void Initialize()
        {
            Planet.PlanetTriggerEnterAction += PlanetTriggerEntered;
            Planet.PlanetTriggerExitAction += PlanetTriggerExited;
        }


        private void PlanetTriggerEntered(Planet planet, IGravitable gravitableObj)
        {
            if (!_affectedBodiesDict.ContainsKey(gravitableObj))
                _affectedBodiesDict.Add(gravitableObj,new HashSet<Planet>());
            _affectedBodiesDict[gravitableObj].Add(planet);


        }

        private void PlanetTriggerExited(Planet planet, IGravitable gravitableObj)
        {
            _affectedBodiesDict[gravitableObj].Remove(planet);
        }

        private void FixedUpdate() => UpdateGravitableObjects();

        private Vector3 CalculateRelativeForce(Rigidbody2D rb1, Rigidbody2D rb2)
        {
            Vector3 directionToPlanet = (rb1.position - rb2.position).normalized;
            float distance = (rb1.position - rb2.position).magnitude;
            float strength = 2*rb1.mass * rb2.mass / Mathf.Pow(distance, 1);

            return directionToPlanet * strength;
        }

        private float CalculateRotationForce(Rigidbody2D rb, IGravitable affectedBody)
        {
            Vector2 toPlanetVec = rb.position -
                                 affectedBody.Rb.position;
            float distance = Vector2.Distance(rb.position, affectedBody.Rb.position);
            toPlanetVec.Normalize();
            float angle = Mathf.Atan2(toPlanetVec.y, toPlanetVec.x) * Mathf.Rad2Deg;
            Debug.Log(toPlanetVec);
            angle = (Convert.ToInt32(angle) - 90);


            return angle;
        }

        private Planet FindClosestPlanet(Vector2 pos, HashSet<Planet> planets)
        {
            float minDistance = Single.MaxValue;
            Planet resPlanet=null;
            foreach (Planet planet in planets)
            {
                if (Vector2.Distance(pos, planet.transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(pos, planet.transform.position);
                    resPlanet = planet;
                }
            }


            return resPlanet;
        }
        private void UpdateGravitableObjects()
        {
            foreach (KeyValuePair<IGravitable, HashSet<Planet>> keyValuePair in _affectedBodiesDict)
            {

                if (keyValuePair.Value.Count == 0)
                    continue;

                float _rotationForce = 0;
                Vector3 _relativeForce = Vector3.zero;

                foreach (Planet planet in keyValuePair.Value)
                {
                    _relativeForce += CalculateRelativeForce(planet.Rb, keyValuePair.Key.Rb);
                }
                
                _rotationForce =
                    CalculateRotationForce(FindClosestPlanet(keyValuePair.Key.Rb.transform.position, keyValuePair.Value).Rb,
                        keyValuePair.Key);
                _relativeForce /= keyValuePair.Value.Count;
                
                Debug.Log("Rotation Force: "+_rotationForce + "Relative Force: " + _relativeForce + " RB: " + keyValuePair.Key.Rb.rotation);
                Debug.Log(keyValuePair.Value.Count);
                
                keyValuePair.Key.AddRelativeForce(_relativeForce);
                keyValuePair.Key.AddRotationForce(_rotationForce);
            }
        }
        
    }
}