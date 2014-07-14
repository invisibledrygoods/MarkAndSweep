using UnityEngine;
using System.Collections.Generic;
using Require;

public class IsGarbageCollectableTest : TestBehaviour
{
    IsGarbageCollectable it;
    Queue<HoldsReferences> references = new Queue<HoldsReferences>();

    public override void Spec()
    {
        Given("it is garbage collectable")
            .When("something references it")
            .ThenWithin("1 second", "it should still exist")
            .Because("if it is referenced it should not be collected");

        Given("it is garbage collectable")
            .When("nothing references it")
            .ThenWithin("1 second", "it should be destroyed")
            .Because("if it isn't referenced it should be collected");

        Given("it is garbage collectable")
            .And("something references it")
            .When("one thing stops referencing it")
            .ThenWithin("1 second", "it should be destroyed")
            .Because("it needs to know when a reference is removed");

        Given("it is garbage collectable")
            .And("something references it")
            .And("something references it")
            .When("one thing stops referencing it")
            .ThenWithin("1 second", "it should still exist")
            .Because("it's only collected when the reference count drops to zero");
    }

    public void ItIsGarbageCollectable()
    {
        it = new GameObject().transform.Require<IsGarbageCollectable>();
    }

    public void SomethingReferencesIt()
    {
        HoldsReferences reference = new GameObject().transform.Require<HoldsReferences>();
        reference.Add(it);
        references.Enqueue(reference);
    }

    public void NothingReferencesIt()
    {
    }

    public void OneThingStopsReferencingIt()
    {
        Destroy(references.Dequeue().gameObject);
    }

    public void ItShouldStillExist()
    {
        if (it == null)
        {
            throw new System.Exception("Expecting object to not have been garbage collected but it was");
        }
    }

    public void ItShouldBeDestroyed()
    {
        if (it != null)
        {
            throw new System.Exception("Expecting object to have been garbage collected but it wasn't");
        }
    }
}
