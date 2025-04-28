using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    public VisualEffect explosionEffect;

    public Volume globalVolume;
    private DepthOfField depthOfField;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit " + collision.gameObject.name + " !");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit wall!");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    print("hit me!");
        //    Destroy(gameObject);
        //}

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Enemy>().isDead == false)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
            if (collision.gameObject.GetComponent<Enemy>().isDead == true)
            {
                collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            CreateBloodSprayFX(collision);
            CreateVFX(collision);
            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayFX(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(GlobalReference.Instance.BloodSprayEffect, contact.point, Quaternion.LookRotation(contact.normal));

        bloodSprayPrefab.transform.SetParent(objectHit.gameObject.transform);
    }

    private void CreateVFX(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        Instantiate(explosionEffect, contact.point, Quaternion.LookRotation(contact.normal));

        explosionEffect.transform.SetParent(objectHit.gameObject.transform);
    }

    private void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        GameObject hole = Instantiate(GlobalReference.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}