﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.ORM
{
    public class FieldValue
    {
        internal FieldValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Value);
        } 
    }
}
