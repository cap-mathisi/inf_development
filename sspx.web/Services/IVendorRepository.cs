﻿using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IVendorRepository
    {
        List<Vendor> List();
    }
}
