using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform GunTransform;
    public float speed = 0.1f;
    Vector3 DirectionToGun;
    Vector3 MoveEachFrame;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        GunTransform = GameObject.Find("GunManual").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DirectionToGun = (transform.position - GunTransform.position).normalized;
        MoveEachFrame = -DirectionToGun * speed * Time.fixedDeltaTime;

        transform.position += MoveEachFrame;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        //Debug.Log("We hit :" + collision.gameObject.name);
    }
}
