/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.Data;

namespace Alive.Foundation.Utilities.MsOffice
{
    #region ==== 结构体 ====

    /// <summary>
    /// 第n个表格的第x行第y列写入的内容
    /// </summary>
    public struct WordTable
    {
        /// <summary>
        /// 表索引
        /// </summary>
        public int tableIndex;

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
        public WordTable(int tableIndex, int column, int row, string content)
        {
            this.tableIndex = tableIndex;
            this.column = column;
            this.row = row;
            this.content = content;
        }
    }

    #endregion 

    public class WordWriter : IDisposable
    {
        #region ==== 私有字段 ====

        Microsoft.Office.Interop.Word._Application oWord = null;
        Microsoft.Office.Interop.Word._Document oDoc = null;

        /// <summary>
        /// 创建的副本数据连接
        /// </summary>
        private _Application conn = null;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得数据连接
        /// </summary>
        protected _Application Connection
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templetName">要读取并写入的模板文件名称</param>
        public WordWriter(object templetName)
        {
            conn = WordOpen(templetName, true);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templetName">要读取的模板文件名称</param>
        /// <param name="saveName">要保存的文件名称</param>
        public WordWriter(object templetName,object saveName)
        {
            conn = NewTemplate(templetName, saveName);
        }

        /// <summary>
        /// 创建一个新的数据连接对象
        /// </summary>
        /// <param name="fileName">Word 文件名（包含路径）</param>
        private _Application WordOpen(object fileName, bool visible)
        {

            try
            {
                if (fileName == null || fileName.ToString().Trim() == "") return null;
                if (File.Exists(fileName.ToString()))
                {
                    object oMissing = Missing.Value;

                    oWord = new Microsoft.Office.Interop.Word.Application();
                    oWord.DisplayAlerts = WdAlertLevel.wdAlertsNone;        //避免打开word的所有警告信息
                    oWord.Visible = visible;
                    object wordName1 = fileName;
                    object vis = visible;
                    oDoc = oWord.Documents.Open(ref fileName,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref vis, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                }
                return oWord;
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                this.Connection = oWord;
                return null;
            }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 创建副本准备编辑
        /// </summary>
        /// <param name="fileName">要读取的模板文件</param>
        /// <param name="fileName">副本文件</param>
        /// <returns>WordWriter的实例</returns>
        private _Application NewTemplate(object fileName, object tempFile)
        {
            //_Application iOldWord = null;
            _Application iWord = null;
            //iOldWord = WordOpen(fileName, false);
            //this.CloseDoc(iOldWord, false);
            if (File.Exists(tempFile.ToString()))
            {
                File.Delete(tempFile.ToString());
            }
            File.Copy(fileName.ToString(), tempFile.ToString());

            iWord = WordOpen(tempFile, true);
            return iWord;
        }



        /// <summary>
        /// 在第n个表格的第x行第y列写入指定的内容。
        /// </summary>
        /// <param name="write">副本文件实例</param>
        /// <param name="ti">表格信息</param>
        /// <returns>写入是否成功</returns>
        public bool WriteInTable(WordTable ti)
        {
            try
            {
                if (ti.tableIndex.ToString() != null && ti.column.ToString() != null && ti.row.ToString() != null)
                {
                    if (conn.ActiveDocument.Tables[ti.tableIndex].Rows.Count<ti.row)
                    {
                        object miss = System.Reflection.Missing.Value;
                        conn.ActiveDocument.Tables[ti.tableIndex].Rows.Add(ref miss);
                    }
                    conn.ActiveDocument.Tables[ti.tableIndex].Cell(ti.row, ti.column).Range.Text = ti.content;
                
                }
                //this.CloseDoc(conn, true);
                return true;
            }

            catch (Exception e)
            {
                string message = e.ToString();
                return false;                
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public void WriteText(string tag,string text)
        {
            foreach (Microsoft.Office.Interop.Word.Field field in oWord.ActiveDocument.Fields)
            {
                if (field.Code.Text.Trim() == tag)
                {
                    field.Result.Text = text;
                    
                }
            }

            oWord.ActiveDocument.Sections[1].Range.Select();
            oWord.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;
            Microsoft.Office.Interop.Word.Selection selection = oWord.Selection;
            foreach (Field field in selection.HeaderFooter.Range.Fields)
            {
                if (field.Code.Text.Trim() == tag)
                {
                    field.Result.Text = text;
                }
            }

            if (selection.HeaderFooter.Range.Fields.Count <= 0)
            {
                oWord.ActiveDocument.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
            }

            oWord.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekMainDocument;
            object story = WdUnits.wdStory;
            object obj = Missing.Value;
            selection.HomeKey(ref story, ref obj);
        }

        /// <summary>
        /// 生成空格字符串
        /// </summary>
        /// <param name="count">空格数量</param>
        /// <returns>空格字符串</returns>
        private string CreateSpace(int count)
        {
            string result = string.Empty;

            for (int i = 0; i < count; i++)
            {
                result += " ";
            }

            return result;
        }





        /// <summary>
        /// 关闭word
        /// </summary>
        /// <param name="write">word实例</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>关闭是否成功</returns>
        private void CloseDoc(_Application write, object isSave)
        {
            //object IsSave = true;
            object oMissing = Missing.Value;

            write.Documents.Close(ref isSave, ref oMissing, ref oMissing);
           
            write.Quit(ref oMissing, ref oMissing, ref oMissing); 
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.Connection != null)
            {
                object IsSave = false;
                object oMissing = Missing.Value;
                this.CloseDoc(this.Connection, false);
                this.CloseDoc(this.conn, false);         
                this.Connection.Documents.Close(ref IsSave, ref oMissing, ref oMissing);
            }


            if (this.oWord != null)
            {
                this.oWord.ActiveDocument.Save();
            }
        }

        #endregion
        #endregion
    }
}
