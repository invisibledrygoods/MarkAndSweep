using UnityEngine;
using System.Collections.Generic;
using Require;

public class HoldsReferences : MonoBehaviour
{
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
        Add(reference.gameObject);
        return reference;
    }

    public void Remove(GameObject reference)
    {
        IsGarbageCollectable collectable = reference.transform.Require<IsGarbageCollectable>();
        references.Remove(collectable);
        collectable.RemoveReference();
    }

    public void Remove(Component reference)
    {
        Remove(reference.gameObject);
    }

    public GameObject Replace(GameObject oldReference, GameObject newReference)
    {
        Remove(oldReference);
        Add(newReference);
        return newReference;
    }

    public T Replace<T>(Component oldReference, T newReference) where T : Component
    {
        Replace(oldReference.gameObject, newReference.gameObject);
        return newReference;
    }

    void OnDestroy()
    {
        foreach (IsGarbageCollectable collectable in references)
        {
            collectable.RemoveReference();
        }
    }
}