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
    
    public partial class TeaTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeaTypes()
        {
            this.TeaSorts = new HashSet<TeaSorts>();
        }
    
        public int ID_TeaType { get; set; }
        public string TeaType_Name { get; set; }
        public string TeaType_Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeaSorts> TeaSorts { get; set; }
    }
}
