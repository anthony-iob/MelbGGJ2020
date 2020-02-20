using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;
   // public UnityEvent hit;
    public float lifetime;
    public bool charged = false;

    public AudioClip[] bulletImpact;
    private AudioSource audioSource;
    public AudioSource comboAudio;

    private Vector3 newSize;
    private float currentTime = 0;

    public int combo;
    public AudioClip[] comboNoises;

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
        combo = 0;
    }

    private void Shoot()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
        // ShrinkOverLifetime();    
    }

    void OnTriggerEnter(Collider collision)
    {
        if (charged)
        {
            if (collision.gameObject.tag == "npc" || collision.gameObject.tag == "bandaidable")
            {

                if (collision.gameObject.GetComponentInChildren<WoundManager>() != null)
                {
                    WoundManager woundManagerChild = collision.gameObject.GetComponentInChildren<WoundManager>();
                    if (woundManagerChild.openWounds > 0)
                    {
                        woundManagerChild.RepairAllWounds();
                        ComboTrigger();
                    }
                }
                else if (collision.gameObject.GetComponentInParent<WoundManager>() != null)
                {
                    WoundManager woundManagerParent = collision.gameObject.GetComponentInParent<WoundManager>();
                    if (woundManagerParent.openWounds > 0)
                    {
                        woundManagerParent.RepairAllWounds();
                        ComboTrigger();
                    }
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
                ComboTrigger();
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

    void ComboTrigger()
    {

        if (combo >= 1)
        {

            if ((combo -1) <= comboNoises.Length)            //check to make sure there's clips left to combo up and its actually a combo and not first hit. 
            {
                comboAudio.clip = comboNoises[combo - 1];      //set clip to combo noise of the current combo -1 to compensate for checks. 
                comboAudio.PlayOneShot(comboAudio.clip);  //play it
                combo++;                                //put the combo up one
            }

            else if (combo > comboNoises.Length)
            {
                comboAudio.PlayOneShot(comboAudio.clip);
                combo++;                                //there's no clips to combo up so we gon' keep playing the last one whilst adding combo score
            }

                      
            if (combo >= 3)
            {
                 GameManager.instance.currentBloodLevel -= (combo * 150);

                //take some flood away if combo is 3 or more.
                //this might be OP because it stacks with each consecutive combo, so like 5, 6+ is gonna take away a whole lotta flood. 
                //May need to implement at fixed values per hit, test. 

                Debug.Log("Your combo WOULD HAVE removed " + (combo * 100) + " flood level!!");
            }
        }
        else combo++;
        if (combo > 1) { Debug.Log(combo + " hit combo!"); }


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
}



