using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;

namespace WebFormSample.Lib
{
    public class JSonStringBuilder
    {
        private StringBuilder builder;

        public JSonStringBuilder()
        {
            builder = new StringBuilder();
        }

        public void Begin()
        {
            builder.Append("{");
        }

        public void End()
        {
            builder.Remove(builder.Length - 1, 1);
            builder.Append("}");
        }

        public void Add(string key, object data)
        {
            if (data.GetType() == typeof(String))
            {
                builder.AppendFormat("\"{0}\":\"{1}\",", key, data);
            }
            else
            {
                builder.AppendFormat("\"{0}\":{1},", key, data);
            }
        }

        public void Add(string josnStr)
        {
            builder.Append(josnStr+",");
        }

        public override string ToString()
        {
            string str = builder.ToString();
            return str;
        }
    }
}