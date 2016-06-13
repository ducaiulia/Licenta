using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllShare.Services.DTOs;

namespace AllShare.Models
{
    public class SettingsViewModel
    {
        public IList<JobDTO> Jobs { get; set; }
    }
}