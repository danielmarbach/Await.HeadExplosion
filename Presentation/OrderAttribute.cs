using System;

namespace Presentation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class OrderAttribute : Attribute
    {
        public int Order { get; }

        public OrderAttribute(int order)
        {
            Order = order;
        }
    }
}