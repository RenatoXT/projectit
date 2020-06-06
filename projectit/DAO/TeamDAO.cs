using System;
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
        protected override SqlParameter[] CreateParams(TeamViewModel model)
        {
            object imgByte = model.Byte_picture;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] param =
            {
                //new SqlParameter("team_id", model.id),
                new SqlParameter("team_picture", imgByte),
                new SqlParameter("team_name", model.name),
                new SqlParameter("team_skill", model.skill),
                new SqlParameter("team_created_at", DateTime.Now),
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

        protected override void SetTable()
        {
            Table = "tbTeam";
        }
    }
}
