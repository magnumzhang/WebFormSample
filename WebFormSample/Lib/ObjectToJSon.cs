using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Reflection;

namespace WebFormSample.Lib
{
    public class ObjectToJSon
    {
        public static string GenerateJSon(object classObject)
        {
            Type ObjectType = classObject.GetType();
            PropertyInfo[] PropertyList = ObjectType.GetProperties();

            JSonStringBuilder JSonBuilder = new JSonStringBuilder();
            JSonBuilder.Begin();

            foreach (PropertyInfo property in PropertyList)
            {
                JSonBuilder.Add(property.Name, property.GetValue(classObject));
            }

            JSonBuilder.End();

            string JSonStr = JSonBuilder.ToString();

            return JSonStr;
        }
    }
}