//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Practice.database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollectionsStorage
    {
        public int ID_CollectionStorage { get; set; }
        public int Collection_ID { get; set; }
        public int CollectionStorage_Count { get; set; }
    
        public virtual Collections Collections { get; set; }
    }
}
