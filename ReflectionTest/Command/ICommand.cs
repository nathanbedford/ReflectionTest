﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest.Command
{
    public interface ICommand
    {
        string Execute(string extraValue);
    }
}
