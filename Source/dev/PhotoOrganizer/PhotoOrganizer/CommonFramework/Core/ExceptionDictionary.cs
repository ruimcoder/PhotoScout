using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFramework.Core.BaseException
{
    class ExceptionDictionary
    {
        enum ExceptionCriticity
        {
            Citical,
            High,
            Normal,
            Low,
            Warning
        };

        enum ExceptionType
        {
            Technical,
            Business,
            Presentation

        }

    }
}
