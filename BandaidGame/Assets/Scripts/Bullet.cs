using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public int travelSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.Shoot();
    }

    private void Shoot() {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * travelSpeed);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "bandaidable") {
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
            if(bandaidable.isBleeding) {
                bandaidable.repair.Invoke();
            }
        }

        if(collision.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        
    }
}
