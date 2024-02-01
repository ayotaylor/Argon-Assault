using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] int scorePerHit = 5;

    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        scoreBoard = FindObjectOfType<ScoreBoard>();    // never use this method in update. it can be very resource/compute intensive
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {

    }

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        //Debug.Log($"{this.name}: I'm hit by {other.gameObject.name}");
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFx, this.transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;  // setting parent of vfx to parent (spawnAtRuntime) in scene
        Destroy(this.gameObject);
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVfx, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;  // setting parent of vfx to parent (spawnAtRuntime) in scene
        hitPoints--;
    }
}
