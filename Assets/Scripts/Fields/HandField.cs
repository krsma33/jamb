using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandField : BaseField
{
    protected override bool IsColumnSpecificConditionMet() => roll == 1 ? true : false;
}
