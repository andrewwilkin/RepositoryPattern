using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Interfaces
{
    public interface IStoreable
    {
        IComparable Id { get; set; }
    }
}
