
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRepository.Helpers
{
    internal static class RepositoryHelper
    {
        public static void DetachLocal<T>(this DbContext context, Func<T, bool> predicate)
            where T : class, new()
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(predicate);

            if (local != null)
                context.Entry(local).State = EntityState.Detached;
        }

        public static void DetachList<T>(this DbContext context, Func<T, bool> predicate)
            where T : class, new()
        {
            var list = context.Set<T>()
                .Local
                .Where(predicate)
                .ToList();

            list.ForEach(item => { context.Entry(item).State = EntityState.Detached; });
        }
    }
}
