//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLDRL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tieuchi
    {
        public Tieuchi()
        {
            this.Danhgias = new HashSet<Danhgia>();
        }
    
        public int ID { get; set; }
        public Nullable<int> Noidung { get; set; }
        public Nullable<int> Sodiem { get; set; }
    
        public virtual ICollection<Danhgia> Danhgias { get; set; }
    }
}
