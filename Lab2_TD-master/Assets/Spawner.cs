using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnRadius = 30f;

    //This is a Singleton of the particle Spawner. there is only one instance 
    // of this Spawner, so we can store it in a static variable named s.
    static public Spawner S;
    static public List<ParticleUnity> ParticleUnitys;

    // These fields allow you to adjust the spawning behavior of the spheres
    [Header("Set in Inspector: Spawning")]
    public GameObject particleUnityPrefab;
    public Transform particleAnchor;
    public int numParticles = 100;
    public float spawnDelay = 0.1f;
    Vector3[] _SpawnPoints;
    

    //add variables here that spawned particles will inherit    

    void Awake()
    {
        //Set the Singleton S to be this instance of particle spawner
        S = this;
        //Start instantiation of the particles
        ParticleUnitys = new List<ParticleUnity>();//the list holding the particles
        _SpawnPoints = GetPointsOnSphere(numParticles);
        InstantiateParticleUnitys();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 180f);
    }
    //a method to spawn the particles
    public void InstantiateParticleUnitys()
    {
        Vector3 SpawnPosition = particleAnchor.position + (_SpawnPoints[ParticleUnitys.Count] * SpawnRadius);
        GameObject go = Instantiate(particleUnityPrefab,SpawnPosition,Quaternion.identity);
        ParticleUnity b = go.GetComponent<ParticleUnity>();
        //b.transform.SetParent(particleAnchor, true);
        b.MyParent = this.transform;
        b.TargetOffset = (_SpawnPoints[ParticleUnitys.Count] * SpawnRadius);
        ParticleUnitys.Add(b);
        if (ParticleUnitys.Count < numParticles)
        {
            Invoke("InstantiateParticleUnitys", spawnDelay);
        }
    }
    private void OnDrawGizmos()
    {
        Vector3[] points = GetPointsOnSphere(100);
        foreach(Vector3 point in points)
        {
           // Gizmos.DrawSphere(point * SpawnRadius, 1f);
        }
    }
    Vector3[] GetPointsOnSphere(int nPoints)
    {
        float fPoints = (float)nPoints;

        Vector3[] points = new Vector3[nPoints];

        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2 / fPoints;

        for (int k = 0; k < nPoints; k++)
        {
            float y = k * off - 1 + (off / 2);
            float r = Mathf.Sqrt(1 - y * y);
            float phi = k * inc;

            points[k] = new Vector3(Mathf.Cos(phi) * r, y, Mathf.Sin(phi) * r);
        }

        return points;
    }
}
