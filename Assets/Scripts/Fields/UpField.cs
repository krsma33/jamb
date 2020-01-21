using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpField : BaseField
{
    protected override bool IsColumnSpecificConditionMet()
    {
        return false;
    }
}
