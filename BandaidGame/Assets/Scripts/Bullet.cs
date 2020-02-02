using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;
    public UnityEvent hit;
    private float lifetime = 5;
    public bool charged = false;

    public AudioClip[] bulletImpact;
    private AudioSource audioSource;

   // private WoundManager woundManager;

    // Start is called before the first frame update
    void Start()
    {
       this.Shoot();

        //make sure bullets don't keep building up over time
        Destroy(gameObject, lifetime);

        audioSource = GetComponent<AudioSource>();
    }

    private void Shoot() {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "bandaidable") {
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
            WoundManager woundManager = collision.gameObject.GetComponentInParent<WoundManager>();
           // audioSource.PlayOneShot(bulletImpact[Random.Range(0, bulletImpact.Length)]);

            if (bandaidable.isBleeding) {
                bandaidable.repair.Invoke();
                woundManager.CuredNoisePlay();
            }
        }

        if(collision.gameObject.tag != "Player")
        {
            audioSource.PlayOneShot(bulletImpact[Random.Range(0, bulletImpact.Length)]);
            this.gameObject.SetActive(false);

            Destroy(this.gameObject, lifetime);
        }
    }

    public void Hit()
    {
        //placeholder for generic bullet collission audio/behaviour/animations
    }


}
