﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpField : BaseField
{
    private bool isPreviousFieldFilled;

    protected override bool IsColumnSpecificConditionMet()
    {
        return isPreviousFieldFilled;
    }
}