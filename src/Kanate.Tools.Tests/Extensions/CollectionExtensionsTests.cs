using Kanate.Tools.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Kanate.Tools.Tests.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region IsEmptyTests

        #region Array

        [Test]
        public void OneElement_Array_Instance_Returns_True()
        {
            var collection = new object[1]
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty());
        }

        [Test]
        public void Empty_Array_Instance_Returns_True()
        {
            var collection = new object[0];

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_Array_Instance_Returns_True()
        {
            var collection = default(object[]);

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_Array_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = default(object[]);

            Assert.IsTrue(collection.IsEmpty<object, object[]>());
        }

        [Test]
        public void Empty_Array_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = new object[0];

            Assert.IsTrue(collection.IsEmpty<object, object[]>());
        }

        [Test]
        public void OneElement_Array_Instance_Returns_True_Using_Generic()
        {
            var collection = new object[1]
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty<object, object[]>());
        }

        #endregion

        #region List

        [Test]
        public void OneElement_List_Instance_Returns_True()
        {
            var collection = new List<object>
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty());
        }

        [Test]
        public void Empty_List_Instance_Returns_True()
        {
            var collection = new List<object>();

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_List_Instance_Returns_True()
        {
            var collection = default(List<object>);

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_List_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = default(List<object>);

            Assert.IsTrue(collection.IsEmpty<object, List<object>>());
        }

        [Test]
        public void Empty_List_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = new List<object>();

            Assert.IsTrue(collection.IsEmpty<object, List<object>>());
        }

        [Test]
        public void OneElement_List_Instance_Returns_True_Using_Generic()
        {
            var collection = new List<object>
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty<object, List<object>>());
        }

        #endregion

        #region HashSet

        [Test]
        public void OneElement_HashSet_Instance_Returns_True()
        {
            var collection = new HashSet<object>
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty());
        }

        [Test]
        public void Empty_HashSet_Instance_Returns_True()
        {
            var collection = new HashSet<object>();

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_HashSet_Instance_Returns_True()
        {
            var collection = default(HashSet<object>);

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_HashSet_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = default(HashSet<object>);

            Assert.IsTrue(collection.IsEmpty<object, HashSet<object>>());
        }

        [Test]
        public void Empty_HashSet_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = new HashSet<object>();

            Assert.IsTrue(collection.IsEmpty<object, HashSet<object>>());
        }

        [Test]
        public void OneElement_HashSet_Instance_Returns_True_Using_Generic()
        {
            var collection = new HashSet<object>
            {
                new object()
            };

            Assert.IsFalse(collection.IsEmpty<object, HashSet<object>>());
        }

        #endregion

        #region HashSet

        [Test]
        public void OneElement_Dictionary_Instance_Returns_True()
        {
            var collection = new Dictionary<object, object>
            {
                { new object(), new object() }
            };

            Assert.IsFalse(collection.IsEmpty());
        }

        [Test]
        public void Empty_Dictionary_Instance_Returns_True()
        {
            var collection = new Dictionary<object, object>();

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_Dictionary_Instance_Returns_True()
        {
            var collection = default(Dictionary<object, object>);

            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void Default_Dictionary_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = default(Dictionary<object, object>);

            Assert.IsTrue(collection.IsEmpty<KeyValuePair<object, object>, Dictionary<object, object>>());
        }

        [Test]
        public void Empty_Dictionary_Instance_Returns_True_Using_Generic_Method()
        {
            var collection = new Dictionary<object, object>();

            Assert.IsTrue(collection.IsEmpty<KeyValuePair<object, object>, Dictionary<object, object>>());
        }

        [Test]
        public void OneElement_Dictionary_Instance_Returns_True_Using_Generic()
        {
            var collection = new Dictionary<object, object>
            {
                { new object(), new object() }
            };

            Assert.IsFalse(collection.IsEmpty<KeyValuePair<object, object>, Dictionary<object, object>>());
        }

        #endregion

        #endregion

        #region UpsertTests

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        public void Upsert_Does_Nothing_For_Undefined_Dictionary(string key)
        {
            Dictionary<string, List<string>> dictionary = null;

            Assert.DoesNotThrow(() => dictionary.Upsert(key, "abc"));
            Assert.IsNull(dictionary);
        }

        [Test]
        public void Upsert_Does_Nothing_For_Undefined_Key()
        {
            var dictionary = new Dictionary<string, List<string>>();

            Assert.DoesNotThrow(() => dictionary.Upsert(null, "abc"));
            Assert.IsNotNull(dictionary);
            Assert.AreEqual(dictionary.Count, 0);
        }

        [Test]
        public void Upsert_Inserts_New_List_With_Given_Value_When_Key_Doesnt_Exist()
        {
            var dictionary = new Dictionary<string, List<string>>();

            Assert.DoesNotThrow(() => dictionary.Upsert("abc", "value"));
            Assert.IsNotNull(dictionary);
            Assert.AreEqual(dictionary.Count, 1);
            Assert.AreEqual(dictionary.Keys.Count, 1);
            Assert.AreEqual(dictionary.Keys.First(), "abc");
            Assert.AreEqual(dictionary["abc"].Count, 1);
            Assert.AreEqual(dictionary["abc"][0], "value");
        }

        [Test]
        public void Upsert_Updates_List_With_Given_Value_When_Key_Exist()
        {
            var dictionary = new Dictionary<string, List<string>>
            {
                {"abc", new List<string> { "value0" }  }
            };

            Assert.DoesNotThrow(() => dictionary.Upsert("abc", "value1"));
            Assert.IsNotNull(dictionary);
            Assert.AreEqual(dictionary.Count, 1);
            Assert.AreEqual(dictionary.Keys.Count, 1);
            Assert.AreEqual(dictionary.Keys.First(), "abc");
            Assert.AreEqual(dictionary["abc"].Count, 2);
            Assert.AreEqual(dictionary["abc"][0], "value0");
            Assert.AreEqual(dictionary["abc"][1], "value1");
        }

        #endregion
    }
}
