﻿using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.PreOrderOperations
{
    public interface IPreOrderOperations
    {
        void Add(PreOrderDTO preOrderDTO);
    }
}
