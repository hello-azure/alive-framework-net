using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Alive.Foundation.Data;

namespace TestConsoleApp
{
    public class ConditionBuilder<T> : ExpressionVisitor
        where T : BusinessObject
    {

    }
}
