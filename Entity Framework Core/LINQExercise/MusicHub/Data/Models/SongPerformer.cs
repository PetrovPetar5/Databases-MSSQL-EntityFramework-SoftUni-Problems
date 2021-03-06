﻿namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class SongPerformer
    {
        public int SongId { get; set; }

        [Required]
        public Song Song { get; set; }

        public int PerformerId {get; set;}

        [Required]
        public Performer Performer { get; set; }
    }
}
