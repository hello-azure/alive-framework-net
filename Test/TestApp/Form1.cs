using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alive.Foundation.Data;
using Alive.Foundation.Storage;
namespace TestApp
{
    public partial class Form1 : Form
    {
        private string id = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserInfo ui = new UserInfo();
            ui.ID.Value =id= Guid.NewGuid().ToString();
            ui.UserName.Value = "xiaoqiang";
            ui.Password.Value = "12345";
            ui.RoleID.Value = "管理员";

            if (Insert(ui))
            {
                MessageBox.Show("操作成功");
            }
            else
            {
                MessageBox.Show("操作失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserInfo ui = new UserInfo();
            ui.ID.Value = id;
            ui.UserName.Value = "xiaoqiang";
            ui.Password.Value = "123456789";
            ui.RoleID.Value = "管理员";

            if (Update(ui))
            {
                MessageBox.Show("操作成功");
            }
            else
            {
                MessageBox.Show("操作失败");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Delete(id))
            {
                MessageBox.Show("操作成功");
            }
            else
            {
                MessageBox.Show("操作失败");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var data = GetData();
        }

        bool Insert(UserInfo ui)
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                StringBuilder sql = new StringBuilder();
                sql.Append("insert into UserInfo(");
                sql.Append("ID,UserName,Password,RoleID");
                sql.Append(") values (");
                sql.Append("@ID,@UserName,@Password,@RoleID");
                sql.Append(") ");

                action.SQL = sql.ToString();
                action.SetParameter("@ID", ui.ID.Value);
                action.SetParameter("@UserName", ui.UserName.Value);
                action.SetParameter("@Password", ui.Password.Value);
                action.SetParameter("@RoleID", ui.RoleID.Value);

                #region ==== Access 语句 ====

                //StringBuilder sql = new StringBuilder();
                //sql.Append("insert into [DUserInfo](");
                //sql.Append("[ID],[UName],[Password],[UState]");
                //sql.Append(") values (");
                //sql.Append("@ID,@UName,@Password,@UState");
                //sql.Append(") ");    

                //action.SQL = sql.ToString();
                //action.SetParameter("@ID", "aa");
                //action.SetParameter("@UName", "aa");
                //action.SetParameter("@Password", "aa");
                //action.SetParameter("@UState", "aa");
                //action.SetParameter("@ID", ui.ID.Value);
                //action.SetParameter("@UName", ui.UserName.Value);
                //action.SetParameter("@Password", ui.Password.Value);
                //action.SetParameter("@UState", ui.RoleID.Value);

                #endregion

                DbResult result = action.Execute();

                return result.IsSuccessful;
               
            }
        }

        bool Update(UserInfo ui)
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                StringBuilder sql = new StringBuilder();
                sql.Append("update UserInfo set ");
                sql.Append(" UserName = @UserName , ");
                sql.Append(" Password = @Password , ");
                sql.Append(" RoleID = @RoleID  ");
                sql.Append(" where ID=@ID ");

                action.SQL = sql.ToString();                
                action.SetParameter("@UserName", ui.UserName.Value);
                action.SetParameter("@Password", ui.Password.Value);
                action.SetParameter("@RoleID", ui.RoleID.Value);
                action.SetParameter("@ID", ui.ID.Value);

                DbResult result = action.Execute();

                return result.IsSuccessful;

            }
        }

        bool Delete(string primaryKey)
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                StringBuilder sql = new StringBuilder();
                sql.Append("delete from UserInfo");
                sql.Append(" where ID=@ID ");

                action.SQL = sql.ToString();
                action.SetParameter("@ID", primaryKey);

                DbResult result = action.Execute();

                return result.IsSuccessful;

            }
        }

        DataResult<UserInfoList> GetData()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                DataResult<UserInfoList> result = new DataResult<UserInfoList>();
                QueryRequest action = mainDb.NewAction<QueryRequest>();

                StringBuilder sql = new StringBuilder();
                sql.Append("select ID,UserName,Password,RoleID from UserInfo");
                action.SQL = sql.ToString();
                DbResult dbResult = action.Execute();

                if (dbResult.IsSuccessful)
                {
                    dbResult.Injector.SetFieldMapping("ID", "ID");
                    dbResult.Injector.SetFieldMapping("UserName", "UserName");
                    dbResult.Injector.SetFieldMapping("Password", "Password");
                    dbResult.Injector.SetFieldMapping("RoleID", "RoleID");
                    UserInfoList uiList = new UserInfoList();
                    dbResult.Injector.Inject(uiList);
                    dbResult.Injector.Close();
                    result.ResultData = uiList;
                }

                return result;
            }
        }
    }
}
