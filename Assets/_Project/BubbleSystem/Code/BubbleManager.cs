using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BubbleManager : MonoBehaviour
{
    public GameObject BubbleClass; // Class from which to spawn bubbles
    public List<GameObject> SpawnPositions;
    public static BubbleManager Instance { get; private set; } // Singleton pattern

    private List<GameObject> BubbleList; // all spawned bubbles
    private int BubbleCount; // tracks the number of bubbles that still exist in the level, both spawned or unspawned
    public float SpawnDelay;

    public bool IsSpawning;

    [Header("Sound")]
    [SerializeField] AudioClip createBubbleSound;
    [SerializeField] AudioClip popBubbleSound;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BubbleList = new List<GameObject>();
        BubbleCount = 0;
        SpawnPositions = SpawnPositions.OrderBy(go => -go.transform.position.y).ToList(); // sort by y-position, negative so the values with the highest y are first
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnBubblesRoutine()
    {
        if (BubbleCount == 0)
        {
            yield break;
        }
        while (enabled)
        {
            IsSpawning = true;
            Vector3 position = SpawnPositions[BubbleList.Count % SpawnPositions.Count].transform.position;
            AddBubble(position);
            BubbleCount--; // this is modified by the line above, we don't want that

            if (BubbleList.Count == BubbleCount)
            {
                Debug.Log("Stopping coroutine...");
                IsSpawning = false;
                StopAllCoroutines();
                yield break;
            }
            yield return new WaitForSeconds(Random.Range(0.0f + SpawnDelay, 0.1f + SpawnDelay));
        }
    }

    // Spawns a bubble with the specified world-space location, with a random rotation
    public void AddBubble(Vector3 position)
    {
        var from = Random.onUnitSphere;
        var to = Random.onUnitSphere;
        from.z = 0.0f;
        to.z = 0.0f;
        Quaternion rotation = Quaternion.FromToRotation(from, to);
        var obj = Instantiate(BubbleClass, position, rotation);
        obj.transform.localScale *= Random.Range(0.65f, 1.0f);

        AudioManager.Instance.PlaySFX(createBubbleSound);

        BubbleCount++;
        BubbleList.Add(obj);
    }

    public int RandomBubbleIndex()
    {
        return Mathf.RoundToInt(Random.Range(0.0f, (float)BubbleList.Count - 1.0f));
    }

    // remove bubble at a specific index
    public void RemoveBubble(int index)
    {
        BubbleList[index].GetComponentInChildren<popInAtSpawn>().PopOut();
        BubbleList.RemoveAt(index);
        BubbleCount--;

        AudioManager.Instance.PlaySFX(popBubbleSound);
    }

    // removes a bubble from a random location
    public void RemoveBubble()
    {
        RemoveBubble(RandomBubbleIndex());
    }

    // precondition: index must be between 0 and BubbleList.Count
    public Transform TransformAtIndex(int index)
    {
        return BubbleList[index].transform;
    }

    // Despawn bubbles, but maintain the current amount of spawned bubbles in the BubbleCount Variable
    // ! This function needs 0.4 seconds to safely complete
    public void RemoveAllBubbles()
    {
        BubbleCount += BubbleCount; // the operation below will subtract the current number of existing bubbles
        for (int i = 0; i < BubbleList.Count; i++)
        {
            Invoke(nameof(RemoveBubble), Random.Range(0.1f, 0.4f));
        }
        // the invoke above modifies BubbleCount, so we keep the initial value
    }

    // Re-Add bubbles that existed before, based on BubbleCount
    // Precondition: 
    public void AddExistingBubbles()
    {
        if (BubbleList.Count != 0)
       {
           Debug.Log("Attempted re-instantiation of bubbles with nonempty list. Aborting!");
           return;
       }

       Debug.Log(BubbleCount);

       StartCoroutine(SpawnBubblesRoutine());
    }

    public IEnumerator Move(bool moveNext)
    {
        RemoveAllBubbles();
        yield return new WaitForSeconds(0.7f);

        Vector3 offset = new Vector3(4.0f, 0.0f, 0.0f);
        if (moveNext)
        {
            transform.position += offset;
        }
        else
        {
            transform .position -= offset;
        }
        AddExistingBubbles();
    }

    public void HandleDamage(int amount)
    {
        StartCoroutine(HandleDamageAsync(amount));
    }

    public IEnumerator HandleDamageAsync(int amount)
    {
        IsSpawning = true;
        while (enabled)
        {
            Debug.Log("Amount of damage: " + amount.ToString());
            Vector3 position = SpawnPositions[BubbleList.Count % SpawnPositions.Count].transform.position;
            if (amount == 0)
            {
                IsSpawning = false;
                StopAllCoroutines();
                yield break;
            }
            else if (amount > 0)
            {
                AddBubble(position);
                amount--;
            }
            else
            {
                RemoveBubble();
                amount++;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
