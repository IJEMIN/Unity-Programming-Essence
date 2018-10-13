using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public static List<LivingEntity> players = new List<LivingEntity>();

    public GameObject followCam;
    private LivingEntity playerEntitiy;

    void Start() {
        playerEntitiy = GetComponent<LivingEntity>();
        playerEntitiy.onDeath += () => Invoke("Respawn", 5f);

        if (isServer)
        {
            players.Add(playerEntitiy);
            playerEntitiy.onDeath += () => players.Remove(playerEntitiy);
        }

        if (!isLocalPlayer)
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

        if (isServer)
        {
            players.Add(playerEntitiy);
        }

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}