using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Runtime
{
    public abstract class ReferenceType
    {
        private readonly Dictionary<string, ReferenceType> _referenceFields = new Dictionary<string, ReferenceType>();
        private readonly Dictionary<string, ValueType> _valueFields = new Dictionary<string, ValueType>();

        protected void AddField(string fieldName, ReferenceType initialValue)
        {
            _referenceFields.Add(fieldName, initialValue);
        }

        protected void AddField(string fieldName, ValueType initialValue)
        {
            _valueFields.Add(fieldName, initialValue);
        }

        protected void SetField(string fieldName, ReferenceType newValue)
        {
            if (!_referenceFields.ContainsKey(fieldName))
            {
                throw new KeyNotFoundException($"The field {fieldName} does not exist.");
            }

            _referenceFields[fieldName] = newValue;
        }

        protected void SetField(string fieldName, ValueType newValue)
        {
            if (!_valueFields.ContainsKey(fieldName))
            {
                throw new KeyNotFoundException($"The field {fieldName} does not exist.");
            }

            var oldType = _valueFields[fieldName].GetType();
            var newType = newValue.GetType();

            if(oldType != newType)
            {
                throw new ArgumentException($"the new value for field {fieldName} does not have the same type as the previous value.");
            }

            _valueFields[fieldName] = newValue;
        }

        protected ReferenceType GetReference(string fieldName)
        {
            return _referenceFields[fieldName];
        }

        protected ValueType GetValue(string fieldName)
        {
            return _valueFields[fieldName];
        }
    }
}
