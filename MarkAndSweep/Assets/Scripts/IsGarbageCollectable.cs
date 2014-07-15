using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IsGarbageCollectable : MonoBehaviour
{
    int references = 0;

    public void AddReference()
    {
        references++;
    }

    public void RemoveReference()
    {
        references--;
    }

    public void Update()
    {
        if (references <= 0)
        {
            Destroy(gameObject);
        }
    }
}
