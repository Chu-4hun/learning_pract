using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Department
    {
        public Department()
        {
        }

        public Department(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_department"].ToString(), out _id);
            name = data["dep_number"].ToString();
        }

        public string name { get; set; }

        private int _id;

        public int ID
        {
            get => _id;
        }
        public static List<Department> getAll()
        {
            List<Department> list = new List<Department>();
            foreach (var data in App.db.execute("select * from Department where true;"))
            {
                list.Add(new Department(data));
            }

            return list;
        }

        public static Department getById(int id)
        {
            var data = App.db.execute("select * from Department where ID_Department=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id.ToString()}
                });
            if (data.Count == 0)
            {
                return new Department();
            }

            return new Department(data[0]);
        }
    }
}