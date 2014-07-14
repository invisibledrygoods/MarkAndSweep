using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IsGarbageCollectable : MonoBehaviour
{
    static HashSet<IsGarbageCollectable> garbageCollectables = new HashSet<IsGarbageCollectable>();

    static IsGarbageCollectable master;
    static float collectInterval = 0.25f;
    static float timeout;

    void Update()
    {
        if (master == null)
        {
            master = this;
        }

        if (master != this)
        {
            return;
        }

        timeout -= Time.deltaTime;

        if (timeout < 0.0f)
        {
            timeout = collectInterval;

            List<IsGarbageCollectable> collectables = IsGarbageCollectable.garbageCollectables.ToList();

            foreach (HoldsReferences referenceHolder in HoldsReferences.referenceHolders)
            {
                foreach (IsGarbageCollectable collectable in referenceHolder.references)
                {
                    collectables.Remove(collectable);
                }
            }

            foreach (IsGarbageCollectable collectable in collectables)
            {
                Destroy(collectable.gameObject);
            }
        }
    }

    void OnEnable()
    {
        garbageCollectables.Add(this);
    }

    void OnDisable()
    {
        garbageCollectables.Remove(this);
    }
}
