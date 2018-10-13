using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public GameObject followCam;

    void Start() {
        GetComponent<PlayerHealth>().onDeath += () => Invoke("Respawn", 5f);

        if (isLocalPlayer)
        {
            
        }
        else
        {
            followCam.SetActive(false);
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = Color.green;
            }
        }
    }

    public void Respawn() {
        if (isLocalPlayer)
        {
            Transform spawnPosition = NetworkManager.singleton.GetStartPosition();

            transform.position = spawnPosition.position;
            transform.rotation = spawnPosition.rotation;
        }

        GetComponent<PlayerShooter>().gun.ammoRemain += 50;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}