using UnityEngine;
using System.Collections;
using Require;
using Shouldly;
using System.Linq;

public class HoldsReferencesTest : TestBehaviour {
    HoldsReferences it;
    IsGarbageCollectable aThing;
    IsGarbageCollectable anotherThing;
    HoldsReferences aClass;
    GameObject aGameObject;
    
    public override void Spec()
    {
        Given("it holds references")
            .And("a thing is garbage collectable")
            .When("a reference to the thing is added")
            .Then("it should reference the thing")
            .Because("Add(reference) adds the reference");

        Given("it holds references")
            .And("a game object exists")
            .When("a reference to the game object is added")
            .Then("the game object should be garbage collectable")
            .Because("adding a game object as a reference automatically makes it collectable");

        Given("it holds references")
            .And("a different class exists")
            .When("a reference to the different class is added")
            .Then("the different class should be garbage collectable")
            .Because("adding another class as a reference automatically makes its game object collectable");

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

    public void AGameObjectExists()
    {
        aGameObject = new GameObject();
    }

    public void ADifferentClassExists()
    {
        aClass = new GameObject().transform.Require<HoldsReferences>();
    }

    public void AReferenceToTheThingIsAdded()
    {
        it.Add(aThing);
    }

    public void AReferenceToTheGameObjectIsAdded()
    {
        it.Add(aGameObject);
    }

    public void AReferenceToTheDifferentClassIsAdded()
    {
        it.Add(aClass);
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

    public void TheGameObjectShouldBeGarbageCollectable()
    {
        aGameObject.GetComponent<IsGarbageCollectable>().ShouldNotBe(null);
    }

    public void TheDifferentClassShouldBeGarbageCollectable()
    {
        aClass.GetComponent<IsGarbageCollectable>().ShouldNotBe(null);
    }
}
