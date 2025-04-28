using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;

    public AudioSource EmptyShooting;

    public AudioSource Reloading_AK;
    public AudioSource Reloading_M1911;

    public AudioClip AK47Shot;
    public AudioClip M1911Shot;

    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;

    public AudioClip zombieDeath;
    public AudioClip zombieWalk;
    public AudioClip zombieAttack;
    public AudioClip zombieChase;
    public AudioClip zombieHurt;

    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDie;
    public AudioClip gameOverMusic;

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

    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.AK47:
                ShootingChannel.PlayOneShot(AK47Shot);
                break;

            case Weapon.WeaponModel.HandgunM1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.AK47:
                Reloading_AK.Play();
                break;

            case Weapon.WeaponModel.HandgunM1911:
                Reloading_M1911.Play();
                break;
        }
    }
}