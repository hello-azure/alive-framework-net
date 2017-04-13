using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Alive.Foundation.Data;
//using TestConsoleApp.Builders;
using Alive.Foundation.Data.DataFields;
using Alive.Tools.CodeGenerator.Foundatation;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Tools.CodeGenerator.Foundatation.Builder;
using System.Data;
using TestConsoleApp.Builders;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> p = (c) => { return c > 1&&c+3<10; };

            var d1 = p.Method;
            var d2 = p.Target;
            var d3 = p.ToString();
            //var d4 = p.GetObjectData();
            var d5 = p.GetType();


            //if (types.Length > 0)
            //{
            //    foreach (var t in types)
            //    {
            //        var attributes = t.GetCustomAttributes(typeof(TableAttribute), true);

            //        if (attributes.Length > 0)
            //        {
            //            TableName name = (attributes[0] as TableAttribute).Name;
            //            DbTableBase instance = (DbTableBase)typeof(DbTableFactory).Assembly.CreateInstance(t.FullName, true);
            //            tableDic.Add(name, instance);
            //        }
            //    }
            //}
            var str = "zhang";
    
            DateTime date1 = DateTime.Now.AddYears(1);
            //Expression<Func<Info, bool>> exp = info => info.Name.Contains("aa") && info.Name.EndsWith("s");
            //Expression<Func<Info, bool>> exp = info => info.ID > 3;
            //Expression<Func<Info, bool>> exp = info => info.Name.Contains("aa") && info.Name.EndsWith("s") || info.ID > 3;
            //Expression<Func<Info, bool>> exp = info => info.Date > DateTime.Parse("2010-10-01");
            //Expression<Func<Info, bool>> exp = info => info.ID > int.Parse("10");
            //Expression<Func<Info, bool>> exp = info => info.IsDelete.Equals(true);
            //Expression<Func<Info, bool>> exp = info => info.IsDelete == (true);
            //Expression<Func<Info, bool>> exp = info => info.BDate == null;
            //Expression<Func<Info, bool>> exp = info => info.BDate != null;
            var bdata = new byte[] { 1, 0 };
            //Expression<Func<Info, bool>> exp = info => info.BDate != bdata;不支持
            int a = 10;
            //Expression<Func<Info, bool>> exp = info => info.ID > a;
            //Expression<Func<Info, bool>> exp = info => info.ID > a+1;
            //Expression<Func<Info, bool>> exp = info => info.Date > date1;
            //Expression<Func<Info, bool>> exp = info => info.Date > date1;

            Expression<Func<Info, bool>> exp = info => info.ID.Value > 1 && info.FullName.Value.StartsWith("s");
            WhereExpressionBuildResult where = new SqlWhereExpressionBuilder<Info>().BuildWhereExpression(exp);

            var whereStr = where.WhereExpression;

            Info info1 = new Info();
            info1.ID.Value = 1;
            bool v1 = info1.ID.HasSet;
            info1.FullName.Value = "";
            bool v2 = info1.FullName.HasSet;
            info1.C1.Value = 1;
            info1.C2.Value = true;
            info1.C3.Value = new byte();
            info1.C4.Value = new byte[] { };
            info1.C5.Value = DateTime.Now;
            info1.C6.Value = 1.0m;
            info1.C7.Value = 1;
            info1.C8.Value = 1;
            info1.C9.Value = Guid.NewGuid();
            info1.C10.Value = 1;
            info1.C11.Value = null;
            info1.C12.Value = 1;
            info1.C13.Value = 1;
            info1.C14.Value=new TimeSpan();

            BusinessObject data = info1;

            var da1 = data.GetNameMapping();
            var da2 = data.GetType("ID").ValueObject;
            var da3 = data.GetType("ID").ValueText;
            var da4 = data.GetType("ID").Name;
            var da5 = data.GetType("ID").HasSet;
            var da6 = data.GetType();
            var da7 = data.GetValueText("ID");
            var da8 = data.GetValueType("ID");

            var t1 = data.GetValueType("ID").FullName;//System.Int32-DbType.Int32
            var t2 = data.GetValueType("FullName").FullName;//System.String- DbType.String
            var t3 = data.GetValueType("C1").FullName;//System.Int64-DbType.Int64
            var t4 = data.GetValueType("C2").FullName;//System.Boolean-DbType.Boolean
            var t5 = data.GetValueType("C3").FullName;//System.Byte- DbType.Byte
            var t6 = data.GetValueType("C4").FullName;//System.Byte[]
            var t7 = data.GetValueType("C5").FullName;//System.DateTime- DbType.DateTime
            var t8 = data.GetValueType("C6").FullName;//System.Decimal- DbType.Decimal
            var t9 = data.GetValueType("C7").FullName;//System.Double-DbType.Double
            var t10 = data.GetValueType("C8").FullName;//System.Single-DbType.Single
            var t11 = data.GetValueType("C9").FullName;//System.Guid-DbType.Guid
            var t12 = data.GetValueType("C10").FullName;//System.Int64
            var t13 = data.GetValueType("C11").FullName;//System.Object-DbType.Object
            var t14 = data.GetValueType("C12").FullName;//System.Single-DbType.Single
            var t15 = data.GetValueType("C13").FullName;//System.Int16-DbType.Int16
            var t16 = data.GetValueType("C14").FullName;//System.TimeSpan
            
       

            //var types = info1.GetType().Name;
            
            //string s = "update userinfo set ID=@ID,D=@ID,";
            //int pos = s.LastIndexOf(',');
            //s = s.Substring(0, pos);

            //var data = exp.ToString();
            //var data2 = exp.ToCondition<Info>();

            //var paras = exp.Parameters;
            //DataCondition<Info> cccc = new DataCondition<Info>(exp);
            //var data3 = cccc.ToCondition();

            //var s1 = SqlBuilder.GetInsertor<Info>(SourceType.SQLSERVER, info1);
            //var s2 = SqlBuilder.GetUpdator<Info>(SourceType.SQLSERVER, info1);
            //var s3 = SqlBuilder.GetDeleter<Info>(SourceType.SQLSERVER, info1);
            //var s4 = SqlBuilder.GetSelector<Info>(SourceType.SQLSERVER, info1);

            //var s5 = SqlBuilder.GetInsertor<Info>(SourceType.OLEDB, info1);
            //var s6 = SqlBuilder.GetUpdator<Info>(SourceType.OLEDB, info1);
            //var s7 = SqlBuilder.GetDeleter<Info>(SourceType.OLEDB, info1);
            //var s8 = SqlBuilder.GetSelector<Info>(SourceType.OLEDB, info1);

            //var s9 = SqlBuilder.GetInsertor<Info>(SourceType.ORACLE, info1);
            //var s10 = SqlBuilder.GetUpdator<Info>(SourceType.ORACLE, info1);
            //var s11 = SqlBuilder.GetDeleter<Info>(SourceType.ORACLE, info1);
            //var s12 = SqlBuilder.GetSelector<Info>(SourceType.ORACLE, info1);
          
        }
    }

    public class DataCondition<T> where T:BusinessObject
    {
        private Expression<Func<T, bool>> exp;

        public DataCondition(Expression<Func<T, bool>> exp)
        {
            this.exp = exp;
        }

        public string ToCondition()
        {
            string result = string.Empty;

            if (exp != null)
            {
                //result = exp.ToString().
                //    Replace(string.Format("{0}", exp.Parameters[0].Name), string.Empty).
                //    Replace(".",string.Empty).
                //    Replace("=>",string.Empty).
                //    Replace("==","=").
                //    Replace("AndAlso", "AND").
                //    Replace("OrElse","OR");

                var node = exp.Body;

                while (node != null)
                { 
                   
                }
            }

            

            return result;
        }

    }

    public class Info : BusinessObject
    {
        public IntField ID
        {
            get
            {
                return this.GetField<IntField>("ID");
            }
            set
            {
                this.GetField<IntField>("ID").Value = value.Value;
            }
        }

        public StringField FullName
        {
            get
            {
                return this.GetField<StringField>("FullName");
            }
            set
            {
                this.GetField<StringField>("FullName").Value = value.Value;
            }
        }

        public BigIntField C1
        {
            get
            {
                return this.GetField<BigIntField>("C1");
            }
            set
            {
                this.GetField<BigIntField>("C1").Value = value.Value;
            }
        }

        public BoolField C2
        {
            get
            {
                return this.GetField<BoolField>("C2");
            }
            set
            {
                this.GetField<BoolField>("C2").Value = value.Value;
            }
        }

        public ByteField C3
        {
            get
            {
                return this.GetField<ByteField>("C3");
            }
            set
            {
                this.GetField<ByteField>("C3").Value = value.Value;
            }
        }

        public ByteArrayField C4
        {
            get
            {
                return this.GetField<ByteArrayField>("C4");
            }
            set
            {
                this.GetField<ByteArrayField>("C4").Value = value.Value;
            }
        }

        public DateTimeField C5
        {
            get
            {
                return this.GetField<DateTimeField>("C5");
            }
            set
            {
                this.GetField<DateTimeField>("C5").Value = value.Value;
            }
        }

        public DecimalField C6
        {
            get
            {
                return this.GetField<DecimalField>("C6");
            }
            set
            {
                this.GetField<DecimalField>("C6").Value = value.Value;
            }
        }

        public DoubleField C7
        {
            get
            {
                return this.GetField<DoubleField>("C7");
            }
            set
            {
                this.GetField<DoubleField>("C7").Value = value.Value;
            }
        }

        public FloatField C8
        {
            get
            {
                return this.GetField<FloatField>("C8");
            }
            set
            {
                this.GetField<FloatField>("C8").Value = value.Value;
            }
        }

        public GuidField C9
        {
            get
            {
                return this.GetField<GuidField>("C9");
            }
            set
            {
                this.GetField<GuidField>("C9").Value = value.Value;
            }
        }

        public LongField C10
        {
            get
            {
                return this.GetField<LongField>("C10");
            }
            set
            {
                this.GetField<LongField>("C10").Value = value.Value;
            }
        }

        public ObjcectField C11
        {
            get
            {
                return this.GetField<ObjcectField>("C11");
            }
            set
            {
                this.GetField<ObjcectField>("C11").Value = value.Value;
            }
        }

        public SingleField C12
        {
            get
            {
                return this.GetField<SingleField>("C12");
            }
            set
            {
                this.GetField<SingleField>("C12").Value = value.Value;
            }
        }

        public SmallIntField C13
        {
            get
            {
                return this.GetField<SmallIntField>("C13");
            }
            set
            {
                this.GetField<SmallIntField>("C13").Value = value.Value;
            }
        }

        public TimeSpanField C14
        {
            get
            {
                return this.GetField<TimeSpanField>("C14");
            }
            set
            {
                this.GetField<TimeSpanField>("C14").Value = value.Value;
            }
        }


        //public string Name
        //{
        //    get;
        //    set;
        //}

        //public DateTime Date
        //{
        //    get;
        //    set;
        //}

        //public bool IsDelete
        //{
        //    get;
        //    set;
        //}

        //public byte[] BDate
        //{
        //    get;
        //    set;
        //}
    }
}
