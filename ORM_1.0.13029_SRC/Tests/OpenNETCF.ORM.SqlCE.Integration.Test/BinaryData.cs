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
    using System.Data;
    
    
    [Entity(KeyScheme.Identity)]
    public class BinaryData
    {
        
        private int m_id;
        
        private byte[] m_binaryfield;
        
        private byte[] m_imagefield;
        
        private string m_ntextfield;
        
        [Field(IsPrimaryKey=true, SearchOrder=FieldSearchOrder.Ascending)]
        public int ID
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
            }
        }
        
        [Field()]
        public byte[] BinaryField
        {
            get
            {
                return this.m_binaryfield;
            }
            set
            {
                this.m_binaryfield = value;
            }
        }
        
        [Field()]
        public byte[] ImageField
        {
            get
            {
                return this.m_imagefield;
            }
            set
            {
                this.m_imagefield = value;
            }
        }
        
        [Field()]
        public string NTextField
        {
            get
            {
                return this.m_ntextfield;
            }
            set
            {
                this.m_ntextfield = value;
            }
        }
    }
}
