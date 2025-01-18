using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startSize = 0.5f;
        main.startLifetime = 1.0f;
        main.startSpeed = 1.0f;
        Messenger.AddListener<bool>(MsgType.PlayerDash, isDashing =>
        {
            if (isDashing)
            {
                main.startLifetime = 10f;
                main.startSpeed = 9f;
            }
            else
            {
                main.startLifetime = 1f;
                main.startSpeed = 2f;
            }
        });
        Messenger.AddListener(MsgType.PlayerHurt, () =>
        {
            StartCoroutine(HurtCoroutine());
        });
    }

    IEnumerator HurtCoroutine()
    {
        var main = particleSystem.main;
        main.startLifetime = 30f;
        main.startSpeed = 10f;
        yield return new WaitForSeconds(0.3f); // 无敌帧
        main.startLifetime = 1f;
        main.startSpeed = 2f;
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
