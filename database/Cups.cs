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
    
    public partial class Cups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cups()
        {
            this.Cup_OrdersTeas = new HashSet<Cup_OrdersTeas>();
        }
    
        public int ID_Cup { get; set; }
        public string Cup_Name { get; set; }
        public int Cup_AmountTea { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cup_OrdersTeas> Cup_OrdersTeas { get; set; }
    }
}
