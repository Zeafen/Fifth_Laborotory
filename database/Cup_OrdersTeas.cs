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
    
    public partial class Cup_OrdersTeas
    {
        public int ID_CupOrderTeas { get; set; }
        public int Order_ID { get; set; }
        public int TeaSort_ID { get; set; }
        public int Cup_ID { get; set; }
        public int CupOrderTeas_Count { get; set; }
    
        public virtual Cups Cups { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual TeaSorts TeaSorts { get; set; }
    }
}
