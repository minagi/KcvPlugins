﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class AdmiralInfoList : ILogsList<AdmiralInfo>
    {
        public DateTime UpdateDate { get; set; }

        public AdmiralInfo[] List { get; set; }

    }
}
