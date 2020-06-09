using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using projectit.Models;

namespace projectit.DAO
{
    public class UsersDAO : DefaultDAO<UsersViewModel>
    {

        protected override void SetTable()
        {
            Table = "tbUsers";
        }

        protected override SqlParameter[] CreateParams(UsersViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = Convert.FromBase64String("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");

            SqlParameter[] param =
            {
                new SqlParameter("users_name", model.name),
                new SqlParameter("users_picture", imgByte),
                new SqlParameter("users_nickname", model.nickname),
                new SqlParameter("users_email", model.email),
                new SqlParameter("users_password", model.password),
                new SqlParameter("users_created_at", DateTime.Now),
                new SqlParameter("users_updated_at", DateTime.Now)

            };

            return param;
        }

        protected override SqlParameter[] UpdateParams(UsersViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                new SqlParameter("id", model.id),
                new SqlParameter("users_picture", imgByte),
                new SqlParameter("users_nickname", model.nickname),
                new SqlParameter("users_email", model.email),
                new SqlParameter("users_password", model.password),
                new SqlParameter("users_updated_at", DateTime.Now)

            };

            return param;
        }


        protected override UsersViewModel MountModel(DataRow register)
        {
            UsersViewModel user = new UsersViewModel()
            {
                id = Convert.ToInt32(register["users_id"]),
                name = register["users_name"].ToString(),
                nickname = register["users_nickname"].ToString(),
                email = register["users_email"].ToString(),
                password = register["users_password"].ToString(),
                created_at = Convert.ToDateTime(register["users_created_at"]),
                updated_at = Convert.ToDateTime(register["users_updated_at"])
            };


            if (register["users_picture"] != DBNull.Value)
                user.Byte_picture = register["users_picture"] as byte[];

            return user;
        }

        public virtual bool Login(string user, string password)
        {
            var p = new SqlParameter[]
          {
                new SqlParameter("user", "''"+user+"''"),
                new SqlParameter("password","''"+password+"''")
          };

            var table = HelperDAO.RunProcSelect("spLogin", p);
            if (table.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public List<UsersViewModel> ListTeamMembers(int team_id)
        {
            List<UsersViewModel> list = new List<UsersViewModel>();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("team_id", team_id)
            };

            DataTable table = HelperDAO.RunProcSelect("spListTeamMembers", parameter);
            //return table;
            foreach (DataRow register in table.Rows)
                list.Add(MountModel(register));
            return list;
        }

        public List<UsersViewModel> ListProjectMembers(int project_id)
        {
            List<UsersViewModel> list = new List<UsersViewModel>();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("project_id", project_id)
            };

            DataTable table = HelperDAO.RunProcSelect("spListProjectMembers", parameter);
            //return table;
            foreach (DataRow register in table.Rows)
                list.Add(MountModel(register));
            return list;
        }

    }
}
