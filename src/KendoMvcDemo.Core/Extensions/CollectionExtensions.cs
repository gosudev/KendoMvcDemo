using System.Collections.Generic;

namespace KendoMvcDemo.Core.Extensions
{
    static class CollectionExtensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }
    }
}
