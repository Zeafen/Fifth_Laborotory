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
    
    public partial class Collections
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collections()
        {
            this.Collection_OrdersTeas = new HashSet<Collection_OrdersTeas>();
            this.CollectionsStorage = new HashSet<CollectionsStorage>();
            this.CollectionTeas = new HashSet<CollectionTeas>();
        }
    
        public int ID_Collection { get; set; }
        public string Collection_Name { get; set; }
        public string Collection_Description { get; set; }
        public decimal Collection_Price { get; set; }
        public int CollectionForm_ID { get; set; }
        public Nullable<int> Employee_ID { get; set; }
        public string Collection_ImageUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection_OrdersTeas> Collection_OrdersTeas { get; set; }
        public virtual CollectionForms CollectionForms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionsStorage> CollectionsStorage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionTeas> CollectionTeas { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
