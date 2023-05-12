using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWhoDomain
{
    public static class EntityMapper
    {
        public static void TransferProperties<T>(T sourceObject, T destinationObject)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite && property.GetValue(sourceObject) != null)
                {
                    object value = property.GetValue(sourceObject);
                    property.SetValue(destinationObject, value);
                }
            }
        }

    }
}
