using NFluent;
using Runtime;
using System;
using System.Collections.Generic;
using Xunit;

namespace Runtime.Tests
{
    public class ReferenceTypeTests
    {
        [Fact]
        public void Should_be_able_to_add_instance_reference_field_with_initial_value()
        {
            //Given
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {"dummyField", new ProxyReferenceType()},
            };

            //When
            //Then
            Check.ThatCode(() => new ProxyReferenceType(referenceFields)).DoesNotThrow();
        }

        [Fact]
        public void Should_be_able_to_add_instance_value_field_with_initial_value()
        {
            //Given
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {"dummyField", false},
            };

            //When
            //Then
            Check.ThatCode(() => new ProxyReferenceType(valueFields)).DoesNotThrow();
        }

        [Fact]
        public void Should_be_able_to_add_both_ref_and_value_field_with_initial_value()
        {
            //Given
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {"dummyField", new ProxyReferenceType()},
            };
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {"value", false},
            };

            //When
            //Then
            Check.ThatCode(() => new ProxyReferenceType(referenceFields, valueFields)).DoesNotThrow();
        }

        [Fact]
        public void Should_not_be_able_to_add_both_ref_and_value_field_with_same_name_from_auto_ctor()
        {
            //Given
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {"dummyField", new ProxyReferenceType()},
            };
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {"dummyField", false},
            };

            //When
            //Then
            Check.ThatCode(() => new ProxyReferenceType(referenceFields, valueFields)).Throws<ArgumentException>();
        }

        [Fact]
        public void Should_be_able_to_retreive_reference_field()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var fieldValue = new ProxyReferenceType();
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {dummyFieldName , fieldValue},
            };

            //When
            var refType = new ProxyReferenceType(referenceFields);

            //Then
            var retreivedValue = refType.GetReference(dummyFieldName);
            Check.That(retreivedValue).IsSameReferenceAs(fieldValue);
        }

        [Fact]
        public void Should_be_able_to_retreive_value_field()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var fieldValue = 255;
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {dummyFieldName , fieldValue},
            };

            //When
            var refType = new ProxyReferenceType(valueFields);

            //Then
            var retreivedValue = refType.GetValue(dummyFieldName);
            Check.That(retreivedValue).IsEqualTo(fieldValue);
        }

        [Fact]
        public void Should_not_be_able_to_set_non_existing_reference_field()
        {
            //Given
            var refType = new ProxyReferenceType();
            var fieldValue = new ProxyReferenceType();

            //When Then
            Check.ThatCode(() => refType.SetField("not existing field", fieldValue)).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void Should_not_be_able_to_set_non_existing_value_field()
        {
            //Given
            var refType = new ProxyReferenceType();
            var fieldValue = (1, 2, 3);

            //When Then
            Check.ThatCode(() => refType.SetField("not existing field", fieldValue)).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void Should_not_be_able_to_get_non_existing_reference_field()
        {
            //Given
            var refType = new ProxyReferenceType();

            //When Then
            Check.ThatCode(() => _ = refType.GetReference("not existing field")).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void Should_not_be_able_to_get_non_existing_value_field()
        {
            //Given
            var refType = new ProxyReferenceType();

            //When Then
            Check.ThatCode(() => _ = refType.GetValue("not existing field")).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void Should_be_able_to_retreive_reference_field_after_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var fieldValue = new ProxyReferenceType();
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {dummyFieldName , fieldValue},
            };
            var refType = new ProxyReferenceType(referenceFields);

            //When
            refType.SetField(dummyFieldName, fieldValue);
            var retreivedValue = refType.GetReference(dummyFieldName);

            //Then
            Check.That(retreivedValue).IsSameReferenceAs(fieldValue);
        }

        [Fact]
        public void Should_be_able_to_retreive_value_field_after_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var initialValue = (true, 76876786);
            var fieldValue = (false, 3);
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {dummyFieldName , initialValue},
            };
            var refType = new ProxyReferenceType(valueFields);

            //When
            refType.SetField(dummyFieldName, fieldValue);
            var retreivedValue = refType.GetValue(dummyFieldName);

            //Then
            Check.That(retreivedValue).IsEqualTo((false, 3));
        }

        [Fact]
        public void Should_be_able_to_retreive_last_reference_field_after_multiple_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var initialValue = new ProxyReferenceType();
            Dictionary<string, ReferenceType> referenceFields = new Dictionary<string, ReferenceType>
            {
                {dummyFieldName , initialValue},
            };
            var refType = new ProxyReferenceType(referenceFields);
            var fieldValue = new ProxyReferenceType();
            var fieldValue2 = new ProxyReferenceType();
            var fieldValue3 = new ProxyReferenceType();

            //When
            refType.SetField(dummyFieldName, fieldValue);
            refType.SetField(dummyFieldName, fieldValue2);
            refType.SetField(dummyFieldName, fieldValue3);

            //Then
            var retreivedValue = refType.GetReference(dummyFieldName);
            Check.That(retreivedValue).Not.IsSameReferenceAs(initialValue);
            Check.That(retreivedValue).Not.IsSameReferenceAs(fieldValue);
            Check.That(retreivedValue).Not.IsSameReferenceAs(fieldValue2);
            Check.That(retreivedValue).IsSameReferenceAs(fieldValue3);
        }

        [Fact]
        public void Should_be_able_to_retreive_last_value_field_after_multiple_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var initialValue = (false, 0);
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {dummyFieldName , initialValue},
            };
            var refType = new ProxyReferenceType(valueFields);

            //When
            refType.SetField(dummyFieldName, (true, 3));
            refType.SetField(dummyFieldName, (false, 3));
            refType.SetField(dummyFieldName, (false, 5));

            //Then
            var retreivedValue = refType.GetValue(dummyFieldName);
            Check.That(retreivedValue).IsEqualTo((false, 5));
        }

        [Fact]
        public void Should_not_be_able_to_set_value_field_with_other_type()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var initialValue = (false, 0);
            Dictionary<string, ValueType> valueFields = new Dictionary<string, ValueType>
            {
                {dummyFieldName , initialValue},
            };
            var refType = new ProxyReferenceType(valueFields);

            //When Then
            Check.ThatCode(() => refType.SetField(dummyFieldName, (sbyte)-3)).Throws<ArgumentException>();
        }
    }
}
