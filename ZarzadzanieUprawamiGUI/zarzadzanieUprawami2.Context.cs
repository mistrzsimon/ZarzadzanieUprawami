﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class zarzadzanieUprawami2Entities1 : DbContext
    {
        public zarzadzanieUprawami2Entities1()
            : base("name=zarzadzanieUprawami2Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gatunki> Gatunki { get; set; }
        public virtual DbSet<Gleby> Gleby { get; set; }
        public virtual DbSet<Magazyn> Magazyn { get; set; }
        public virtual DbSet<Obszary> Obszary { get; set; }
        public virtual DbSet<Odmiany> Odmiany { get; set; }
        public virtual DbSet<Okresowosci> Okresowosci { get; set; }
        public virtual DbSet<Pola> Pola { get; set; }
        public virtual DbSet<PracePolowe> PracePolowe { get; set; }
        public virtual DbSet<PraceZasoby> PraceZasoby { get; set; }
        public virtual DbSet<Pracownicy> Pracownicy { get; set; }
        public virtual DbSet<Producenci> Producenci { get; set; }
        public virtual DbSet<RodzajePrac> RodzajePrac { get; set; }
        public virtual DbSet<Rosliny> Rosliny { get; set; }
        public virtual DbSet<StanyPola> StanyPola { get; set; }
        public virtual DbSet<StanyUpraw> StanyUpraw { get; set; }
        public virtual DbSet<Uprawy> Uprawy { get; set; }
        public virtual DbSet<Zasób> Zasób { get; set; }
    }
}