/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Utilities.Format;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    public class TDbTableService : FileGenerator
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// using 节的信息
        /// </summary>
        private Using usingStage = new Using();

        /// <summary>
        /// SQL条件表达式实例字典
        /// </summary>
        private static readonly Dictionary<SourceType, string> typeExpInstanceDic = new Dictionary<SourceType, string>();

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 模板
        /// </summary>
        public TemplateDbTableInfo Template
        {
            get;
            set;
        }

        #endregion

        #region === 构造函数 ====

        /// <summary>
        /// 根据制定的方法信息，创建一个客户端服务代码文件生成器
        /// </summary>
        /// <param name="method">要访问的方法信息</param>
        public TDbTableService(SourceType sourceType, TemplateDbTableInfo template)
        {
            this.SourceType = sourceType;
            this.Template = template;
            this.FileName = this.Template.Name + ".cs";

            if (template.SUsings != null && template.SUsings.Count > 0)
            {
                foreach (var item in template.SUsings)
                {
                    this.usingStage.Add(item);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        static TDbTableService()
        {
            typeExpInstanceDic.Clear();
            typeExpInstanceDic.Add(SourceType.OLEDB, "OledbWhereExpressionBuilder");
            typeExpInstanceDic.Add(SourceType.ORACLE, "OracleWhereExpressionBuilder");
            typeExpInstanceDic.Add(SourceType.SQLSERVER, "SqlWhereExpressionBuilder");
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 创建生成器
        /// </summary>
        /// <returns></returns>
        protected override ICodeGenerator CreateGenerator()
        {
            Document doc = new Document();

            doc.Content.Add(this.CreateFileTitle());
            doc.Content.Add(this.usingStage);
            doc.Content.Add(this.CreateNameSpace());

            return doc;
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 创建文件头注释
        /// </summary>
        /// <returns>头注释生成器</returns>
        private ICodeGenerator CreateFileTitle()
        {
            FileTitleComment result = new FileTitleComment();

            if (this.Template.STitleComments != null && this.Template.STitleComments.Count > 0)
            {
                foreach (var comment in this.Template.STitleComments)
                {
                    result.Lines.Add(comment);
                }
            }

            return result;
        }

        /// <summary>
        /// 创建命名空间
        /// </summary>
        /// <returns></returns>
        private ICodeGenerator CreateNameSpace()
        {
            NameSpace result = new NameSpace();

            // 命名空间名称
            result.Name = "ClassLibrary1";//默认

            if (!string.IsNullOrEmpty(this.Template.SNameSpace))
            {
                result.Name = this.Template.SNameSpace;
            }

            // 内容
            result.AddContent(this.CreateActionClass());

            return result;
        }

        /// <summary>
        /// 创建动作接口
        /// </summary>
        /// <returns>动作接口生成器</returns>
        private Class CreateActionClass()
        {
            Class result = new Class();


            // 注释
            result.Comment = CreateHeader();

            // 基本信息
            result.IsPublic = true;
            result.IsStatic = true;
            result.Name = this.Template.Name;

            // 私有成员
            Region memberRegion = new Region();
            memberRegion.Name = "==== 私有字段 ====";
            memberRegion.AddContent(new Code("/// <summary>"));
            memberRegion.AddContent(new Code("/// 数据访问表字典"));
            memberRegion.AddContent(new Code("/// <summary>"));
            memberRegion.AddContent(new Code("private static readonly Dictionary<TableName, IBaseTable> tables = new Dictionary<TableName, IBaseTable>();"));
            result.AddRegion(memberRegion);

            // 构造函数
            Region constructorRegion = new Region();
            constructorRegion.Name = "==== 构造函数 ====";
            constructorRegion.AddContent(new Code("/// <summary>"));
            constructorRegion.AddContent(new Code("/// 构造函数"));
            constructorRegion.AddContent(new Code("/// <summary>"));
            constructorRegion.AddContent(new Code("static DbHelper()"));
            constructorRegion.AddContent(new Code("{"));
            constructorRegion.AddContent(new Code("    var types = typeof({0}).Assembly.GetTypes();",this.Template.Name));
            constructorRegion.AddContent(new Code(""));
            constructorRegion.AddContent(new Code("    if (types.Length > 0)"));
            constructorRegion.AddContent(new Code("    {"));
            constructorRegion.AddContent(new Code("        foreach (var t in types)"));
            constructorRegion.AddContent(new Code("        {"));
            constructorRegion.AddContent(new Code("            var indexModeAttributes = t.GetCustomAttributes(typeof(TableAttribute), true);"));
            constructorRegion.AddContent(new Code(""));
            constructorRegion.AddContent(new Code("            if (indexModeAttributes.Length > 0)"));
            constructorRegion.AddContent(new Code("            {"));
            constructorRegion.AddContent(new Code("                tables.Add((indexModeAttributes[0] as TableAttribute).Name,"));
            constructorRegion.AddContent(new Code("                      (IBaseTable)typeof({0}).Assembly.CreateInstance(t.FullName, true));",this.Template.Name));
            constructorRegion.AddContent(new Code("            }"));
            constructorRegion.AddContent(new Code("        }"));
            constructorRegion.AddContent(new Code("    }"));
            constructorRegion.AddContent(new Code("}"));
            result.AddRegion(constructorRegion);

            //方法生成逻辑
            Region methordRegion = new Region();
            methordRegion.Name = "==== 方法 ====";

            var methords = this.CreateMethords();
            if (methords != null && methords.Count > 0)
            {
                foreach (var methrod in methords)
                {
                    methordRegion.AddContent(methrod);
                    methordRegion.AddContent(new BlankLine());
                }
            }

            result.AddRegion(methordRegion);

            return result;
        }

        /// <summary>
        /// 创建参数属性
        /// </summary>
        /// <param name="item">参数信息</param>
        /// <returns>属性生成器</returns>
        private List<Methord> CreateMethords()
        {
            List<Methord> result = new List<Methord>();

            for (int i = 0; i < this.Template.MethordOverloadCount; i++)
            {
                var genericList = string.Empty;         //泛型参数列表
                var paraList = string.Empty;            //参数列表
                var boParaList = string.Empty;          //集合参数列表
                var conditionParaList = string.Empty;   //条件参数列表
                var expressionParaList = string.Empty;  //表达式参数列表
                var genericConstrainList = string.Empty;//泛型约束列表
                var retrunList1 = string.Empty;         //方法1返回值项列表
                var retrunList2 = string.Empty;         //方法2返回值项列表
                var retrunList3 = string.Empty;         //方法3返回值项列表
                var retrunList4 = string.Empty;        //方法4返回值项列表

                Methord methord1 = new Methord();
                DocumentComment comment1 = new DocumentComment();
                comment1.SummaryLines.Add("向数据库表中添加记录", false);

                Methord methord2 = new Methord();
                DocumentComment comment2 = new DocumentComment();
                comment2.SummaryLines.Add("向数据库表中添加多条记录", false);

                Methord methord3 = new Methord();
                DocumentComment comment3 = new DocumentComment();
                comment3.SummaryLines.Add("向数据库表中更新记录", false);

                Methord methord4 = new Methord();
                DocumentComment comment4 = new DocumentComment();
                comment4.SummaryLines.Add("删除数据库表记录", false);

                for (int j = 0; j < i + 1; j++)
                {
                    if (i == 0)
                    {
                        comment1.SummaryLines.Add("<param name=\"t\">业务数据对象</param>", true);
                        comment2.SummaryLines.Add("<param name=\"t\">业务数据对象集合</param>", true);
                        comment3.SummaryLines.Add("<param name=\"dataCondition\">业务数据及条件</param>", true);
                        comment4.SummaryLines.Add("<param name=\"condition\">Where条件表达式树</param>", true);

                        genericList = "T";
                        paraList = "T t";
                        boParaList = "BoList<T> t";
                        conditionParaList = "DataCondition<T> dataCondition";
                        expressionParaList = "Expression<Func<T, bool>> condition";
                        genericConstrainList = "where T : BusinessObject, new()";

                        retrunList1 = "tables[(TableName)Enum.Parse(typeof(TableName), t.GetType().Name, true)].Add(action, t).IsSuccessful";
                        retrunList2 = "tables[(TableName)Enum.Parse(typeof(TableName), t.GetType().Name, true)].Add(action, t as IBoList).IsSuccessful";
                        retrunList3 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), dataCondition.Data.GetType().Name, true)].Update(action, dataCondition.Data, new {0}<T>().BuildWhereExpression(dataCondition.Condition).WhereExpression).IsSuccessful", typeExpInstanceDic[this.SourceType]);
                        retrunList4 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), new T().GetType().Name, true)].Remove(action, new {0}<T>().BuildWhereExpression(condition).WhereExpression).IsSuccessful", typeExpInstanceDic[this.SourceType]);
                     }
                    else
                    {
                        comment1.SummaryLines.Add(string.Format("<param name=\"t{0}\">业务数据对象</param>", j + 1), true);
                        comment2.SummaryLines.Add(string.Format("<param name=\"t{0}\">业务数据对象集合</param>", j + 1), true);
                        comment3.SummaryLines.Add(string.Format("<param name=\"dataCondition{0}\">业务数据及条件</param>", j + 1), true);
                        comment4.SummaryLines.Add(string.Format("<param name=\"condition{0}\">Where条件表达式树</param>", j + 1), true);

                        if (j == 0)
                        {
                            genericList = string.Format("T{0}", j + 1);
                            paraList = string.Format("T{0} t{0}", j + 1);
                            boParaList = string.Format("BoList<T{0}> t{0}", j + 1);
                            conditionParaList = string.Format("DataCondition<T{0}> dataCondition{0}", j + 1);
                            expressionParaList = string.Format("Expression<Func<T{0}, bool>> condition{0}", j + 1);
                            genericConstrainList = string.Format("where T{0} : BusinessObject, new()", j + 1);

                            retrunList1 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), t{0}.GetType().Name, true)].Add(action, t{0}).IsSuccessful", j + 1);
                            retrunList2 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), t{0}.GetType().Name, true)].Add(action, t{0} as IBoList).IsSuccessful", j + 1);
                            retrunList3 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), dataCondition{0}.Data.GetType().Name, true)].Update(action, dataCondition{0}.Data, new {1}<T{0}>().BuildWhereExpression(dataCondition{0}.Condition).WhereExpression).IsSuccessful", j + 1, typeExpInstanceDic[this.SourceType]);
                            retrunList4 = string.Format("tables[(TableName)Enum.Parse(typeof(TableName), new T{0}().GetType().Name, true)].Remove(action, new {1}<T{0}>().BuildWhereExpression(condition{0}).WhereExpression).IsSuccessful", j + 1, typeExpInstanceDic[this.SourceType]);
                            continue;
                        }

                        genericList = genericList + string.Format(",T{0}", j + 1);
                        paraList = paraList + string.Format(",T{0} t{0}", j + 1);
                        boParaList = boParaList + string.Format(",BoList<T{0}> t{0}", j + 1);
                        conditionParaList = conditionParaList + string.Format(",DataCondition<T{0}> dataCondition{0}", j + 1);
                        expressionParaList = expressionParaList + string.Format(",Expression<Func<T{0}, bool>> condition{0}", j + 1);
                        genericConstrainList = genericConstrainList + string.Format(" where T{0} : BusinessObject, new()", j + 1);

                        retrunList1 = retrunList1 + string.Format(" && tables[(TableName)Enum.Parse(typeof(TableName), t{0}.GetType().Name, true)].Add(action, t{0}).IsSuccessful", j + 1);
                        retrunList2 = retrunList2 + string.Format(" && tables[(TableName)Enum.Parse(typeof(TableName), t{0}.GetType().Name, true)].Add(action, t{0} as IBoList).IsSuccessful", j + 1);
                        retrunList3 = retrunList3 + string.Format(" && tables[(TableName)Enum.Parse(typeof(TableName), dataCondition{0}.Data.GetType().Name, true)].Update(action, dataCondition{0}.Data, new {1}<T{0}>().BuildWhereExpression(dataCondition{0}.Condition).WhereExpression).IsSuccessful", j + 1, typeExpInstanceDic[this.SourceType]);
                        retrunList4 = retrunList4 + string.Format(" && tables[(TableName)Enum.Parse(typeof(TableName), new T{0}().GetType().Name, true)].Remove(action, new {1}<T{0}>().BuildWhereExpression(condition{0}).WhereExpression).IsSuccessful", j + 1, typeExpInstanceDic[this.SourceType]);
                    }
                }

                comment1.SummaryLines.Add("<returns>true:成功 false:失败</returns>", true);
                comment2.SummaryLines.Add("<returns>true:成功 false:失败</returns>", true);
                comment3.SummaryLines.Add("<returns>true:成功 false:失败</returns>", true);
                comment4.SummaryLines.Add("<returns>true:成功 false:失败</returns>", true);

                methord1.Comment = comment1;
                methord1.IsRedefind = true;
                methord1.AddCode(new Code("public static bool Add<{0}>({1})",genericList,paraList));
                methord1.AddCode(new Code("    {0}", genericConstrainList));
                methord1.AddCode(new Code("{"));
                methord1.AddCode(new Code("    using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
                methord1.AddCode(new Code("    {"));
                methord1.AddCode(new Code("        NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();"));
                methord1.AddCode(new Code(""));
                methord1.AddCode(new Code("        try"));
                methord1.AddCode(new Code("        {"));
                methord1.AddCode(new Code("            retrun {0};", retrunList1));
                methord1.AddCode(new Code("        }"));
                methord1.AddCode(new Code("        catch (Exception ex)"));
                methord1.AddCode(new Code("        {"));
                methord1.AddCode(new Code("            throw (ex);"));
                methord1.AddCode(new Code("        }"));
                methord1.AddCode(new Code("    }"));
                methord1.AddCode(new Code("}"));

                methord2.Comment = comment2;
                methord2.IsRedefind = true;
                methord2.AddCode(new Code("public static bool Add<{0}>({1})", genericList,boParaList));
                methord2.AddCode(new Code("    {0}", genericConstrainList));
                methord2.AddCode(new Code("{"));
                methord2.AddCode(new Code("    using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
                methord2.AddCode(new Code("    {"));
                methord2.AddCode(new Code("        NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();"));
                methord2.AddCode(new Code(""));
                methord2.AddCode(new Code("        try"));
                methord2.AddCode(new Code("        {"));
                methord2.AddCode(new Code("            retrun {0};", retrunList2));
                methord2.AddCode(new Code("        }"));
                methord2.AddCode(new Code("        catch (Exception ex)"));
                methord2.AddCode(new Code("        {"));
                methord2.AddCode(new Code("            throw (ex);"));
                methord2.AddCode(new Code("        }"));
                methord2.AddCode(new Code("    }"));
                methord2.AddCode(new Code("}"));

                methord3.Comment = comment3;
                methord3.IsRedefind = true;
                methord3.AddCode(new Code("public static bool Update<{0}>({1})", genericList, conditionParaList));
                methord3.AddCode(new Code("    {0}", genericConstrainList));
                methord3.AddCode(new Code("{"));
                methord3.AddCode(new Code("    using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
                methord3.AddCode(new Code("    {"));
                methord3.AddCode(new Code("        NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();"));
                methord3.AddCode(new Code(""));
                methord3.AddCode(new Code("        try"));
                methord3.AddCode(new Code("        {"));
                methord3.AddCode(new Code("            retrun {0};", retrunList3));
                methord3.AddCode(new Code("        }"));
                methord3.AddCode(new Code("        catch (Exception ex)"));
                methord3.AddCode(new Code("        {"));
                methord3.AddCode(new Code("            throw (ex);"));
                methord3.AddCode(new Code("        }"));
                methord3.AddCode(new Code("    }"));
                methord3.AddCode(new Code("}"));

                methord4.Comment = comment4;
                methord4.IsRedefind = true;
                methord4.AddCode(new Code("public static bool Remove<{0}>({1})", genericList, expressionParaList));
                methord4.AddCode(new Code("    {0}", genericConstrainList));
                methord4.AddCode(new Code("{"));
                methord4.AddCode(new Code("    using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
                methord4.AddCode(new Code("    {"));
                methord4.AddCode(new Code("        NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();"));
                methord4.AddCode(new Code(""));
                methord4.AddCode(new Code("        try"));
                methord4.AddCode(new Code("        {"));
                methord4.AddCode(new Code("            retrun {0};", retrunList4));
                methord4.AddCode(new Code("        }"));
                methord4.AddCode(new Code("        catch (Exception ex)"));
                methord4.AddCode(new Code("        {"));
                methord4.AddCode(new Code("            throw (ex);"));
                methord4.AddCode(new Code("        }"));
                methord4.AddCode(new Code("    }"));
                methord4.AddCode(new Code("}"));

                result.Add(methord1);
                result.Add(methord2);
                result.Add(methord3);
                result.Add(methord4);
            }

            Methord methord5 = new Methord();
            DocumentComment comment5 = new DocumentComment();
            comment5.SummaryLines.Add("查询数据库表记录", false);
            comment5.SummaryLines.Add("<param name=\"condition\">Where条件表达式树</param>", true);
            comment5.SummaryLines.Add("<returns>有数据返回的数据访问结果</returns>", true);

            methord5.Comment = comment5;
            methord5.IsRedefind = true;
            methord5.AddCode(new Code("public static DataResult<IBoList> Select<T>(Expression<Func<T, bool>> condition)"));
            methord5.AddCode(new Code("    where T : BusinessObject, new()"));
            methord5.AddCode(new Code("{"));
            methord5.AddCode(new Code("    using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
            methord5.AddCode(new Code("    {"));
            methord5.AddCode(new Code("        QueryRequest action = mainDb.NewAction<QueryRequest>();"));
            methord5.AddCode(new Code(""));
            methord5.AddCode(new Code("        try"));
            methord5.AddCode(new Code("        {"));
            methord5.AddCode(new Code("            retrun tables[(TableName)Enum.Parse(typeof(TableName), new T().GetType().Name, true)].Select(action,new {0}<T>().BuildWhereExpression(condition).WhereExpression);", typeExpInstanceDic[this.SourceType]));
            methord5.AddCode(new Code("        }"));
            methord5.AddCode(new Code("        catch (Exception ex)"));
            methord5.AddCode(new Code("        {"));
            methord5.AddCode(new Code("            throw (ex);"));
            methord5.AddCode(new Code("        }"));
            methord5.AddCode(new Code("    }"));
            methord5.AddCode(new Code("}"));

            result.Add(methord5);

            return result;
        }


        /// <summary>
        /// 生成类的头注释生成器
        /// </summary>
        /// <returns>类的头注释生成器</returns>
        private DocumentComment CreateHeader()
        {
            DocumentComment comments = new DocumentComment();

            if (this.Template.SDocumentComment != null && this.Template.SDocumentComment.Count > 0)
            {
                foreach (var comment in this.Template.SDocumentComment)
                {
                    comments.SummaryLines.Add(comment, false);
                }
            }

            return comments;
        }

        #endregion
    }
}
