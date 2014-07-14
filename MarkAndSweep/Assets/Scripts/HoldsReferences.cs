using UnityEngine;
using System.Collections.Generic;
using Require;

public class HoldsReferences : MonoBehaviour
{
    public static HashSet<HoldsReferences> referenceHolders = new HashSet<HoldsReferences>();
    
    public HashSet<IsGarbageCollectable> references = new HashSet<IsGarbageCollectable>();

    public GameObject Add(GameObject reference)
    {
        references.Add(reference.transform.Require<IsGarbageCollectable>());
        return reference;
    }

    public T Add<T>(T reference) where T : Component
    {
        references.Add(reference.transform.Require<IsGarbageCollectable>());
        return reference;
    }

    public void Remove(GameObject reference)
    {
        references.Remove(reference.transform.Require<IsGarbageCollectable>());
    }

    public void Remove<T>(T reference) where T : Component
    {
        references.Remove(reference.transform.Require<IsGarbageCollectable>());
    }

    public GameObject Replace(GameObject oldReference, GameObject newReference)
    {
        references.Remove(oldReference.transform.Require<IsGarbageCollectable>());
        references.Add(newReference.transform.Require<IsGarbageCollectable>());
        return newReference;
    }

    public T Replace<T>(T oldReference, T newReference)
        where T : Component
    {
        references.Remove(oldReference.transform.Require<IsGarbageCollectable>());
        references.Add(newReference.transform.Require<IsGarbageCollectable>());
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
}