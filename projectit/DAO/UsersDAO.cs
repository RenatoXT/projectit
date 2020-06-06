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
        protected override SqlParameter[] CreateParams(UsersViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                //new SqlParameter("users_id", model.id),
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

        protected override UsersViewModel MountModel(DataRow register)
        {
            UsersViewModel user = new UsersViewModel()
            {
                id = Convert.ToInt32(register["users_id"]),
                name = register["users_name"].ToString(),
                nickname = register["users_name"].ToString(),
                email = register["users_email"].ToString(),
                password = register["users_password"].ToString(),
                created_at = Convert.ToDateTime(register["users_created_at"]),
                updated_at = Convert.ToDateTime(register["users_updated_at"])
            };


            if (register["users_picture"] != DBNull.Value)
                user.Byte_picture = register["users_picture"] as byte[];

            return user;
        }

        protected override void SetTable()
        {
            Table = "tbUsers";
        }
    }
}
