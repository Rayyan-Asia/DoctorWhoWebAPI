﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorWho.Db
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set;}
        public int DoctorNumber { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime FirstEpisodeDate { get; set; }
        public DateTime? LastEpisodeDate { get; set;}

        public List<Episode> Episodes { get; set;}

        public Doctor()
        {
            Episodes = new List<Episode>();
        }

    }
}