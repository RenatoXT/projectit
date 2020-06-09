﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using projectit.Models;

namespace projectit.DAO
{
    public class TeamDAO : DefaultDAO<TeamViewModel>
    {
        protected override void SetTable()
        {
            Table = "tbTeam";
        }

        protected override SqlParameter[] CreateParams(TeamViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = Convert.FromBase64String("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");

            SqlParameter[] param =
            {
                new SqlParameter("team_picture", imgByte),
                new SqlParameter("team_name", model.name),
                new SqlParameter("team_skill", model.skill),
                new SqlParameter("team_created_at", DateTime.Now),
                new SqlParameter("team_updated_at", DateTime.Now),
                new SqlParameter("user_id", model.user_id)

            };

            return param;
        }

        protected override SqlParameter[] UpdateParams(TeamViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                new SqlParameter("id", model.id),
                new SqlParameter("team_picture", imgByte),
                new SqlParameter("team_name", model.name),
                new SqlParameter("team_skill", model.skill),
                new SqlParameter("team_updated_at", DateTime.Now)

            };

            return param;
        }

        protected override TeamViewModel MountModel(DataRow register)
        {
            TeamViewModel model = new TeamViewModel()
            {
                id = Convert.ToInt32(register["team_id"]),
                name = register["team_name"].ToString(),
                skill = register["team_skill"].ToString(),
                created_at = Convert.ToDateTime(register["team_created_at"]),
                updated_at = Convert.ToDateTime(register["team_updated_at"])
            };


            if (register["team_picture"] != DBNull.Value)
                model.Byte_picture = register["team_picture"] as byte[];

            return model;
        }


        public List<TeamViewModel> ListUserTeams(int users_id)
        {
            List<TeamViewModel> list = new List<TeamViewModel>();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("users_id", users_id)
            };

            DataTable table = HelperDAO.RunProcSelect("spListUserTeams", parameter);
            //return table;
            foreach (DataRow register in table.Rows)
                list.Add(MountModel(register));
            return list;
        }


    }
}
