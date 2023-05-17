using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Camera fpsCam;
    public float shotDelay = 1.5f;

    private bool canShoot = true;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        // bulletSpeed = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0))
        {
            canShoot = false; // Prevent shooting again until delay is over
            Invoke("ResetShotDelay", shotDelay); // Set delay for shooting again
            ShootBullet(bulletSpawnPoint);
        }

        //foreach (var touch in Input.touches) {
        //    if (touch.phase == TouchPhase.Began) {
                // Construct a ray from the current touch coordinates
        //        var ray = Camera.main.ScreenPointToRay (touch.position);
        //        if (Physics.Raycast (ray)) {
                    // Create a particle if hit
        //            ShootBullet(transform);
        //        }
        //        Torre torre = Torre.instance;
        //        torre.dispara();
        //    }
        //}
        
    }

    void ShootBullet(Transform firePoint)
    {
        //RaycastHit hit;
        
        Torre torre = GameObject.Find("Torre").GetComponent<Torre>();
        if (torre.visible & !torre.terminado) {
            //Physics.Raycast(fpsCam.transform.position, bulletSpawnPoint.forward)
            InstantiateBullet(firePoint);

            torre.dispara();
        }
    }

    void InstantiateBullet(Transform origin)
    {
        var bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        bullet.GetComponent<Rigidbody>().velocity = fpsCam.transform.forward * bulletSpeed;
    }

    void ResetShotDelay()
    {
        canShoot = true; // Allow shooting again
    }
}
