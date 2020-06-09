using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using projectit.Models;

namespace projectit.DAO
{
    public class PostItDAO : DefaultDAO<PostItViewModel>
    {
        
        protected override void SetTable()
        {
            Table = "tbPostIt";
        }

        protected override SqlParameter[] CreateParams(PostItViewModel model)
        {
            switch (model.status)
            {
                case "1":
                    model.status = "To Do";
                    break;
                case "2":
                    model.status = "In Progress";
                    break;
                case "3":
                    model.status = "Testing";
                    break;
                case "4":
                    model.status = "Done";
                    break;
                default:
                    model.status = "Stories";
                    break;
            }
            
            SqlParameter[] param =
            {
                new SqlParameter("postit_header", model.header),
                new SqlParameter("postit_body", model.body),
                new SqlParameter("postit_doing_by", model.doing_by),
                new SqlParameter("postit_status", model.status),
                new SqlParameter("postit_created_at", DateTime.Now),
                new SqlParameter("postit_updated_at", DateTime.Now),
                new SqlParameter("project_id", model.project_id)

            };

            return param;
        }

        protected override SqlParameter[] UpdateParams(PostItViewModel model)
        {
            SqlParameter[] param =
            {
                new SqlParameter("id", model.id),
                new SqlParameter("postit_header", model.header),
                new SqlParameter("postit_body", model.body),
                new SqlParameter("postit_doing_by", model.doing_by),
                new SqlParameter("postit_status", model.status),
                new SqlParameter("postit_updated_at", DateTime.Now)

            };

            return param;
        }

        protected override PostItViewModel MountModel(DataRow register)
        {
            PostItViewModel model = new PostItViewModel()
            {
                id = Convert.ToInt32(register["postit_id"]),
                header = register["postit_header"].ToString(),
                body = register["postit_body"].ToString(),
                doing_by = register["postit_doing_by"].ToString(),
                status = register["postit_status"].ToString(),
                created_at = Convert.ToDateTime(register["postit_created_at"]),
                updated_at = Convert.ToDateTime(register["postit_updated_at"])
            };

            return model;
        }

        public List<PostItViewModel> ListPostIts(int project_id)
        {
            List<PostItViewModel> list = new List<PostItViewModel>();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("project_id", project_id)
            };

            DataTable table = HelperDAO.RunProcSelect("spListPostIts", parameter);
            //return table;
            foreach (DataRow register in table.Rows)
                list.Add(MountModel(register));
            return list;
        }


    }
}
