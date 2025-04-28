using UnityEngine;
using UnityEngine.VFX;

public class GlobalReference : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GlobalReference Instance { get; set; }

    public GameObject bulletImpactEffectPrefab;

    public GameObject FragExplosionEffect;
    public GameObject SmokeExplosionEffect;
    public GameObject BloodSprayEffect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}