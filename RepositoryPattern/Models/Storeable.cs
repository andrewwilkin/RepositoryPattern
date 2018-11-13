using System;
using System.Collections.Generic;
using System.Text;
using RepositoryPattern.Interfaces;

namespace RepositoryPattern.Models
{
    public class Storeable : IStoreable
    {
        public IComparable Id { get; set; }
    }
}
