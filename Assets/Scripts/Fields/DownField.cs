using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownField : BaseField
{
    protected override bool IsColumnSpecificConditionMet()
    {
        return true;
    }
}
