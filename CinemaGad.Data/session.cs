//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaGad.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class session
    {
        public int session_id { get; set; }
        public int film_id { get; set; }
        public int cinema_id { get; set; }
        public System.DateTime start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
    
        public virtual cinema cinema { get; set; }
        public virtual film film { get; set; }
    }
}
