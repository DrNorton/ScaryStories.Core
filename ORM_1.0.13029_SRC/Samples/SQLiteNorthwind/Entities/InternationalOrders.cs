//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5456
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNETCF.ORM
{
    using System;
    using OpenNETCF.ORM;
    
    
    [Entity(KeyScheme.None)]
    public class InternationalOrders
    {
        
        private static InternationalOrders ORM_CreateProxy(OpenNETCF.ORM.FieldAttributeCollection fields, System.Data.IDataReader results)
        {
            var item = new InternationalOrders();
foreach(var field in fields){
            var value = results[field.Ordinal];
switch(field.FieldName){
case "OrderID":
            // If this is a TimeSpan, use the commented line below
            // item.OrderID = (value == DBNull.Value) ? TimeSpan.MinValue; : new TimeSpan((long)value);
            item.OrderID = (value == DBNull.Value) ? 0 : (long)value;
            break;
case "CustomsDescription":
            item.CustomsDescription = (value == DBNull.Value) ? null : (string)value;
            break;
case "ExciseTax":
            item.ExciseTax = (value == DBNull.Value) ? 0 : (decimal)value;
            break;
}
}
            return item;
        }
        
        private long m_orderid;
        
        private string m_customsdescription;
        
        private decimal m_excisetax;
        
        private Orders m_refOrders;
        
        [Field(IsPrimaryKey=true)]
        public long OrderID
        {
            get
            {
                return this.m_orderid;
            }
            set
            {
                this.m_orderid = value;
            }
        }
        
        [Field()]
        public string CustomsDescription
        {
            get
            {
                return this.m_customsdescription;
            }
            set
            {
                this.m_customsdescription = value;
            }
        }
        
        [Field()]
        public decimal ExciseTax
        {
            get
            {
                return this.m_excisetax;
            }
            set
            {
                this.m_excisetax = value;
            }
        }
        
        [Reference(typeof(Orders), "OrderID", ReferenceType=ReferenceType.ManyToOne)]
        public Orders Order
        {
            get
            {
                return this.m_refOrders;
            }
            set
            {
                this.m_refOrders = value;
            }
        }
    }
}
