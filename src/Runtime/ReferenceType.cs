using System;
using System.Collections.Generic;
using System.Linq;

namespace Runtime
{
    public abstract class ReferenceType
    {
        private readonly Dictionary<string, ReferenceType> _referenceFields = new Dictionary<string, ReferenceType>();
        private readonly Dictionary<string, ValueType> _valueFields = new Dictionary<string, ValueType>();

        public ReferenceType()
        {
        }

        public ReferenceType(Dictionary<string, ReferenceType> referenceFields)
        {
            referenceFields.ToList().ForEach(field => AddField(field.Key, field.Value));
        }

        public ReferenceType(Dictionary<string, ValueType> valueFields)
        {
            valueFields.ToList().ForEach(field => AddField(field.Key, field.Value));
        }

        public ReferenceType(Dictionary<string, ReferenceType> referenceFields, Dictionary<string, ValueType> valueFields)
        {
            referenceFields.ToList().ForEach(field => AddField(field.Key, field.Value));
            valueFields.ToList().ForEach(field => AddField(field.Key, field.Value));
        }

        private void AddField(string fieldName, ReferenceType initialValue)
        {
            if (_valueFields.ContainsKey(fieldName))
            {
                throw new ArgumentException($"The field {fieldName} already exists.");
            }

            _referenceFields.Add(fieldName, initialValue);
        }

        private void AddField(string fieldName, ValueType initialValue)
        {
            if(_referenceFields.ContainsKey(fieldName))
            {
                throw new ArgumentException($"The field {fieldName} already exists.");
            }

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

            if (oldType != newType)
            {
                throw new ArgumentException($"the new value for field {fieldName} does not have the same type as the previous value.");
            }

            _valueFields[fieldName] = newValue;
        }

        protected ReferenceType GetReference(string fieldName)
        {
            return _referenceFields[fieldName];
        }

        public ValueType GetValue(string fieldName)
        {
            return _valueFields[fieldName];
        }
    }
}
