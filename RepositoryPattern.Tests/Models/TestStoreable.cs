using System;
using System.Collections.Generic;
using System.Text;
using RepositoryPattern.Models;

namespace RepositoryPattern.Tests.Models
{
    public class TestStoreable : Storeable
    {
        public string Message { get; set; }
    }
}
