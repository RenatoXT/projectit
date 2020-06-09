﻿using System;
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
        protected override void SetTable()
        {
            Table = "tbProject";
        }

        protected override SqlParameter[] CreateParams(ProjectViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = Convert.FromBase64String("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");

            SqlParameter[] param =
            {
                new SqlParameter("project_code", model.code),
                new SqlParameter("project_picture", imgByte),
                new SqlParameter("project_description", model.description),
                new SqlParameter("project_created_at", DateTime.Now),
                new SqlParameter("project_updated_at", DateTime.Now),
                new SqlParameter("team_id", model.team_id)

            };

            return param;
        }

        protected override SqlParameter[] UpdateParams(ProjectViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                new SqlParameter("id", model.id),
                new SqlParameter("project_picture", imgByte),
                new SqlParameter("project_description", model.description),
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

        public List<ProjectViewModel> ListTeamProjects(int team_id)
        {
            List<ProjectViewModel> list = new List<ProjectViewModel>();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("team_id", team_id)
            };

            DataTable table = HelperDAO.RunProcSelect("spListTeamProjects", parameter);
            //return table;
            foreach (DataRow register in table.Rows)
                list.Add(MountModel(register));
            return list;
        }





    }
}
