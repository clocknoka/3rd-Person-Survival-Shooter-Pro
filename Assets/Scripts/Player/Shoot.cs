using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*
// Shoot.cs
//*
// Class behaviour
//*
// @category   3rd Person Survival Shooter Pro
// @tutorial   GameDevHQ
// @author     Dave González
// @copyright  2021 Dave González
// @version    CVS: 0.1
// @link       website
//*
public class Shoot : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootWeapon();
    }

    // Shoots the weapon
    private void ShootWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("We hit" + hitInfo.collider.gameObject.name);
                Health health = hitInfo.collider.GetComponent<Health>();

                if (health != null)
                    health.Damage(25);
            }
        }
    }

}