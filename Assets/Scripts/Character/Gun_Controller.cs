using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dartPrefab;
    public Transform firePoint;
    [SerializeField]public float fireForce;
    Vector2 mousePosition;
    public GameObject pivotObject;

    public void Fire()
    {
        GameObject dart = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);
        dart.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    private void Update() {
        
    }
}
