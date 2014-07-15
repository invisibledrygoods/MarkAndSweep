using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IsGarbageCollectable : MonoBehaviour
{
    int references = 0;

    public void AddReference()
    {
        references++;
        Debug.Log("adding: now " + this + " has " + references + " references");
    }

    public void RemoveReference()
    {
        references--;
    }

    public void Update()
    {
        if (references <= 0)
        {
            Debug.Log("destroying");
            Destroy(gameObject);
        }
    }
}
