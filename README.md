MarkAndSweep
============

A bit of automatic resource management.

I switched it to reference counting within a day of completion so the name is now a misnomer.

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

#### Don't be afraid to use it for something other than bullets

    class WalksTowardsTarget
    {
        def SetTarget(CanBePathedTo newTarget)
        {
            GetComponent<HoldsReferences>().Add(target);
            
            WalkTo(target).Then(() => {
                GetComponent<HoldsReferences>().Remove(target);
            });
        }
    }

I use something like this in production to clean up unused shared pathing nodes and it works pretty terrifically
