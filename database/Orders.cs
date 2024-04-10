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
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.Collection_OrdersTeas = new HashSet<Collection_OrdersTeas>();
            this.Cup_OrdersTeas = new HashSet<Cup_OrdersTeas>();
            this.Pack_OrdersTeas = new HashSet<Pack_OrdersTeas>();
        }
    
        public int ID_Order { get; set; }
        public decimal Order_TotalPrice { get; set; }
        public Nullable<int> Emplyoee_ID { get; set; }
        public System.DateTime Order_Date { get; set; }
        public decimal Order_Paid { get; set; }
        public decimal Order_PayBack { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection_OrdersTeas> Collection_OrdersTeas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cup_OrdersTeas> Cup_OrdersTeas { get; set; }
        public virtual Employees Employees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pack_OrdersTeas> Pack_OrdersTeas { get; set; }
    }
}
