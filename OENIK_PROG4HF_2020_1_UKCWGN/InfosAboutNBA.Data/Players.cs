//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfosAboutNba.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Players
    {
        public int idPlayers { get; set; }
        public string PName { get; set; }
        public int Age { get; set; }
        public Nullable<int> NumberOfPlayedSeason { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }
        public Nullable<int> PWeight { get; set; }
        public Nullable<int> NumberOfChampionships { get; set; }
        public Nullable<int> LifetimePoints { get; set; }
        public Nullable<int> PointsInSeason { get; set; }
        public Nullable<int> PValue { get; set; }
        public int idTeams { get; set; }
    
        public virtual Teams Teams { get; set; }
    }
}