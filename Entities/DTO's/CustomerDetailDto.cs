﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO_s
{
    public class CustomerDetailDto : IDto
    {
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int FindexScore { get; set; }
    }
}
