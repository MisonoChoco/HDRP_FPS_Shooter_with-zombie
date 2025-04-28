using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP;
    public GameObject bloodyVignette;

    public TextMeshProUGUI playerHpUI;
    public GameObject gameOverUI;

    public CinemachineCamera playerCamera;
    public CinemachineCamera deathCamera;

    public Volume globalVolume;

    //private Vignette vignette;
    private DepthOfField DepthOfField;

    public bool isDead;

    private void Start()
    {
        playerHpUI.text = $"Health {HP}";
        playerCamera.Priority = 10;
        deathCamera.Priority = 5;

        //if (globalVolume.profile.TryGet(out vignette))
        //{
        //    vignette.active = false;
        //}
        //if (globalVolume.profile.TryGet(out DepthOfField))
        //{
        //    DepthOfField.active = false;
        //}
    }

    private void Update()
    {
        SwitchToDeathCamera();
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            print("Dead");
            PlayerDead();
            isDead = true;
        }
        else
        {
            print("Hit");
            StartCoroutine(bloodyScreenEffect());
            playerHpUI.text = $"Health {HP}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(2f);

        GetComponent<FirstPersonController>().enabled = false;
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Player>().enabled = false;

        GetComponentInChildren<Animator>().enabled = true;
        playerHpUI.gameObject.SetActive(false);

        GetComponent<Blackout>().StartFade();

        StartCoroutine(ShowGameOverUI());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //OnPlayerHit();
            //Destroy(collision.gameObject);
        }
    }

    //public void OnPlayerHit()
    //{
    //    StartCoroutine(ApplyBlurEffect());
    //}

    //private IEnumerator ApplyBlurEffect()
    //{
    //    if (vignette != null)
    //    {
    //        vignette.active = true;
    //        //vignette.focusMode.value = DepthOfFieldMode.Manual;
    //        //vignette.nearFocusStart.value = 0.1f;
    //        //vignette.nearFocusEnd.value = 1.0f;

    //        yield return new WaitForSeconds(3f);

    //        vignette.active = false;
    //    }
    //}

    private void SwitchToDeathCamera()
    {
        CinemachineCamera playerCamera = GameObject.Find("CinemachineCameraLive").GetComponent<CinemachineCamera>();
        CinemachineCamera deathCamera = GameObject.Find("CinemachineCameraDeath").GetComponent<CinemachineCamera>();

        if (Input.GetKeyDown(KeyCode.V))
        {
            playerCamera.Priority = 5;
            deathCamera.Priority = 10;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerCamera.Priority = 10;
            deathCamera.Priority = 5;
        }

        if (playerCamera != null && deathCamera != null)
        {
        }
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
        if (DepthOfField != null)
        {
            DepthOfField.active = true;
            //vignette.focusMode.value = DepthOfFieldMode.Manual;
            //vignette.nearFocusStart.value = 0.1f;
            //vignette.nearFocusEnd.value = 1.0f;

            yield return new WaitForSeconds(20f);

            DepthOfField.active = false;
        }
    }

    private IEnumerator bloodyScreenEffect()
    {
        if (bloodyVignette.activeInHierarchy == false)
        {
            bloodyVignette.SetActive(true);
        }

        var image = bloodyVignette.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyVignette.activeInHierarchy)
        {
            bloodyVignette.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
    }
}