//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Osoba
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> VremeKreiranja { get; set; }
        public Nullable<bool> Rezident { get; set; }
        public string IdentifikacioniBroj { get; set; }
        public string Jezik { get; set; }
        public Nullable<long> IdentifikacioniDokumentId { get; set; }
    }
}
