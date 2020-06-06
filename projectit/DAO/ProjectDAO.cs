using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using projectit.Models;

namespace projectit.DAO
{
    public class ProjectDAO : DefaultDAO<ProjectViewModel>
    {
        protected override SqlParameter[] CreateParams(ProjectViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                //new SqlParameter("project_id", model.id),
                new SqlParameter("project_code", model.code),
                new SqlParameter("project_picture", imgByte),
                new SqlParameter("project_description", model.description),
                new SqlParameter("project_created_at", DateTime.Now),
                new SqlParameter("project_updated_at", DateTime.Now)

            };

            return param;
        }

        protected override ProjectViewModel MountModel(DataRow register)
        {
            ProjectViewModel model = new ProjectViewModel()
            {
                id = Convert.ToInt32(register["project_id"]),
                code = register["project_code"].ToString(),
                description = register["project_description"].ToString(),
                updated_at = Convert.ToDateTime(register["project_updated_at"]),
                created_at = Convert.ToDateTime(register["project_created_at"]),

            };


            if (register["project_picture"] != DBNull.Value)
                model.Byte_picture = register["project_picture"] as byte[];

            return model;
        }

        protected override void SetTable()
        {
            Table = "tbProject";
        }
    }
}
