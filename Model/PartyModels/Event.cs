using System;
using System.Collections.Generic;
using System.Linq;

namespace ITB2203Application.Model
{
    public class Event
    {
        public int Id { get; set; }
        public Guid SpeakerId { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
    }
}
