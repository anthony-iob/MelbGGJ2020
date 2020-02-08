using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;
    public UnityEvent hit;
    public float lifetime;
    public bool charged = false;

    public AudioClip[] bulletImpact;
    private AudioSource audioSource;

    private Vector3 newSize;
    private float currentTime = 0;

    Vector3 originalScale;
    Vector3 destinationScale;

    // Start is called before the first frame update
    void Start()
    {
        destinationScale = new Vector3(0.0f, 0.0f, 0.0f);
        originalScale = this.gameObject.transform.localScale;
        this.Shoot();

        //make sure bullets don't keep building up over time / destroy after period - can be disabled if we put in particles on collision.
        Destroy(gameObject, lifetime);

        audioSource = GetComponent<AudioSource>();
    }

    private void Shoot()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
        // ShrinkOverLifetime();    
    }
    void OnCollisionEnter(Collision collision)
    {
        if (charged)
        {
            if (collision.gameObject.tag == "npc" || collision.gameObject.tag == "bandaidable")
            {
                
                if (collision.gameObject.GetComponentInChildren<WoundManager>() != null)
                {
                    collision.gameObject.GetComponentInChildren<WoundManager>().RepairAllWounds();
                }
                else if (collision.gameObject.GetComponentInParent<WoundManager>() != null)
                {
                    collision.gameObject.GetComponentInParent<WoundManager>().RepairAllWounds();
                    // Debug.Log("you hit a wound but it's okay because I fixed it");
                }
            }
        }
        else if (collision.gameObject.tag == "bandaidable")
        {
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
            WoundManager woundManager = collision.gameObject.GetComponentInParent<WoundManager>();

            if (bandaidable.isBleeding)
            {
                bandaidable.repair.Invoke();
                woundManager.CuredNoisePlay();
                
                
            }
        }

        if (collision.gameObject)
        {
            if (audioSource != null) { audioSource.PlayOneShot(bulletImpact[Random.Range(0, bulletImpact.Length)]); }
            this.GetComponent<Rigidbody>().useGravity = true;


            /* turned this off because it kills the audio - also kinda cool having the things around...instead should turn off and instantiate a particle system -> Audio on particle
            will match action - so hit wall = wall sound, hit dude = a sparkle for healing and a success sound. */

            //this.gameObject.SetActive(false); 

        }
    }


    void Update()
    {
        currentTime += Time.deltaTime;
       //  if (!charged)
       // { this.gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / lifetime); }
       // else if (charged)
       // {
       // this has been commented out because it makes frankies and the player fly. 

       //  newSize = this.gameObject.transform.localScale *= 2;   
       //  this.gameObject.transform.localScale = Vector3.Lerp(newSize, destinationScale, currentTime / lifetime);
       // }

    }

    void ShrinkOverLifetime()
    {




    }


    public void Hit()
    {
        //placeholder for generic bullet collission audio/behaviour/animations
    }
}



