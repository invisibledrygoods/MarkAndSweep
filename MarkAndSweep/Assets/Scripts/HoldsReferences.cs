using UnityEngine;
using System.Collections.Generic;

public class HoldsReferences : MonoBehaviour
{
    public static HashSet<HoldsReferences> referenceHolders = new HashSet<HoldsReferences>();
    
    public HashSet<IsGarbageCollectable> references = new HashSet<IsGarbageCollectable>();

    public IsGarbageCollectable Add(IsGarbageCollectable reference)
    {
        references.Add(reference);
        return reference;
    }

    public void Remove(IsGarbageCollectable reference)
    {
        references.Remove(reference);
    }

    public IsGarbageCollectable Replace(IsGarbageCollectable oldReference, IsGarbageCollectable newReference)
    {
        references.Remove(oldReference);
        references.Add(newReference);
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