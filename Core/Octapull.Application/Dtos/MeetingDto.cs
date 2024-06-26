﻿using Microsoft.AspNetCore.Http;

namespace Octapull.Application.Dtos
{
    public class MeetingDto
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
    }
}
