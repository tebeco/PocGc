using NFluent;
using System;
using System.Collections.Generic;
using Xunit;

namespace Gc.Core.Tests
{
    public class ReferenceTypeTests
    {
        [Fact]
        public void Should_not_be_able_to_add_twice_same_instance_reference_field()
        {
            //Given
            var refType = new ProxyReferenceType();
            var initialValue = new ProxyReferenceType();

            //When
            refType.AddField("dummyField", initialValue);

            //Then
            Check.ThatCode(() => refType.AddField("dummyField", initialValue)).Throws<ArgumentException>();
        }

        [Fact]
        public void Should_not_be_able_to_add_twice_same_instance_value_field()
        {
            //Given
            var refType = new ProxyReferenceType();
            var initialValue = 1;

            //When
            refType.AddField("dummyField", initialValue);

            //Then
            Check.ThatCode(() => refType.AddField("dummyField", initialValue)).Throws<ArgumentException>();
        }

        [Fact]
        public void Should_be_able_to_add_instance_reference_field_with_initial_value()
        {
            //Given
            var refType = new ProxyReferenceType();
            var initialValue = new ProxyReferenceType();

            //When Then
            Check.ThatCode(() => refType.AddField("dummyField", initialValue)).DoesNotThrow();
        }

        [Fact]
        public void Should_be_able_to_add_instance_value_field_with_initial_value()
        {
            //Given
            var refType = new ProxyReferenceType();
            var initialValue = false;

            //When Then
            Check.ThatCode(() => refType.AddField("dummyField", initialValue)).DoesNotThrow();
        }

        [Fact]
        public void Should_be_able_to_retreive_reference_field_value()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            var fieldValue = new ProxyReferenceType();

            //When
            refType.AddField(dummyFieldName, fieldValue);

            //Then
            var retreivedValue = refType.GetReference(dummyFieldName);
            Check.That(fieldValue).IsSameReferenceAs(retreivedValue);
        }

        [Fact]
        public void Should_be_able_to_retreive_value_field_value()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            byte fieldValue = 255;

            //When
            refType.AddField(dummyFieldName, fieldValue);

            //Then
            var retreivedValue = refType.GetValue(dummyFieldName);
            Check.That(fieldValue).IsEqualTo(255);
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
        public void Should_be_able_to_retreive_reference_field_value_after_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            var initialValue = new ProxyReferenceType();

            refType.AddField(dummyFieldName, initialValue);
            var fieldValue = new ProxyReferenceType();

            //When
            refType.SetField(dummyFieldName, fieldValue);

            //Then
            var retreivedValue = refType.GetReference(dummyFieldName);
            Check.That(retreivedValue).IsSameReferenceAs(fieldValue);
        }

        [Fact]
        public void Should_be_able_to_retreive_value_field_value_after_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            var initialValue = (false, 3);

            //When
            refType.AddField(dummyFieldName, initialValue);

            //Then
            var retreivedValue = refType.GetValue(dummyFieldName);
            Check.That(retreivedValue).IsEqualTo((false, 3));
        }

        [Fact]
        public void Should_be_able_to_retreive_last_reference_field_value_after_multiple_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            var initialValue = new ProxyReferenceType();
            refType.AddField(dummyFieldName, initialValue);
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
        public void Should_be_able_to_retreive_last_value_field_value_after_multiple_set()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            refType.AddField(dummyFieldName, (false, 1));

            //When
            refType.SetField(dummyFieldName, (true, 3));

            //Then
            var retreivedValue = refType.GetValue(dummyFieldName);
            Check.That(retreivedValue).IsEqualTo((true, 3));
        }

        [Fact]
        public void Should_not_be_able_to_set_value_field_with_other_type()
        {
            //Given
            const string dummyFieldName = "dummyField";
            var refType = new ProxyReferenceType();
            refType.AddField(dummyFieldName, (false, 1));

            //When Then
            Check.ThatCode(() => refType.SetField(dummyFieldName, (sbyte)-3)).Throws<ArgumentException>();
        }
    }
}
