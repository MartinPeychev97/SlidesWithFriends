﻿using BAL.Models.Presentation;

namespace BAL.Models.Event
{
    public class EventStartViewModel
    {
        public string Username { get; set; }

        public string QRCode { get; set; }

        public PresentationEventViewModel Presentation { get; set; }
    }
}