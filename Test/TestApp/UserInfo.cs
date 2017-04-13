using Alive.Foundation.Data;
using Alive.Foundation.Data.DataFields;

namespace TestApp
{
    public class UserInfo:BusinessObject
    {
        #region ==== 公共属性 ====

        public StringField ID
        {
            get 
            {
                return this.GetField<StringField>("ID");
            }
            set
            {
                this.GetField<StringField>("ID").Value = value.Value;
            }
        }

        public StringField UserName
        {
            get
            {
                return this.GetField<StringField>("UserName");
            }
            set
            {
                this.GetField<StringField>("UserName").Value = value.Value;
            }
        }

        public StringField Password
        {
            get
            {
                return this.GetField<StringField>("Password");
            }
            set
            {
                this.GetField<StringField>("Password").Value = value.Value;
            }
        }

        public StringField RoleID
        {
            get
            {
                return this.GetField<StringField>("RoleID");
            }
            set
            {
                this.GetField<StringField>("RoleID").Value = value.Value;
            }
        }

        #endregion
    }
}
