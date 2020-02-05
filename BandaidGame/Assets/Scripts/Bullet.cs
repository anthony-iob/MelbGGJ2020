using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;
    public UnityEvent hit;
    public float lifetime = 1.5f;
    public bool charged = false;

    public AudioClip[] bulletImpact;
    private AudioSource audioSource;

   // private WoundManager woundManager;

    // Start is called before the first frame update
    void Start()
    {
       this.Shoot();

        //make sure bullets don't keep building up over time / destroy after period - can be disabled if we put in particles on collision.
        Destroy(gameObject, lifetime);

        audioSource = GetComponent<AudioSource>();
    }

    private void Shoot() {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
    }

    void OnCollisionEnter(Collision collision) {

        if (charged) 
        {
            if(collision.gameObject.tag == "npc")
            {
                Debug.Log("REPAIRING ALL WOUNDS");
                collision.gameObject.GetComponentInChildren<WoundManager>().RepairAllWounds(); ;
            }
        } else if (collision.gameObject.tag == "bandaidable") {
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
			WoundManager woundManager = collision.gameObject.GetComponentInParent<WoundManager>();

			if (bandaidable.isBleeding) {
                bandaidable.repair.Invoke();
                woundManager.CuredNoisePlay();
            }
        }

        if(collision.gameObject)
        {
            if (audioSource != null) { audioSource.PlayOneShot(bulletImpact[Random.Range(0, bulletImpact.Length)]);}

            /* turned this off because it kills the audio - also kinda cool having the things around...instead should turn off and instantiate a particle system -> Audio on particle
            will match action - so hit wall = wall sound, hit dude = a sparkle for healing and a success sound. */

            //this.gameObject.SetActive(false); 

        }
    }

    public void Hit()
    {
        //placeholder for generic bullet collission audio/behaviour/animations
    }


}
