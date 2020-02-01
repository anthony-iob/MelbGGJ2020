using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;
    public UnityEvent hit;
    private float lifetime = 5;

    // Start is called before the first frame update
    void Start()
    {
       this.Shoot();

        //make sure bullets don't keep building up over time
        Destroy(gameObject, lifetime);
    }

    private void Shoot() {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collide");
        if(collision.gameObject.tag == "bandaidable") {
            Debug.Log("baindaidable");
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
            if(bandaidable.isBleeding) {
                Debug.Log("isBleeding");
                bandaidable.repair.Invoke();
            }
        }

        if(collision.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void Hit()
    {
        //placeholder for generic bullet collission audio/behaviour/animations
    }


}
