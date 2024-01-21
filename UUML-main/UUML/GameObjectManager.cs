using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager
{
    public static GameObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObjectManager();
            }
            return instance;
        }
    }

    public class GameObjectClass : MonoBehaviour
    {
        //Generic class, use a real class name for your game.
    }

    public const float CollectionInterval = 3f;

    public List<GameObjectClass> items = new List<GameObjectClass>();
    // ... other lists

    /* Networking
    public PlayerControllerB localPlayer;
    */
    // ... other fields

    public IEnumerator CollectObjects()
    {
        while (true)
        {
            //Uncomment the network games to use this
            /*
            InitializeReferences();
            */
            ClearLists();

            CollectObjectsOfType(items);
            // ... other calls

            yield return new WaitForSeconds(CollectionInterval);
        }
    }

    //For network games. A template for getting local player
    /*
    public void InitializeReferences()
    {
        localPlayer = NetworkManager.Instance?.localPlayerController;
        // ... other initializations
    }
    */

    public void ClearLists()
    {
        items.Clear();
        // ... other clears
    }

    public void CollectObjectsOfType<T>(List<T> list, Predicate<T> predicate = null) where T : MonoBehaviour
    {
        foreach (var obj in UnityEngine.Object.FindObjectsOfType<T>())
        {
            if (predicate == null || predicate(obj))
            {
                list.Add(obj);
            }
        }
    }

    public List<T> FindObjectsOfType<T>(Predicate<T> predicate = null) where T : MonoBehaviour
    {
        var objects = UnityEngine.Object.FindObjectsOfType<T>();
        return predicate == null ? new List<T>(objects) : new List<T>(objects).FindAll(predicate);
    }

    private static GameObjectManager instance;
}