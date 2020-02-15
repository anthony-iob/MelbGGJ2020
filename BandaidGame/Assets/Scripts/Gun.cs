using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using EZCameraShake;

public class Gun : Singleton<Gun>
{
    public GameObject projectile;
    public GameObject projectilePosition;
    public float chargeShotSizeMultiplier, chargeShotDelay;
    GameObject loadedBullet;
	public UnityEvent chargedShot;
	public UnityEvent normalShot;
	public UnityEvent chargingStuff;

	float chargeTime = 0;

    public AudioSource bulletFire;
	public AudioSource chargedBulletFire;
	public AudioClip[] bulletSFX;
    public AudioClip maxChargeSFX;
	public AudioSource chargeSFX;
    public ParticleSystem explosion, maxChargeParticle, chargingParticles;

    public bool hasChargedBullet = false;

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
        hasChargedBullet = false;
        loadedBullet = Instantiate(projectile, projectilePosition.transform.position, projectilePosition.transform.rotation) as GameObject;
		if (chargeTime >= chargeShotDelay)
        {
			loadedBullet.GetComponent<Bullet>().charged = true;
			CameraShaker.Instance.ShakeOnce(8f, 2f, 0.1f, 3f);
            loadedBullet.transform.localScale *= chargeShotSizeMultiplier;
            explosion.Play();
			chargedBulletFire.Play();
			chargedShot.Invoke();
            chargeSFX.volume = 0.3f;
		}
		CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, 2f);
		bulletFire.PlayOneShot(bulletSFX[Random.Range(0, bulletSFX.Length)]);
        chargeTime = 0;
		normalShot.Invoke();
	}

    public void Charge()
    {
        chargeTime += Time.deltaTime;
	}

    public float GetChargePercentage()
    {
        return chargeTime / chargeShotDelay;
	}

	public void CancelCharge()
	{
		chargeTime = 0;
		chargeSFX.Stop();
	}

    void Update()
    {
        if(chargeTime >= chargeShotDelay && hasChargedBullet == false)
        {
            chargeSFX.volume = 0.20f;
            if (bulletFire != null) { bulletFire.PlayOneShot(maxChargeSFX); }
            if (maxChargeParticle != null) { maxChargeParticle.Play(); } else Debug.Log("You're missing a particle for the max charge just fyi");
            hasChargedBullet = true;
        }

		if (chargeTime > 0.3 && hasChargedBullet == false)
		{
			chargingParticles.Play();
		}

	}
}