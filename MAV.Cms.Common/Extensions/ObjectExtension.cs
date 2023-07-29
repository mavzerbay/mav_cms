using MAV.Cms.Common.BaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MAV.Cms.Common.Extensions
{
    public static class ObjectExtension
    {
        public static void FillChildMasterId<T>(this T entity) where T : BaseEntity
        {
            var masterName = entity.GetType().Name;
            Guid masterId = Guid.Parse(entity.GetType().GetProperty("Id").GetValue(entity).ToString());

            if (masterId == Guid.Empty)
            {
                masterId = Guid.NewGuid();
                entity.GetType().GetProperty("Id").SetValue(entity, masterId);
            }

            var collectionProperties = entity.GetType().GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToList();

            for (int i = 0; i < collectionProperties.Count; i++)
            {
                var child = collectionProperties[i].GetValue(entity) as IList;
                if (child != null)
                {
                    for (int p = 0; p < child.Count; p++)
                    {
                        child[p].GetType().GetProperty(masterName + "Id").SetValue(child[p], masterId);
                        (child[p] as BaseEntity).FillChildMasterId();
                    }
                }
            }
        }
        public static void SetModelAndChildSoftDelete<T>(this T entity) where T : BaseEntity
        {
            var masterName = entity.GetType().Name;
            entity.GetType().GetProperty("isSoftDelete").SetValue(entity, true);

            var collectionProperties = entity.GetType().GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToList();

            for (int i = 0; i < collectionProperties.Count; i++)
            {
                var child = collectionProperties[i].GetValue(entity) as IList;
                if (child != null)
                {
                    for (int p = 0; p < child.Count; p++)
                    {
                        child[p].GetType().GetProperty("isSoftDelete").SetValue(child[p], true);
                        (child[p] as BaseEntity).SetModelAndChildSoftDelete();
                    }
                }
            }
        }
    }
}
