using System.Reflection;
using Core.Entities;
using Core.Entities.Concrete;

namespace Core.Utilities.EntityMap;

public static class EntityMapper<X, Z>
        where X : class, IBaseEntity, new()
        where Z : class, IBaseEntity, new()
    {
        public static List<X> Map(List<Z> entities)
        {
            PropertyInfo[] propertiesX = typeof(X).GetProperties();
            PropertyInfo[] propertiesZ = typeof(Z).GetProperties();

            var list = new List<X>();

            foreach (var entity in entities)
            {
                var entityX = new X();
            
                foreach (var propertyX in propertiesX)
                {
                    foreach (var propertyZ in propertiesZ)
                    {
                        if (propertyX.Name == propertyZ.Name)
                        {
                            typeof(X).GetProperty(propertyX.Name)?.SetValue(entityX, typeof(Z).GetProperty(propertyZ.Name)?.GetValue(entity));
                        }
                    }
                
                }
                list.Add(entityX);
            }

            return list;
        }

        public static List<X> Map(List<Z> entities,params string[] properties)
        {
            PropertyInfo[] propertiesX = typeof(X).GetProperties();
            PropertyInfo[] propertiesZ = typeof(Z).GetProperties();

            var list = new List<X>();
            
            foreach (var entity in entities)
            {
                var entityX = new X();
            
                foreach (var propertyX in propertiesX)
                {
                    foreach (var propertyZ in propertiesZ)
                    {
                        if (propertyX.Name == propertyZ.Name)
                        {
                            if (propertyX.PropertyType == propertyZ.PropertyType)
                            {
                                typeof(X).GetProperty(propertyX.Name)?.SetValue(entityX, typeof(Z).GetProperty(propertyZ.Name)?.GetValue(entity));
                            }

                            if (propertyX.PropertyType != propertyZ.PropertyType)
                            {
                                if (propertyX.PropertyType == typeof(string))
                                {
                                    typeof(X).GetProperty(propertyX.Name)?.SetValue(entityX, typeof(Z).GetProperty(propertyZ.Name)?.GetValue(entity)?.ToString());
                                }
                            }
                        }

                        for (int i = 0; i < properties.Length; i=i+2)
                        {
                            if (properties[i] == propertyX.Name && properties[i+1] == propertyZ.Name)
                            {
                                typeof(X).GetProperty(properties[i])?.SetValue(entityX, typeof(Z).GetProperty(properties[i+1])?.GetValue(entity));
                            }
                        }
                    }
                
                }
                list.Add(entityX);
            }

            return list;
        }
    }
    