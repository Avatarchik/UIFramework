using System;
using UnityEngine;

namespace VBM {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class PropertyToEnumDrawerAttribute : PropertyAttribute {
    }
}