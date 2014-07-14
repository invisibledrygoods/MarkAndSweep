using UnityEngine;
using System.Collections;
using Require;
using Shouldly;
using System.Linq;

public class HoldsReferencesTest : TestBehaviour {
    HoldsReferences it;
    IsGarbageCollectable aThing;
    IsGarbageCollectable anotherThing;
    
    public override void Spec()
    {
        Given("it holds references")
            .And("a thing is garbage collectable")
            .When("a reference to the thing is added")
            .Then("it should reference the thing")
            .Because("Add(reference) adds the reference");

        Given("it holds references")
            .And("a thing is garbage collectable")
            .When("a reference to the thing is added")
            .And("a reference to the thing is added")
            .Then("it should only reference the thing once")
            .Because("multiple references to the same object are only counted once");

        Given("it holds references")
            .And("a thing is garbage collectable")
            .And("a reference to the thing is added")
            .When("a reference to the thing is removed")
            .Then("it should not reference the thing")
            .Because("Remove(reference) removes the reference");

        Given("it holds references")
            .And("a thing is garbage collectable")
            .And("another thing is garbage collectable")
            .And("a reference to the thing is added")
            .When("the thing is replaced with the other thing")
            .Then("it should reference the other thing")
            .And("it should not reference the thing")
            .Because("Replace(oldReference, newReference) replaces one reference with another");
    }

    public void ItHoldsReferences()
    {
        it = transform.Require<HoldsReferences>();
    }

    public void AThingIsGarbageCollectable()
    {
        aThing = new GameObject().transform.Require<IsGarbageCollectable>();
    }

    public void AnotherThingIsGarbageCollectable()
    {
        anotherThing = new GameObject().transform.Require<IsGarbageCollectable>();
    }

    public void AReferenceToTheThingIsAdded()
    {
        it.Add(aThing);
    }

    public void AReferenceToTheThingIsRemoved()
    {
        it.Remove(aThing);
    }

    public void TheThingIsReplacedWithTheOtherThing()
    {
        it.Replace(aThing, anotherThing);
    }

    public void ItShouldReferenceTheThing()
    {
        it.references.ShouldContain(aThing);
    }

    public void ItShouldOnlyReferenceTheThingOnce()
    {
        it.references.Where(_ => _ == aThing).Count().ShouldBe(1);
    }

    public void ItShouldNotReferenceTheThing()
    {
        it.references.ShouldNotContain(aThing);
    }

    public void ItShouldReferenceTheOtherThing()
    {
        it.references.ShouldContain(anotherThing);
    }
}
