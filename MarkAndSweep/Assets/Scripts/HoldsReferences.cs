using UnityEngine;
using System.Collections.Generic;
using Require;

public class HoldsReferences : MonoBehaviour
{
    public static HashSet<HoldsReferences> referenceHolders = new HashSet<HoldsReferences>();
    
    public HashSet<IsGarbageCollectable> references = new HashSet<IsGarbageCollectable>();

    public GameObject Add(GameObject reference)
    {
        IsGarbageCollectable collectable = reference.transform.Require<IsGarbageCollectable>();
        references.Add(collectable);
        collectable.AddReference();
        return reference;
    }

    public T Add<T>(T reference) where T : Component
    {
        IsGarbageCollectable collectable = reference.transform.Require<IsGarbageCollectable>();
        references.Add(collectable);
        collectable.AddReference();
        return reference;
    }

    public void Remove(GameObject reference)
    {
        IsGarbageCollectable collectable = reference.transform.Require<IsGarbageCollectable>();
        references.Remove(collectable);
        collectable.RemoveReference();
    }

    public void Remove<T>(T reference) where T : Component
    {
        IsGarbageCollectable collectable = reference.transform.Require<IsGarbageCollectable>();
        references.Remove(collectable);
        collectable.RemoveReference();
    }

    public GameObject Replace(GameObject oldReference, GameObject newReference)
    {
        Remove(oldReference);
        Add(newReference);
        return newReference;
    }

    public T Replace<T>(T oldReference, T newReference)
        where T : Component
    {
        Remove(oldReference);
        Add(newReference);
        return newReference;
    }

    void OnEnable()
    {
        referenceHolders.Add(this);
    }

    void OnDisable()
    {
        referenceHolders.Remove(this);
    }

    void OnDestroy()
    {
        foreach (IsGarbageCollectable collectable in references)
        {
            collectable.RemoveReference();
        }
    }
}