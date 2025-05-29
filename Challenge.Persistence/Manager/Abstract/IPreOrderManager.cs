using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Abstract
{
    public interface IPreOrderManager
    {
        void Add(PreOrderDTO preOrderDTO);
    }
}
