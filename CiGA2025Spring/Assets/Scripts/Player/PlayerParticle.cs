using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        var main = particleSystem.main;
        main.startSize = 0.5f;
        main.startLifetime = 1.0f;
        main.startSpeed = 1.0f;
    }

    void Update()
    {
        var main = particleSystem.main;
        
        var velocity = particleSystem.velocityOverLifetime;
        velocity.x = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * 2f;
        velocity.z = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * 2f;
        
        main.startRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
    }
}
