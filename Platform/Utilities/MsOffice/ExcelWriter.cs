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
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Alive.Foundation.Utilities.MsOffice
{
    #region ==== 结构体 ====

    /// <summary>
    /// 第n个表格的第x行第y列写入的内容
    /// </summary>
    public struct ExcelTable
    {
        /// <summary>
        /// 列
        /// </summary>
        public int column;

        /// <summary>
        /// 行
        /// </summary>
        public int row;

        /// <summary>
        /// 文本
        /// </summary>
        public string content;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableIndex">表索引</param>
        /// <param name="column">列</param>
        /// <param name="row">行</param>
        /// <param name="content">文本</param>
        public ExcelTable(int row, int column, string content)
        {
            this.column = column;
            this.row = row;
            this.content = content;
        }
    }

    /// <summary>
    /// Cell样式相关
    /// </summary>
    public struct CellStyle
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int? width;

        /// <summary>
        /// 高度
        /// </summary>
        public int? height;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public CellStyle(int? width, int? height)
        {
            this.width = width;
            this.height = height;
        }
    }

    #endregion 

    /// <summary>
    /// Excel数据写入工具
    /// </summary>
    public class ExcelWriter:IDisposable
    {
        #region ==== 属性 ====

        /// <summary>
        /// 保存的完整路径名
        /// </summary>
        private string fileFullName=string.Empty;

        /// <summary>
        /// 获得数据连接
        /// </summary>
        Excel.Application objApp;

        Excel._Workbook objBook;

        Excel.Workbooks objBooks;

        Excel.Sheets objSheets;

        Excel._Worksheet objSheet;

        //Excel.Range range;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的数据连接对象
        /// </summary>
        /// <param name="fileName">Excel 文件名（包含路径）</param>
        public ExcelWriter(string fileName)
        {
            this.fileFullName=fileName;
            objApp = new Excel.Application();
            objBooks = objApp.Workbooks;
            objBook = objBooks.Add(Missing.Value);
            objSheets = objBook.Worksheets;
            objSheet = (Excel._Worksheet)objSheets.get_Item(1);
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 在第n个表格的第x行第y列写入指定的内容。
        /// </summary>
        /// <param name="write">副本文件实例</param>
        /// <param name="ti">表格信息</param>
        public void WriteInExcel(ExcelTable et)
        {
            try
            {
#warning 这里的设计不好，应该把Excel时1起索引这一细节隐藏起来，不应该暴露给调用方。这里应该在2.0改进。
                objSheet.Cells[et.row, et.column] = et.content;
            }
            catch
            { }
        }
        /// <summary>
        /// 在第n个表格的第x行第y列写入指定的内容。
        /// </summary>
        /// <param name="write">副本文件实例</param>
        /// <param name="ti">表格信息</param>
        public void WriteInExcel(ExcelTable et,CellStyle cs)
        {
            try
            {
                objSheet.Cells[et.row, et.column] = et.content;

                //if (cs.width != null)
                //{
                //    objSheet.Cells.Width = cs.width;
                //}

                //if (cs.height != null)
                //{
                //    objSheet.Cells.Height = cs.height;
                //}
            }
            catch
            { }
        }


        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <returns>是否保存成功</returns>
        public bool Save()
        {
            try
            {
                bool isSave = false;

                if (isSave)
                {
                    objApp.Visible = true;
                    objApp.UserControl = true;
                }
                else
                {
                    objBook.SaveAs(Path.Combine(fileFullName, string.Empty),
                        Excel.XlFileFormat.xlWorkbookNormal,
                        Missing.Value,
                        Missing.Value,
                        Missing.Value,
                        Missing.Value,
                        Excel.XlSaveAsAccessMode.xlExclusive,
                        Missing.Value,
                        Missing.Value,
                        Missing.Value,
                        Missing.Value,
                        Missing.Value);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 是否Excel开启的对象
        /// </summary>
        /// <param name="obj">对象</param>
        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion


        #region ==== 接口实现 ====

        #region IDisposable 成员

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.objBook.Close(true,
                Missing.Value,
                Missing.Value);
            objApp.Quit();

            ReleaseObject(objSheet);
            ReleaseObject(objBook);
            ReleaseObject(objApp);
        }

        #endregion

        #endregion
    }
}
