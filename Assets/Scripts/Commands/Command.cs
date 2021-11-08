using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    void Execute();

    void ExecuteDown();

    void Reset();
}