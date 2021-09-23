using System;
using System.Reflection;
using System.Text.Json.Serialization;
using Anshan.Scaffolder.Extensions;

namespace Anshan.Scaffolder.Models
{
    public class Property
    {
        private Property(string title, Type type)
        {
            Title = new MultiStyleText(title);
            Type = type;
            TypeName = type.Name;
            IsUserDefinedType = type.IsUserDefinedType();

            if (Type.IsEnum || Type == typeof(int))
                TypeName = "int";
            else if (Type == typeof(string)) TypeName = "string";
        }

        public Property(PropertyInfo propertyInfo) : this(propertyInfo.Name, propertyInfo.PropertyType)
        {
        }

        public Property(ParameterInfo parameterInfo) : this(parameterInfo.Name, parameterInfo.ParameterType)
        {
        }

        public MultiStyleText Title { get; }

        [JsonIgnore] public Type Type { get; }

        [JsonIgnore] public Type InnerType => Type.IsList() ? Type.GetGenericArguments()[0] : Type;

        public MultiStyleText InnerTypeName => InnerType.Name;

        public MultiStyleText TypeName { get; private set; }

        public bool IsUserDefinedType { get; }

        public bool IsList => Type.IsList();

        public void AddPostfix(string postfix)
        {
            if (Type.IsList())
            {
                var listInnerType = Type.GetGenericArguments()[0];

                if (listInnerType.IsUserDefinedType())
                    TypeName = "IEnumerable<" + listInnerType.Name + postfix + ">";
                else
                    TypeName = "IEnumerable<" + listInnerType.Name + ">";
            }
            else if (IsUserDefinedType)
            {
                TypeName = Type.Name + postfix;
            }
        }
    }
}