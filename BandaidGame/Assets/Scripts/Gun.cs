using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Gun : Singleton<Gun>
{
    public GameObject projectile;
    public GameObject projectilePosition;
    public float chargeShotSizeMultiplier = 2, chargeShotDelay = 1;
    GameObject loadedBullet;
	

    float chargeTime = 0;

    public AudioSource bulletFire;
	public AudioSource chargedBulletFire;
	public AudioClip[] bulletSFX;
	public AudioSource chargeSFX;
    public ParticleSystem explosion;

    private void Start()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            bulletFire = GetComponent<AudioSource>();
        }
        else Debug.Log("An AudioSource is missing from an object which makes noises");
    }

    public void Shoot()
    {
        loadedBullet = Instantiate(projectile, projectilePosition.transform.position, projectilePosition.transform.rotation) as GameObject;
        if (chargeTime >= chargeShotDelay)
        {
            loadedBullet.GetComponent<Bullet>().charged = true;
			CameraShaker.Instance.ShakeOnce(8f, 2f, 0.1f, 3f);
            loadedBullet.transform.localScale *= chargeShotSizeMultiplier;
            explosion.Play();
			chargedBulletFire.Play();
		}
		CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, 2f);
		bulletFire.PlayOneShot(bulletSFX[Random.Range(0, bulletSFX.Length)]);
        chargeTime = 0;
    }

    public void Charge()
    {
        chargeTime += Time.deltaTime;
	}

    public float GetChargePercentage()
    {
        return chargeTime / chargeShotDelay;
	}
}
