using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this.Shoot();
    }

    private void Shoot() {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
    }

    void OnCollisionEnter(Collision collision) {
        // Debug.Log("Collision");
        if(collision.gameObject.tag == "bandaidable") {
            Bandaidable bandaidable = collision.gameObject.GetComponent<Bandaidable>();
            if(bandaidable) {
                bandaidable.Repair();
            }
        }
    }
}
