﻿using System.Collections.Generic;

public static class Deconstructors
{
    #region Methods

    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
    {
        key     = kvp.Key;
        value   = kvp.Value;
    }

    #endregion
}