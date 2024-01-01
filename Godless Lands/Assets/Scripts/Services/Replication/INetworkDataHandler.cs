﻿using Protocol.Data.Replicated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Replication
{
    public interface INetworkDataHandler
    {
        void UpdateData(IReplicationData updatedData);
    }
}