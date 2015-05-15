using UnityEngine;
using System.Collections;

public class BlockGenerator : MonoBehaviour
{

    public GameObject blockPrefab;
    public float distanceBetweenBlocks = 36f;

    bool hasNewblock = false;
    int spawnedBlocks = 0;
    Vector3 firstBlockPos;

    void Start()
    {
        firstBlockPos = GameObject.FindGameObjectsWithTag("Block")[0].transform.position;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.gameObject.name == "Trigger" && !hasNewblock)
        {
            hit.collider.isTrigger = true;
            Vector3 spawnPos = new Vector3(firstBlockPos.x, firstBlockPos.y, firstBlockPos.z);
            spawnPos.z += distanceBetweenBlocks * spawnedBlocks;

            Instantiate(blockPrefab, spawnPos, Quaternion.identity);
            hasNewblock = true;
            StartCoroutine("onBlockSpawned");
        }
    }

    IEnumerator onBlockSpawned()
    {
        spawnedBlocks++;
        yield return new WaitForEndOfFrame();
        hasNewblock = false;

        GameObject firstBlock = GameObject.FindGameObjectsWithTag("Block")[0];
        Destroy(firstBlock, 0.5f);
    }
}
