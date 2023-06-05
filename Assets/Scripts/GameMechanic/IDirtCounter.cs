using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirtCounter
{
    const int MaxDirtValue = 131072;
    float GetDirtAmout();
}
