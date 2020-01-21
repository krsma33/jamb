using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeField : BaseField
{
    protected override bool IsColumnSpecificConditionMet() => true;
}
