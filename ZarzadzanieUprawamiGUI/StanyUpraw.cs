//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZarzadzanieUprawamiGUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class StanyUpraw
    {
        public int idUprawaStan { get; set; }
        public Nullable<int> idUprawa { get; set; }
        public Nullable<int> idStan { get; set; }
    
        public virtual StanyPola StanyPola { get; set; }
        public virtual Uprawy Uprawy { get; set; }
    }
}
