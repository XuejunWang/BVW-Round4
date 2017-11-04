using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFallDown : MonoBehaviour {
    public bool fallDownAutomatic = false;
    public float fallDownDelay = 10f;
    public float objectMass = 1f;

    public bool addExplosionForce = false;
    public float explosionForce = 10f;
    public float explosionRadius = 10f;
    public Transform explosionPosition;

    public bool isFloatingObject = false;
    public float objectFloatingHeight = 8.5f;
    public float waterDensity = 1f;

    private Rigidbody rb;
    private AQUAS_Buoyancy buoyancy;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        if (fallDownAutomatic)
        {
            FallDown();
        }
        //FallDown();
        //StartCoroutine(FallDownWithDelay());
    }

    private IEnumerator FallDownWithDelay()
    {
        yield return new WaitForSeconds(fallDownDelay);

        if (isFloatingObject == true)
        {
            buoyancy = gameObject.AddComponent<AQUAS_Buoyancy>();
            buoyancy.waterLevel = objectFloatingHeight;
            buoyancy.waterDensity = waterDensity;
        }

        rb.useGravity = true;
        if (objectMass != 0)
        {
            rb.mass = objectMass;
        }

        if (addExplosionForce == true)
        {
            rb.AddExplosionForce(explosionForce, explosionPosition.transform.position, explosionRadius);
        }
    }

    public void FallDown()
    {
        if (isFloatingObject == true)
        {
            buoyancy = gameObject.AddComponent<AQUAS_Buoyancy>();
            buoyancy.waterLevel = objectFloatingHeight;
            buoyancy.waterDensity = waterDensity;
        }

        rb.useGravity = true;
        if (objectMass != 0)
        {
            rb.mass = objectMass;
        }

        if (addExplosionForce == true)
        {
            rb.AddExplosionForce(explosionForce, explosionPosition.transform.position, explosionRadius);
        }
    }

    public void FallDownAfter(float time)
    {
        StartCoroutine(FallDownAfterCoroutine(time));
    }

    IEnumerator FallDownAfterCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        FallDown();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
