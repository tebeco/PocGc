using Runtime;
using System;

namespace Gc.Core.Tests
{
    public class ProxyReferenceType : ReferenceType
    {
        public new void AddField(string fieldName, ReferenceType initialValue)
        {
            base.AddField(fieldName, initialValue);
        }

        public new void AddField(string fieldName, ValueType initialValue)
        {
            base.AddField(fieldName, initialValue);
        }

        public new void SetField(string fieldName, ReferenceType newValue)
        {
            base.SetField(fieldName, newValue);
        }

        public new void SetField(string fieldName, ValueType newValue)
        {
            base.SetField(fieldName, newValue);
        }

        public new ReferenceType GetReference(string fieldName)
        {
            return base.GetReference(fieldName);
        }

        public new ValueType GetValue(string fieldName)
        {
            return base.GetValue(fieldName);
        }
    }
}
