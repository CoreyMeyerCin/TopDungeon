using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public interface IValue<T> : IEqualityComparer<IValue<T>>,/*this allows us to check the collection to see if the table is fulfilled*/ IEquatable<IValue<T>>//this allows us to use IEquatable that allows us to use dictionaries with comparitable keys
    {
        T Value { get;}
    }
}
