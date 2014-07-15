MarkAndSweep
============

A bit of automatic resource management.

Example
-------

#### It happens magically so there's really very little to it

    class Cirno
    {
        void Update()
        {
            GetComponent<HoldsReferences>().Add(CreateBullet());
        }
    }

That's all. As soon as Cirno is destroyed all her bullets will be as well.

#### It also works with multiple reference holders which is neat

    class TrinexxLeftHead
    {
        void ShootFlames()
        {
            GameObject flames = Instantiate(flamePrefab) as GameObject;
            this.GetComponent<HoldsReferences>().Add(flames);
            leftHead.GetComponent<HoldsReferences>().Add(flames);
            middleHead.GetComponent<HoldsReferences>().Add(flames);
        }
    }

And that's also all. As soon as the last of the heads is destroyed all the flames will magically go away.

In Progress
-----------

 - There is basically a 100% chance that I'll add either waitForCollected to
   IsGarbageCollectable or just a spawnOnCollected field

 - If I switch this to reference counting with check for `references == 0` on
   the HoldsReferences' `OnDestroy` it would run ultra-quick and be way cool.
