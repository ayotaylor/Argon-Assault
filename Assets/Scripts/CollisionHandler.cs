using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    [SerializeField] ParticleSystem explosionParticleSystem;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)  // might need to make all colliders on player triggers aka click trigger checkbox on each collider
    {
        StartCrashSequence();
        if (other.isTrigger)
        {
            Debug.Log($"{this.name} just collided into {other.gameObject.name}");
        }
    }

    private void StartCrashSequence()
    {
        explosionParticleSystem.Play();
        DisableChildMeshRenderers();
        //GetComponent<MeshRenderer>().enabled = false;
        DisableChildBoxColliders();
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    private void DisableChildBoxColliders()
    {
        Transform[] playerShipParts = GetComponentsInChildren<Transform>();
        foreach (Transform playerShipPart in playerShipParts)
        {
            BoxCollider[] bc = playerShipPart.gameObject.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider b in bc)
            {
                b.enabled = false;
            }
        }
        
    }

    private void DisableChildMeshRenderers()
    {
        MeshRenderer[] rs = GetComponentsInChildren<MeshRenderer>();
        foreach (Renderer r in rs)
        {
            r.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)    // might remove if i make colliders on player triggers
    {
        StartCrashSequence();
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
