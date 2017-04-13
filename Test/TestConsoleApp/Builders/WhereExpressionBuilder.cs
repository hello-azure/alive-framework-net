/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TestConsoleApp.Properties;
using Alive.Foundation.Data;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TestConsoleApp.Builders
{
    /// <summary>
    /// Where条件表达式树构建器基类
    /// </summary>
    /// <typeparam name="T">业务数据</typeparam>
    public abstract class WhereExpressionBuilder<T> : ExpressionVisitor, IWhereExpressionBuilder<T>
        where T : BusinessObject
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 
        /// </summary>
        private readonly StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, object> parameterValues = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        private bool startsWith = false;

        /// <summary>
        /// 
        /// </summary>
        private bool endsWith = false;

        /// <summary>
        /// 
        /// </summary>
        private bool contains = false;

        /// <summary>
        /// 
        /// </summary>
        private CallType callType = CallType.NULL;

        #endregion

        #region ==== 构造函数 ====

        public WhereExpressionBuilder()
        {
           
        }

        #endregion

        #region ==== 公共属性 ====

        /// <summary>
        /// Gets a <c>System.String</c> value which represents the AND operation in the WHERE clause.
        /// </summary>
        protected virtual string And
        {
            get { return "AND"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the OR operation in the WHERE clause.
        /// </summary>
        protected virtual string Or
        {
            get { return "OR"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the EQUAL operation in the WHERE clause.
        /// </summary>
        protected virtual string Equal
        {
            get { return "="; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the NOT operation in the WHERE clause.
        /// </summary>
        protected virtual string Not
        {
            get { return "NOT"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the NOT EQUAL operation in the WHERE clause.
        /// </summary>
        protected virtual string NotEqual
        {
            get { return "<>"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the LIKE operation in the WHERE clause.
        /// </summary>
        protected virtual string Like
        {
            get { return "LIKE"; }
        }
        /// <summary>
        /// Gets a <c>System.Char</c> value which represents the place-holder for the wildcard in the LIKE operation.
        /// </summary>
        protected virtual char LikeSymbol
        {
            get { return '%'; }
        }

        #endregion

        #region ==== 重写方法 ====

        #region ==== 重写表达式构建逻辑 ====

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            string str;
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    str = "+";
                    break;
                case ExpressionType.AddChecked:
                    str = "+";
                    break;
                case ExpressionType.AndAlso:
                    str = this.And;
                    break;
                case ExpressionType.Divide:
                    str = "/";
                    break;
                case ExpressionType.Equal:
                    str = this.Equal;
                    break;
                case ExpressionType.GreaterThan:
                    str = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    str = ">=";
                    break;
                case ExpressionType.LessThan:
                    str = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    str = "<=";
                    break;
                case ExpressionType.Modulo:
                    str = "%";
                    break;
                case ExpressionType.Multiply:
                    str = "*";
                    break;
                case ExpressionType.MultiplyChecked:
                    str = "*";
                    break;
                case ExpressionType.Not:
                    str = this.Not;
                    break;
                case ExpressionType.NotEqual:
                    str = this.NotEqual;
                    break;
                case ExpressionType.OrElse:
                    str = this.Or;
                    break;
                case ExpressionType.Subtract:
                    str = "-";
                    break;
                case ExpressionType.SubtractChecked:
                    str = "-";
                    break;
                default:
                    throw new NotSupportedException(node.GetType().Name);
            }

            Out("(");
            Visit(node.Left);
            Out(" ");
            Out(str);
            Out(" ");
            Visit(node.Right);
            Out(")");
            return node;
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(T) ||
                typeof(T).IsSubclassOf(node.Member.DeclaringType))
            {
                if (!string.IsNullOrEmpty(node.Member.Name))
                {
                    Out(string.Format("{0}", node.Member.Name));
                }
            }
            else if (node.Member is PropertyInfo)
            {
                Out(string.Format("{0}", (node.Expression as MemberExpression).Member.Name));
            }
            else if (node.Member is FieldInfo)
            {
                ConstantExpression ce = node.Expression as ConstantExpression;
                FieldInfo fi = node.Member as FieldInfo;
                object fieldValue = fi.GetValue(ce.Value);
                Expression constantExpr = Expression.Constant(fieldValue);
                Visit(constantExpr);
            }

            return node;
        }
      
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConstantExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
           object obj = HandleCallType(node.Value);

            if (obj is string)
            {
                OutString(obj);
            }
            else if (
                obj is int || 
                obj is Int16 ||
                obj is Int32 || 
                obj is Int64 ||
                obj is uint||
                obj is UInt16||
                obj is UInt32||
                obj is UInt64||
                obj is double||
                obj is decimal||
                obj is float||        
                obj is Single)
            {
                Out(string.Format("{0}", obj));
            }
            else if (obj is DateTime || obj is TimeSpan)
            {
                Out(string.Format("'{0}'", obj.ToString()));
            }
            else if (obj is Guid)
            {
                Out(string.Format("'{0}'", obj.ToString()));
            }
            else if (obj is bool || obj is Boolean)
            {
                Out(string.Format("{0}", ((bool)obj) ? 1 : 0));
            }
            else if (obj == null)
            {
                Out("''");
            }
            else
            {
                StringBuilder ex = new StringBuilder();
                ex.Append("请检查条件表达式树是否存在不合法的数据类型作为条件验证!");
                ex.Append("允许设置为条件的合法数据类型:string,int,Int16,Int32,Int64,uint,UInt16,UInt32,UInt64,double,decimal,float,Single,DateTime,Guid,bool,Boolean");
                ex.Append("允许设置为条件的合法操作:string.StartsWith,sting.EndsWith,Equals,Contains,object.Prase,算数表达式,传递变量");
                ex.Append("允许设置为条件的合法运算符:+、-、*、/、%、>、>=、<、<=、==、!=、&&、||、!");
                throw new Exception(ex.ToString());
            }

            return node;
        }

    

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MethodCallExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "StartsWith")
            {
                Out("(");
                Visit(node.Object);
                startsWith = true;
                Out(" ");
                Out(Like);
                Out(" ");
                ExecuteVisit(node);   
                Out(")");
            }
            else if (node.Method.Name == "EndsWith")
            {
                Out("(");
                Visit(node.Object);
                endsWith = true;
                Out(" ");
                Out(Like);
                Out(" ");
                ExecuteVisit(node);   
                Out(")");
            }
            else if (node.Method.Name == "Equals")
            {
                Out("(");
                Visit(node.Object);
                Out(" ");
                Out(Equal);
                Out(" ");
                ExecuteVisit(node);   
                Out(")");
            }
            else if (node.Method.Name == "Contains")
            {
                Out("(");
                Visit(node.Object);
                contains = true;
                Out(" ");
                Out(Like);
                Out(" ");
                ExecuteVisit(node);   
                Out(")");
            }
            else if (node.Method.Name == "Parse")
            {
                Visit(node.Object);

                SetCallType(node);

                ExecuteVisit(node);  
            }
            else
            {
                StringBuilder ex = new StringBuilder();
                ex.Append("当前设置的条件表达式右式不合法!");
                ex.Append("允许设置为条件的表达式右式操作:string.StartsWith,sting.EndsWith,Equals,Contains,object.Prase,算数表达式");
                ex.Append("解决办法:可以尝试使用传递变量方式解决该问题!");
                throw new Exception(ex.ToString());
            }
         
            return node;
        }

        #endregion

        #region ==== 异常处理重写方法 ====

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BlockExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.CatchBlock"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConditionalExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DebugInfoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DefaultExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DynamicExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDynamic(System.Linq.Expressions.DynamicExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ElementInit"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.GotoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.IndexExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.InvocationExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelTarget"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression&lt;T&gt;"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ListInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LoopExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberAssignment"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberListBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberMemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewArrayExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ParameterExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.RuntimeVariablesExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchCase"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TypeBinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.UnaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            throw new NotSupportedException(node.GetType().Name);
        }

        #endregion

        #endregion

        #region ==== 接口实现 ====

        /// <summary>
        /// Builds the WHERE clause from the given expression object.
        /// </summary>
        /// <param name="expression">The expression object.</param>
        /// <returns>The <c>Apworks.Storage.Builders.WhereClauseBuildResult</c> instance
        /// which contains the build result.</returns>
        public WhereExpressionBuildResult BuildWhereExpression(Expression<Func<T, bool>> expression)
        {
            this.sb.Clear();
            this.parameterValues.Clear();
            this.Visit(expression.Body);
            WhereExpressionBuildResult result = new WhereExpressionBuildResult
            {
                WhereExpression = string.IsNullOrEmpty(sb.ToString())?null:string.Format(" WHERE {0}",sb.ToString())
            };
            return result;
        }

        #endregion

        #region ==== 私有方法 ====

        private void Out(string s)
        {
            sb.Append(s);
        }

        private void OutString(object obj)
        {
            if (startsWith)
            {
                Out(string.Format("'{0}{1}'", obj.ToString(), LikeSymbol));
            }
            else if (endsWith)
            {
                Out(string.Format("'{0}{1}'", LikeSymbol, obj.ToString()));
            }
            else if (contains)
            {
                Out(string.Format("'{0}{1}{0}'", LikeSymbol, obj.ToString()));
            }
            else
            {
                Out(string.Format("'{0}'", obj.ToString()));
            }
        }

        private void ExecuteVisit(MethodCallExpression node)
        {
            if (node.Arguments == null || node.Arguments.Count != 1)
                throw new NotSupportedException(node.GetType().Name);

            Expression expr = node.Arguments[0];

            if (expr is ConstantExpression || expr is MemberExpression)
            {
                Visit(expr);
            }
        }

        private object HandleCallType(object obj)
        {
            if (callType != CallType.NULL)
            {
                switch (callType)
                {
                    case CallType.INT16:
                        obj = Int16.Parse(obj.ToString());
                        break;
                    case CallType.INT32:
                        obj = Int32.Parse(obj.ToString());
                        break;
                    case CallType.INT64:
                        obj = Int64.Parse(obj.ToString());
                        break;
                    case CallType.UINT16:
                        obj = UInt16.Parse(obj.ToString());
                        break;
                    case CallType.UINT32:
                        obj = UInt32.Parse(obj.ToString());
                        break;
                    case CallType.UINT64:
                        obj = UInt64.Parse(obj.ToString());
                        break;
                    case CallType.DOUBLE:
                        obj = double.Parse(obj.ToString());
                        break;
                    case CallType.DECIMAL:
                        obj = decimal.Parse(obj.ToString());
                        break;
                    case CallType.BYTE:
                        obj = Byte.Parse(obj.ToString());
                        break;
                    case CallType.SINGLE:
                        obj = Single.Parse(obj.ToString());
                        break;
                    case CallType.DATETIME:
                        obj = DateTime.Parse(obj.ToString());
                        break;
                    case CallType.TIMESPAN:
                        obj = TimeSpan.Parse(obj.ToString());
                        break;
                    case CallType.GUID:
                        obj = Guid.Parse(obj.ToString());
                        break;
                    case CallType.BOOL:
                        obj = bool.Parse(obj.ToString());
                        break;
                }

                callType = CallType.NULL;
            }
            return obj;
        }

        private void SetCallType(MethodCallExpression node)
        {
            switch (node.Method.ReturnType.FullName)
            {
                case "System.Int16":
                    callType = CallType.INT16;
                    break;
                case "System.Int32":
                    callType = CallType.INT32;
                    break;
                case "System.Int64":
                    callType = CallType.INT64;
                    break;
                case "System.UInt16":
                    callType = CallType.UINT16;
                    break;
                case "System.UInt32":
                    callType = CallType.UINT32;
                    break;
                case "System.UInt64":
                    callType = CallType.UINT64;
                    break;
                case "System.Double":
                    callType = CallType.DOUBLE;
                    break;
                case "System.Decimal":
                    callType = CallType.DECIMAL;
                    break;
                case "System.Byte":
                    callType = CallType.BYTE;
                    break;
                case "System.Single":
                    callType = CallType.SINGLE;
                    break;
                case "System.DateTime":
                    callType = CallType.DATETIME;
                    break;
                case "System.TimeSpan":
                    callType = CallType.TIMESPAN;
                    break;
                case "System.Guid":
                    callType = CallType.GUID;
                    break;
                case "System.Boolean":
                    callType = CallType.BOOL;
                    break;
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        private enum CallType
        {
            NULL,
            INT16,
            INT32,
            INT64,
            UINT16,
            UINT32,
            UINT64,
            DOUBLE,
            DECIMAL,
            BYTE,
            SINGLE,
            DATETIME,
            TIMESPAN,
            GUID,
            BOOL,
        }

        #endregion
    }
}
